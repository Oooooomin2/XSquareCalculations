using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Repositories;
using Xunit;

namespace XSquareCalculationsApiTest.Repositories
{
    public sealed class ResolveTemplatesRepositoryTest
    {
        [Fact]
        internal async Task GetTemplateDetailAsyncTest()
        {
            using var context = new XSquareCalculationsAPITestDbContextFactory().CreateDbContext();
            
            var template = new Template
            {
                TemplateId = 1,
                TemplateName = "template1",
                ThumbNail = "12345",
                LikeCount = 1,
                DownloadCount = 1,
                DelFlg = "0",
                UserId = 1
            };

            var user = new User
            {
                UserId = 1,
                UserName = "test1",
                UserPassword = "12345",
                LikeNumberSum = 1,
                PasswordSalt = "12345",
                DelFlg = "0"
            };

            context.Templates.Add(template);
            context.Users.Add(user);
            context.SaveChanges();

            var systemDateMock = new Mock<ISystemDate>();
            var systemDate = systemDateMock
                .Setup(o => o.GetSystemDate())
                .Returns(new DateTime(2021, 1, 1));

            var target = new ResolveTemplatesRepository(context, systemDateMock.Object);
            var templateDetail = await target.GetTemplateDetailAsync(1);

            Assert.Equal(template, templateDetail.Template);
            Assert.Equal("test1", templateDetail.UserName);
        }

        [Fact]
        internal async Task GetTemplatesAsyncTest()
        {
            using var context = new XSquareCalculationsAPITestDbContextFactory().CreateDbContext();

            var template1 = new Template
            {
                TemplateId = 1,
                TemplateName = "template1",
                ThumbNail = "12345",
                LikeCount = 1,
                DownloadCount = 1,
                DelFlg = "0",
                UserId = 1
            };

            var user1 = new User
            {
                UserId = 1,
                UserName = "test1",
                UserPassword = "12345",
                LikeNumberSum = 1,
                PasswordSalt = "12345",
                DelFlg = "0"
            };

            var template2 = new Template
            {
                TemplateId = 2,
                TemplateName = "template2",
                ThumbNail = "12345",
                LikeCount = 1,
                DownloadCount = 1,
                DelFlg = "0",
                UserId = 2
            };

            var user2 = new User
            {
                UserId = 2,
                UserName = "test2",
                UserPassword = "12345",
                LikeNumberSum = 1,
                PasswordSalt = "12345",
                DelFlg = "0"
            };

            context.Templates.Add(template1);
            context.Users.Add(user1);
            context.Templates.Add(template2);
            context.Users.Add(user2);
            context.SaveChanges();

            var systemDateMock = new Mock<ISystemDate>();
            var systemDate = systemDateMock
                .Setup(o => o.GetSystemDate())
                .Returns(new DateTime(2021, 1, 1));

            var target = new ResolveTemplatesRepository(context, systemDateMock.Object);
            var templates = await target.GetTemplatesAsync();

            Assert.Equal(template1, templates.Single(o => o.Template.TemplateId == 1).Template);
            Assert.Equal("test1", templates.Single(o => o.Template.TemplateId == 1).UserName);
            Assert.Equal(template2, templates.Single(o => o.Template.TemplateId == 2).Template);
            Assert.Equal("test2", templates.Single(o => o.Template.TemplateId == 2).UserName);
        }
    }
}
