using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProBook.Application.Interfaces;

namespace ProBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _reportService.GetStatsAsync();
            return Ok(stats);
        }

        [HttpGet("distribution")]
        public async Task<IActionResult> GetDistribution()
        {
            var distribution = await _reportService.GetDistributionAsync();
            return Ok(distribution);
        }
    }
}