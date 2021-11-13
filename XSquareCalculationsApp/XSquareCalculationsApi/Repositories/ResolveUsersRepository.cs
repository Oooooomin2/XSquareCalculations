using System.Linq;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Persistance;

namespace XSquareCalculationsApi.Repositories
{
    public interface IResolveUsersRepository
    {
        bool IsDuplicateUserRegist(string userName);

        User GetUserWithUserName(string userName);

        void CreateNewUser(User user);
    }

    public class ResolveUsersRepository : IResolveUsersRepository
    {
        private readonly XSquareCalculationContext _context;
        private readonly ISystemDate _systemDate;

        public ResolveUsersRepository(XSquareCalculationContext context, ISystemDate systemDate)
        {
            _context = context;
            _systemDate = systemDate;
        }

        public void CreateNewUser(User user)
        {
            var nowDatetime = _systemDate.GetSystemDate();
            _context.Users.Add(new User
            {
                UserName = user.UserName,
                UserPassword = user.UserPassword,
                PasswordSalt = user.PasswordSalt,
                LikeNumberSum = 0,
                DelFlg = "0",
                CreatedTime = nowDatetime,
                UpdatedTime = nowDatetime
            });
            _context.SaveChanges();
        }

        public User GetUserWithUserName(string userName)
        {
            return _context.Users.SingleOrDefault(o => o.UserName == userName);
        }

        public bool IsDuplicateUserRegist(string userName)
        {
            return _context.Users.Any(o => o.UserName == userName);
        }
    }
}
