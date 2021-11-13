using Moq;
using System.Threading.Tasks;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Repositories;
using XSquareCalculationsApi.Services.Templates;
using Xunit;

namespace XSquareCalculationsApiTest.Services.Templates
{
    public sealed class DownloadTemplateServiceTest
    {
        [Fact]
        internal async Task DownloadTemplateAsyncNullTest()
        {
            var resolveTemplatesRepositoryMock = new Mock<IResolveTemplatesRepository>();
            resolveTemplatesRepositoryMock
                .Setup(o => o.GetDownloadTargetAsync(It.IsAny<int>()))
                .ReturnsAsync((Template)null);

            var target = new DownloadTemplateService(resolveTemplatesRepositoryMock.Object);
            var result = await target.DownloadTemplateAsync(It.IsAny<int>());

            Assert.Null(result);
        }

        [Fact]
        internal async Task DownloadTemplateAsyncCorrectTest()
        {
            var template = new Template
            {
                TemplateId = 1,
                TemplateBlob = new byte[1] { 0x00 }
            };

            var resolveTemplatesRepositoryMock = new Mock<IResolveTemplatesRepository>();
            resolveTemplatesRepositoryMock
                .Setup(o => o.GetDownloadTargetAsync(It.IsAny<int>()))
                .ReturnsAsync(template);

            var target = new DownloadTemplateService(resolveTemplatesRepositoryMock.Object);
            var result = await target.DownloadTemplateAsync(It.IsAny<int>());

            Assert.Equal(new byte[1] { 0x00 }, template.TemplateBlob);
        }
    }
}
