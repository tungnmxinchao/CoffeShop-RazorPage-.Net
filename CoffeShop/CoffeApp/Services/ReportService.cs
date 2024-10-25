using CoffeApp.DTOs;
using CoffeApp.Interfaces;

namespace CoffeApp.Services
{
	public class ReportService
	{
		private readonly IReportRepository _reportRepository;

		public ReportService(IReportRepository reportRepository)
		{
			_reportRepository = reportRepository;
		}

		public List<DailyReport> GetDailyReports()
		{
			return _reportRepository.GetDailyReport();
		}

		public List<MonthlyReport> GetMonthlyReports()
		{
			return _reportRepository.GetMonthlyReport();
		}
	}
}
