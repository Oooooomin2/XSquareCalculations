using Microsoft.AspNetCore.Mvc;
using System;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Repositories;

namespace XSquareCalculationsApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IResolveUsersRepository _resolveUsersRepository;
        private readonly IPassword _password;

        public UserController(IResolveUsersRepository resolveUsersRepository, IPassword password)
        {
            _resolveUsersRepository = resolveUsersRepository;
            _password = password;
        }

        /*[HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserDetail(int id)
        {
            var detail = await _context.Users.SingleOrDefaultAsync(o => o.UserId == id);
            if(detail == null)
            {
                return NotFound();
            }

            return detail;
        }*/

        [HttpPost]
        public ActionResult Create([FromForm] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var IsExistedUserName = _resolveUsersRepository.IsDuplicateUserRegist(user.UserName);
            if (IsExistedUserName)
            {
                return BadRequest(new ApiResponse
                { 
                    Content = "DuplicateUserName",
                    Message = "このユーザ名は既に使われています。"
                });
            }

            if(user.UserPassword.Length < 8)
            {
                return BadRequest(new ApiResponse
                {
                    Content = "ShortPasswordLength",
                    Message = "パスワードは8文字以上にしてください。"
                });
            }

            user.PasswordSalt = _password.CreateSaltBase64();
            user.UserPassword = _password.CreatePasswordHashBase64(user.PasswordSalt, user.UserPassword);

            _resolveUsersRepository.CreateNewUser(user);

            return Ok();
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var target = await _context.Users.FindAsync(id);
            if (target == null)
                return NotFound();

            target.UserName = user.UserName;
            target.UpdatedTime = user.UpdatedTime;

            _context.Users.Update(target);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var target = await _context.Users.FindAsync(id);
            if (target == null)
                return NotFound();

            target.DelFlg = "1";
            await _context.SaveChangesAsync();

            return Ok(id);
        }*/
    }
}
