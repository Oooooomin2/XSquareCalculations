using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XSquareCalculationsApi.ViewModels;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Repositories;
using XSquareCalculationsApi.Services.Templates;

namespace XSquareCalculationsApi.Controllers
{
    [Route("api/[controller]")]
    public class TemplateController : Controller
    {
        private readonly IResolveTemplatesRepository _resolveTemplatesRepository;
        private readonly IDownloadTemplateService _downloadTemplateService;
        private readonly IResolveAthenticateRepository _resolveAthenticateRepository;
        private readonly ISystemDate _systemDate;

        public TemplateController(
            ISystemDate systemDate,
            IResolveTemplatesRepository resolveTemplatesRepository,
            IDownloadTemplateService downloadTemplateService,
            IResolveAthenticateRepository resolveAthenticateRepository)
        {
            _systemDate = systemDate;
            _resolveTemplatesRepository = resolveTemplatesRepository;
            _downloadTemplateService = downloadTemplateService;
            _resolveAthenticateRepository = resolveAthenticateRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TemplateViewModel>> GetTemplates()
        {
            return await _resolveTemplatesRepository.GetTemplatesAsync();
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadTemplateAsync(int id)
        {
            var templateBlob = await _downloadTemplateService.DownloadTemplateAsync(id);
            if(templateBlob == null)
            {
                return NotFound();
            }
            return File(templateBlob, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateViewModel>> GetTemplateDetail(int id)
        {
            var templateDetail = await _resolveTemplatesRepository.GetTemplateDetailAsync(id);
            if(templateDetail == null) return NotFound();

            return templateDetail;
        }

        [HttpPost]
        public IActionResult Create([FromForm] Template template)
        {
            Request.Headers.TryGetValue("Authorization", out var idToken);
            Request.Headers.TryGetValue("UserId", out var userIdStr);
            if (string.IsNullOrEmpty(idToken) || string.IsNullOrEmpty(userIdStr))
            {
                return Unauthorized();
            }

            int.TryParse(userIdStr, out var userId);

            var target = _resolveAthenticateRepository.GetLoginAuthData(userId, idToken);
            if(target == null)
            {
                return Unauthorized();
            }

            var isExpired = target.ExpiredDateTime < _systemDate.GetSystemDate();
            if (isExpired)
            {
                return Unauthorized();
            }

            var formFiles = HttpContext.Request.Form.Files;
            foreach(var file in formFiles)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                if(file.Name == "templateBlob")
                {
                    template.TemplateBlob = fileBytes;
                }
                if(file.Name == "thumbNail")
                {
                    template.ThumbNail = Convert.ToBase64String(fileBytes);
                }
            }
            template.UserId = userId;

            _resolveTemplatesRepository.RegistTemplate(template);

            return Ok();
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Template template)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var target = await _context.Templates.FindAsync(id);
            if (target == null)
                return NotFound();

            target.TemplateName = template.TemplateName;
            target.TemplateBlob = template.TemplateBlob;
            target.LikeCount = template.LikeCount;
            target.UpdatedTime = template.UpdatedTime;

            _context.Templates.Update(target);
            await _context.SaveChangesAsync();

            return Ok();
        }*/

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var target = await _context.Templates.FindAsync(id);
            if (target == null)
                return NotFound();

            target.DelFlg = "1";
            await _context.SaveChangesAsync();

            return Ok(id);
        }*/
    }
}
