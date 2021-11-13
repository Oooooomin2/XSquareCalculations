using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using XSquareCalculationsApi.Entities;
using XSquareCalculationsApi.Models;
using XSquareCalculationsApi.Persistance;

namespace XSquareCalculationsApi.Controllers
{
    [Route("api/[controller]")]
    public class MessageWithTemplateController : Controller
    {
        private readonly XSquareCalculationContext _context;
        private readonly ISystemDate _systemDate;

        public MessageWithTemplateController(XSquareCalculationContext context, ISystemDate systemDate)
        {
            _context = context;
            _systemDate = systemDate;
        }

        [HttpGet]
        public async Task<IEnumerable<MessagesWithTemplate>> GetMessages()
        {
            return await _context.MessagesWithTemplates.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MessagesWithTemplate message)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nowDatetime = _systemDate.GetSystemDate();
            message.CreatedTime = nowDatetime;
            message.UpdatedTime = nowDatetime;

            _context.MessagesWithTemplates.Add(message);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MessagesWithTemplate message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var target = await _context.MessagesWithTemplates.FindAsync(id);
            if (target == null)
                return NotFound();

            target.Message = message.Message;
            target.UpdatedTime = message.UpdatedTime;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var target = await _context.MessagesWithTemplates.FindAsync(id);
            if (target == null)
                return NotFound();

            target.DelFlg = "1";
            await _context.SaveChangesAsync();

            return Ok(id);
        }
    }
}
