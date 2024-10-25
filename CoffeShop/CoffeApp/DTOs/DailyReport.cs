namespace CoffeApp.DTOs
{
	public class DailyReport
	{
		public DateTime Date { get; set; }
		public int OrderCount { get; set; }
		public decimal TotalRevenue { get; set; }
	}
}
