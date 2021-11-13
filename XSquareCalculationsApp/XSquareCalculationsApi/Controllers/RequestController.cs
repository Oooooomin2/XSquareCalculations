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
    public class RequestController : Controller
    {
        /*private readonly XSquareCalculationContext _context;
        private readonly ISystemDate _systemDate;

        public RequestController(XSquareCalculationContext context, ISystemDate systemDate)
        {
            _context = context;
            _systemDate = systemDate;
        }*/

        [HttpGet]
        public IEnumerable<string> GetRequests()
        {
            return new List<string>() { "test1" };
        }

        /*[HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequestDetail(int id)
        {
            var detail = await _context.Requests.SingleOrDefaultAsync(o => o.RequestId == id);
            if(detail == null)
            {
                return NotFound();
            }

            return detail;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Request request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nowDatetime = _systemDate.GetSystemDate();
            request.CreatedTime = nowDatetime;
            request.UpdatedTime = nowDatetime;

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var target = await _context.Requests.FindAsync(id);
            if (target == null)
                return NotFound();

            target.DelFlg = "1";
            await _context.SaveChangesAsync();

            return Ok(id);
        }*/
    }
}
