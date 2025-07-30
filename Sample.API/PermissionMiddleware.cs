using Microsoft.AspNetCore.Http;

namespace CBS.Service
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPermissionService permissionService)
        {
            // Get the endpoint
            var endpoint = context.GetEndpoint();

            if (endpoint == null)
            {
                // If no endpoint is resolved, continue to the next middleware
                await _next(context);
                return;
            }

            // Check for PermissionAttribute
            var permission = endpoint.Metadata.GetMetadata<PermissionAttribute>()?.PermissionName;
            var action = endpoint.Metadata.GetMetadata<PermissionAttribute>()?.Action;

            if (!string.IsNullOrEmpty(permission))
            {
                // Validate permissions
                var userId = context.Request.Headers["User-ID"].FirstOrDefault();
                if (string.IsNullOrEmpty(userId) || !permissionService.UserHasPermission(int.Parse(userId), permission, action))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Permission Denied");
                    return;
                }
            }

            await _next(context);
        }
    }
}