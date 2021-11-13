using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XSquareCalculationsApi.Controllers;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Repositories;
using XSquareCalculationsApi.Services.Templates;
using XSquareCalculationsApi.ViewModels;
using Xunit;

namespace XSquareCalculationsApiTest.Controllers
{
    public sealed class TemplateControllerTest
    {
        private readonly Mock<IResolveTemplatesRepository> _resolveTemplatesRepositoryMock;
        private readonly Mock<IDownloadTemplateService> _downloadTemplateServiceMock;
        private readonly Mock<IResolveAthenticateRepository> _resolveAthenticateRepositoryMock;
        private readonly Mock<ISystemDate> _systemDateMock;

        public TemplateControllerTest()
        {
            _resolveTemplatesRepositoryMock = new Mock<IResolveTemplatesRepository>();
            _downloadTemplateServiceMock = new Mock<IDownloadTemplateService>();
            _resolveAthenticateRepositoryMock = new Mock<IResolveAthenticateRepository>();
            _systemDateMock = new Mock<ISystemDate>();
        }

        [Fact]
        internal async Task DownloadTemplateNullTest()
        {
            _downloadTemplateServiceMock
                .Setup(o => o.DownloadTemplateAsync(It.IsAny<int>()))
                .ReturnsAsync((byte[])null);

            var target = new TemplateController(
                _systemDateMock.Object,
                _resolveTemplatesRepositoryMock.Object,
                _downloadTemplateServiceMock.Object,
                _resolveAthenticateRepositoryMock.Object);

            var result = await target.DownloadTemplateAsync(It.IsAny<int>());
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        internal async Task GetTemplateDetailNullTest()
        {
            _resolveTemplatesRepositoryMock
                .Setup(o => o.GetTemplateDetailAsync(It.IsAny<int>()))
                .ReturnsAsync((TemplateViewModel)null);

            var target = new TemplateController(
                _systemDateMock.Object,
                _resolveTemplatesRepositoryMock.Object,
                _downloadTemplateServiceMock.Object,
                _resolveAthenticateRepositoryMock.Object);

            var result = await target.GetTemplateDetail(It.IsAny<int>());
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        internal void CreateWhenHeaderValueEmptyOrNullUnauthorizedTest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = string.Empty;
            httpContext.Request.Headers["UserId"] = string.Empty;

            var target = new TemplateController(
                _systemDateMock.Object,
                _resolveTemplatesRepositoryMock.Object,
                _downloadTemplateServiceMock.Object,
                _resolveAthenticateRepositoryMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            var result = target.Create(It.IsAny<Template>());
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        internal void CreateUnauthorizedTest()
        {
            _resolveAthenticateRepositoryMock
                .Setup(o => o.GetLoginAuthData(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((Authenticate)null);

            _systemDateMock
                .Setup(o => o.GetSystemDate())
                .Returns(new DateTime(2021, 1, 1));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "12345";
            httpContext.Request.Headers["UserId"] = "1";

            var target = new TemplateController(
                _systemDateMock.Object,
                _resolveTemplatesRepositoryMock.Object,
                _downloadTemplateServiceMock.Object,
                _resolveAthenticateRepositoryMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            // ログインデータがない場合はUnauthorizedエラーを返却
            var resultNotHasLoginData = target.Create(It.IsAny<Template>());
            Assert.IsType<UnauthorizedResult>(resultNotHasLoginData);

            _resolveAthenticateRepositoryMock
                .Setup(o => o.GetLoginAuthData(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new Authenticate { ExpiredDateTime = new DateTime(2020, 12, 31) });

            // ログイン認証キーが有効期限を切れていた場合はUnauthorizedエラーを返却
            var resultExpired = target.Create(It.IsAny<Template>());
            Assert.IsType<UnauthorizedResult>(resultExpired);
        }
    }
}
