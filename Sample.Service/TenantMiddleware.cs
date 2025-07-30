using CBS.Data;
using CBS.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, RoutingDbContext masterDbContext)
    {
        var path = context.Request.Path.Value;
        // Bypass Swagger and OpenAPI documentation paths
        if (path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/swagger/index.html", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/swagger/v1/swagger.json", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith("/api-docs", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        var tenantId = context.Request.Headers["Tenant-ID"].FirstOrDefault();

        if (string.IsNullOrEmpty(tenantId) || !int.TryParse(tenantId, out var id))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Tenant-ID header is missing or invalid.");
            return;
        }

        var test = masterDbContext.tblClientDetails.Select(q => q.client_id).ToList();
        var tenant = masterDbContext.tblClientDetails.Select(q => new { q.client_id, q.db_pwd, q.db_name, q.server_name, q.db_username }).Where(t => t.client_id == id).FirstOrDefault();

        if (tenant == null)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Tenant not found.");
            return;
        }
        var connectionData = "server=" + tenant.server_name + ";database=" + tenant.db_name + ";uid=" + tenant.db_username + ";password= " + tenant.db_pwd + ";Encrypt=True;TrustServerCertificate=True;";
        context.Items["TenantConnectionString"] = connectionData;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
            // Log ex.StackTrace if needed
        }
    }
}