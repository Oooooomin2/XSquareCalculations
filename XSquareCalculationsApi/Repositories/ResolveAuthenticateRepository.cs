using System.Linq;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Persistance;

namespace XSquareCalculationsApi.Repositories
{
    public interface IResolveAthenticateRepository
    {
        Authenticate GetLoginAuthData(int userId, string idToken);
        void AddLoginHistory(Authenticate auth);
    }

    public class ResolveAuthenticateRepository : IResolveAthenticateRepository
    {
        private readonly XSquareCalculationContext _context;

        public ResolveAuthenticateRepository(XSquareCalculationContext context)
        {
            _context = context;
        }

        public void AddLoginHistory(Authenticate auth)
        {
            _context.Authenticates.Add(auth);
            _context.SaveChanges();
        }

        public Authenticate GetLoginAuthData(int userId, string idToken)
        {
            return _context.Authenticates
                .SingleOrDefault(o => o.UserId == userId && o.IdToken == idToken.ToString());
        }
    }
}
