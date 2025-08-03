using CBS.Data;
using CBS.Repository;
using CBS.Service;
using CBS.Service.Cache;
using CBS.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sample.Service.Service.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CBS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkSqlServer().AddDbContextPool<RoutingDbContext>(b => b.UseSqlServer(connectionString));

            services.AddScoped<TenantDbContext>(provider =>
            {
                var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                var connectionString = httpContext?.Items["TenantConnectionString"]?.ToString();

                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Tenant connection string not found.");

                var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                return new TenantDbContext(optionsBuilder.Options);
            });

            services.AddHttpContextAccessor();
            services.AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins(
                        "http://localhost:5173",
                        "http://localhost:5174",
                        "https://iconsoft-dashboard.netlify.app",
                        "https://iconsoft-dashboard11.netlify.app") // Include the production URL
                           .AllowAnyMethod() // Allow all HTTP methods
                           .AllowAnyHeader() // Allow all headers
                           .AllowCredentials(); // Required if using cookies or Authorization headers
                });
            });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Banking System", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT" // Optional
                };

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        new string[] {}
                    }
                };
                c.EnableAnnotations();
                // Configure Swagger to display enums as strings
                c.SchemaGeneratorOptions = new Swashbuckle.AspNetCore.SwaggerGen.SchemaGeneratorOptions
                {
                    UseInlineDefinitionsForEnums = true
                };

                //c.DocumentFilter<SwaggerEndpointFilter>();

                //c.DescribeAllEnumsAsStrings(); // For older versions of Swashbuckle
                c.AddSecurityDefinition("bearerAuth", securityScheme);
                c.AddSecurityRequirement(securityRequirement);
            });

            #region JWT

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],

                    ValidateLifetime = true, // Ensure the token's `exp` claim is valid
                    ClockSkew = TimeSpan.Zero, // Avoid issues with clock drift during validation

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully.");
                        string authHeader = context.Request.Headers["Authorization"];
                        if (authHeader != null && authHeader.StartsWith("Bearer"))
                        {
                            var token = authHeader.Substring("Bearer ".Length).Trim();
                            var decodeToken = new JwtSecurityToken(jwtEncodedString: token);
                            string tenantId = decodeToken.Claims.First(c => c.Type == "tenantId").Value;
                            string userId = decodeToken.Claims.First(c => c.Type == "userId").Value;
                            string sessionId = decodeToken.Claims.First(c => c.Type == "sessionId").Value;

                            context.Request.Headers["Tenant-ID"] = tenantId;
                            context.Request.Headers["User-ID"] = userId;
                            context.Request.Headers["Session-ID"] = sessionId;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine("Challenge issued: " + context.ErrorDescription);
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();

            #endregion JWT

            services.AddMemoryCache();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            RegisterLibrary(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Banking API V1");
                c.DisplayRequestDuration();
                c.InjectJavascript("/swagger-ui/swagger-search.js"); // Custom JS for search
            });
            //}

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseMiddleware<TenantMiddleware>();

            app.UseMiddleware<PermissionMiddleware>();
            app.Use(async (context, next) =>
            {
                var endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    Console.WriteLine($"Matched Endpoint: {endpoint.DisplayName}");
                }
                else
                {
                    Console.WriteLine("No endpoint matched.");
                }
                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterLibrary(IServiceCollection services)
        {
            #region DependencyInjection

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserPreferenceService, UserPreferenceService>();
            services.AddScoped<AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IClientService, ClientService>();
            #endregion DependencyInjection
        }
    }
}