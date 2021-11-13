using System;
using System.Linq;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Repositories;
using Xunit;

namespace XSquareCalculationsApiTest.Repositories
{
    public sealed class ResolveAuthenticateRepositoryTest
    {
        [Fact]
        internal void AddLoginHistoryTest()
        {
            using var context = new XSquareCalculationsAPITestDbContextFactory().CreateDbContext();
            var auth = new Authenticate
            {
                UserId = 1,
                IdToken = "12345",
                ExpiredDateTime = new DateTime(2021, 1, 1),
                CreatedTime = new DateTime(2021, 2, 1)
            };
            var target = new ResolveAuthenticateRepository(context);
            target.AddLoginHistory(auth);
            var actual = context.Authenticates
                .Single(o => o.UserId == 1);
            Assert.Equal("12345", actual.IdToken);
            Assert.Equal(new DateTime(2021, 1, 1), actual.ExpiredDateTime);
            Assert.Equal(new DateTime(2021, 2, 1), actual.CreatedTime);
        }

        [Fact]
        internal void GetRecentLoginDataTest()
        {
            using var context = new XSquareCalculationsAPITestDbContextFactory().CreateDbContext();
            var authOld = new Authenticate
            {
                UserId = 1,
                IdToken = "12345",
                ExpiredDateTime = new DateTime(2021, 1, 1),
                CreatedTime = new DateTime(2021, 2, 1)
            };

            var authNew = new Authenticate
            {
                UserId = 1,
                IdToken = "123456",
                ExpiredDateTime = new DateTime(2021, 1, 2),
                CreatedTime = new DateTime(2021, 2, 2)
            };

            var authOtherUser = new Authenticate
            {
                UserId = 2,
                IdToken = "1234567",
                ExpiredDateTime = new DateTime(2021, 1, 2),
                CreatedTime = new DateTime(2021, 2, 2)
            };

            context.Authenticates.Add(authOld);
            context.Authenticates.Add(authNew);
            context.Authenticates.Add(authOtherUser);
            context.SaveChanges();

            var target = new ResolveAuthenticateRepository(context);
            var recentLoginDataUser1 = target.GetLoginAuthData(1, "123456");
            Assert.Equal(authNew, recentLoginDataUser1);
        }
    }
}
