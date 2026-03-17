using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProBook.Application.Interfaces;

namespace ProBook.WebApi.Controllers
{
    /// <summary>
    /// Controlador para generar reportes y estadísticas del sistema ProBook.
    /// Proporciona métricas de ingresos, ocupación y distribución de habitaciones.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        /// <summary>
        /// Constructor que inyecta el servicio de reportes.
        /// </summary>
        /// <param name="reportService">Servicio de reportes implementado en Infrastructure.</param>
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Obtiene estadísticas generales del hotel.
        /// Incluye ingresos totales y tasa de ocupación.
        /// </summary>
        /// <returns>Objeto con totalRevenue y occupancyRate.</returns>
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _reportService.GetStatsAsync();
            return Ok(stats);
        }

        /// <summary>
        /// Obtiene la distribución de reservas por tipo de habitación.
        /// Útil para gráficos circulares mostrando Suite, Double, Individual.
        /// </summary>
        /// <returns>Lista de objetos con type y count.</returns>
        [HttpGet("distribution")]
        public async Task<IActionResult> GetDistribution()
        {
            var distribution = await _reportService.GetDistributionAsync();
            return Ok(distribution);
        }
    }
}