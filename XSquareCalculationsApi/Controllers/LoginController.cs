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
        private readonly IResolveJwtAuthenticate _resolveJwtAuthenticate;
        private readonly IPassword _password;

        public LoginController(
            IResolveAthenticateRepository resolveAthenticateRepository,
            IResolveUsersRepository resolveUserRepository,
            IResolveJwtAuthenticate resolveJwtAuthenticate,
            IPassword password)
        {
            _resolveAthenticateRepository = resolveAthenticateRepository;
            _resolveUserRepository = resolveUserRepository;
            _resolveJwtAuthenticate = resolveJwtAuthenticate;
            _password = password;
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
                var jwtResponse = _resolveJwtAuthenticate.CreateJwtResponse(target.UserId,target.UserName);
                var auth = new Authenticate
                {
                    UserId = jwtResponse.UserId,
                    IdToken = jwtResponse.IdToken,
                    ExpiredDateTime = jwtResponse.ExpiredDateTime,
                    CreatedTime = jwtResponse.CreatedTime
                };
                _resolveAthenticateRepository.AddLoginHistory(auth);

                return Ok(jwtResponse);
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
