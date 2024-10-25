using CoffeApp.DTOs;

namespace CoffeApp.Interfaces
{
	public interface IReportRepository
	{
		public List<DailyReport> GetDailyReport();

		public List<MonthlyReport> GetMonthlyReport();
	}
}
