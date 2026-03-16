namespace ProBook.Application.Interfaces
{
    public interface IReportService
    {
        Task<object> GetStatsAsync();
        Task<object> GetDistributionAsync();
    }
}