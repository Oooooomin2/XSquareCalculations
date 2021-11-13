using System.Threading.Tasks;
using XSquareCalculationsApi.Repositories;

namespace XSquareCalculationsApi.Services.Templates
{
    public interface IDownloadTemplateService
    {
        Task<byte[]> DownloadTemplateAsync(int templateId);
    }

    public class DownloadTemplateService : IDownloadTemplateService
    {
        private readonly IResolveTemplatesRepository _resolveTemplatesRepository;

        public DownloadTemplateService(IResolveTemplatesRepository resolveTemplatesRepository)
        {
            _resolveTemplatesRepository = resolveTemplatesRepository;
        }

        public async Task<byte[]> DownloadTemplateAsync(int templateId)
        {
            var target = await _resolveTemplatesRepository.GetDownloadTargetAsync(templateId);
            if (target == null)
            {
                return null ;
            }
            return target.TemplateBlob;
        }
    }
}
