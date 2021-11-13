using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Persistance;
using XSquareCalculationsApi.ViewModels;

namespace XSquareCalculationsApi.Repositories
{
    public interface IResolveTemplatesRepository
    {
        Task<IEnumerable<TemplateViewModel>> GetTemplatesAsync();
        Task<Template> GetDownloadTargetAsync(int id);
        void IncrementDownloadCount(Template template);
        Task<TemplateViewModel> GetTemplateDetailAsync(int id);
        void RegistTemplate(Template template);
    }

    public class ResolveTemplatesRepository : IResolveTemplatesRepository
    {
        private readonly XSquareCalculationContext _context;
        private readonly ISystemDate _systemDate;

        public ResolveTemplatesRepository(XSquareCalculationContext context, ISystemDate systemDate)
        {
            _context = context;
            _systemDate = systemDate;
        }

        public async Task<Template> GetDownloadTargetAsync(int id)
        {
            return await _context.Templates.SingleOrDefaultAsync(o => o.TemplateId == id);
        }

        public async Task<TemplateViewModel> GetTemplateDetailAsync(int id)
        {
            return await _context.Templates
                .Where(o => o.TemplateId == id)
                .Join(
                    _context.Users,
                    t => t.UserId,
                    u => u.UserId,
                    (template, user) => new TemplateViewModel
                    {
                        Template = template,
                        UserName = user.UserName
                    })
                .Select(joinTable => new TemplateViewModel
                {
                    Template = joinTable.Template,
                    UserName = joinTable.UserName
                })
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TemplateViewModel>> GetTemplatesAsync()
        {
            return await _context.Templates
                .Join(
                    _context.Users,
                    t => t.UserId,
                    u => u.UserId,
                    (template, user) => new TemplateViewModel
                    {
                        Template = template,
                        UserName = user.UserName
                    })
                .Select(joinTable => new TemplateViewModel
                {
                    Template = joinTable.Template,
                    UserName = joinTable.UserName
                })
                .ToListAsync();
        }

        public void IncrementDownloadCount(Template template)
        {
            template.DownloadCount++;
            _context.SaveChanges();
        }

        public void RegistTemplate(Template template)
        {
            var nowDatetime = _systemDate.GetSystemDate();
            _context.Templates.Add(new Template
            {
                TemplateName = template.TemplateName,
                UserId = template.UserId,
                ThumbNail = template.ThumbNail,
                TemplateBlob = template.TemplateBlob,
                DelFlg = "0",
                DownloadCount = 0,
                LikeCount = 0,
                CreatedTime = nowDatetime,
                UpdatedTime = nowDatetime
            }); ;
            _context.SaveChanges();
        }
    }
}
