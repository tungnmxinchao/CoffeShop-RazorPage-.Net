namespace CoffeApp.DTOs
{
	public class MonthlyReport
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public int OrderCount { get; set; }
		public decimal TotalRevenue { get; set; }
	}
}
