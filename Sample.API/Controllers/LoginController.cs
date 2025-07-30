using Azure;
using CBS.Data.DTO;
using CBS.Data.RoutingDB;
using CBS.Service;
using CBS.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CBS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly AuthService _authService;

        public LoginController(ILoginService loginService, AuthService authService)
        {
            _loginService = loginService;
            _authService = authService;
        }

        [HttpPost("userauthentication")]
        public async Task<IActionResult> UserLogin([FromHeader(Name = "Tenant-ID")] int tenantId, [FromBody] LoginDTO login)
        {
            try
            {
                var response = _loginService.GetLoggedInUserDetail(login.UserName, login.Password, tenantId);

                if (response.isLoginSuccess == true)
                {
                    var token = _authService.GenerateJwtToken(tenantId, response.userDetail.Id);
                    return new OkObjectResult(new
                    {
                        UserDetail = response.userDetail,
                        Token = token
                    });
                }
                else
                {
                    return new BadRequestObjectResult(new
                    {
                        ErrorMessage = "User Login Failed"
                    });
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}