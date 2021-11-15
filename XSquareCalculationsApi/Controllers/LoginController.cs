using Microsoft.AspNetCore.Mvc;
using System;
using XSquareCalculationsApi.ViewModels;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Repositories;

namespace XSquareCalculationsApi.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IResolveAthenticateRepository _resolveAthenticateRepository;
        private readonly IResolveUsersRepository _resolveUserRepository;
        private readonly IPassword _password;
        private readonly ISystemDate _systemDate;

        public LoginController(
            IResolveAthenticateRepository resolveAthenticateRepository,
            IResolveUsersRepository resolveUserRepository,
            IPassword password,
            ISystemDate systemDate)
        {
            _resolveAthenticateRepository = resolveAthenticateRepository;
            _resolveUserRepository = resolveUserRepository;
            _password = password;
            _systemDate = systemDate;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var target = _resolveUserRepository.GetUserWithUserName(user.UserName);
            if (target == null) 
            {
                return BadRequest(new ApiResponse
                {
                    Content = "LoginFailed",
                    Message = "ユーザ名かパスワードが正しくありません。"
                });
            }

            var userPasswordBase64 = _password.CreatePasswordHashBase64(target.PasswordSalt, user.UserPassword);
            if (target.UserPassword == userPasswordBase64)
            {
                string token = Guid.NewGuid().ToString("N").Substring(0, 32);
                var createdTime = _systemDate.GetSystemDate();
                var auth = new Authenticate
                {
                    UserId = target.UserId,
                    IdToken = token,
                    ExpiredDateTime = createdTime.AddHours(12),
                    CreatedTime = createdTime
                };
                _resolveAthenticateRepository.AddLoginHistory(auth);

                return Ok(new { auth.UserId, auth.IdToken, auth.ExpiredDateTime });
            }
            else
            {
                return BadRequest(new ApiResponse
                {
                    Content = "LoginFailed",
                    Message = "ユーザ名かパスワードが正しくありません。"
                });
            }
        }
    }
}
