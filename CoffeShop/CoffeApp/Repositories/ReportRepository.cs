using CoffeApp.DTOs;
using CoffeApp.Interfaces;
using CoffeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeApp.Repositories
{
	public class ReportRepository : IReportRepository
	{
		public List<DailyReport> GetDailyReport()
		{

			var dailyReport = PRN221_CoffeShopContext.Ins.Orders
				.Where(order => order.OrderDate.HasValue)
				.GroupBy(order => order.OrderDate.Value.Date)
				.Select(g => new DailyReport
				{
					Date = g.Key,
					OrderCount = g.Count(),
					TotalRevenue = g.Sum(order => order.TotalAmount)
				})
				.OrderBy(report => report.Date)
				.ToList();

			return dailyReport;
		}

		public List<MonthlyReport> GetMonthlyReport()
		{
			var monthlyReport = PRN221_CoffeShopContext.Ins.Orders
				.Where(order => order.OrderDate.HasValue)
				.GroupBy(order => new { order.OrderDate.Value.Year, order.OrderDate.Value.Month })
				.Select(g => new MonthlyReport
				{
					Year = g.Key.Year,
					Month = g.Key.Month,
					OrderCount = g.Count(),
					TotalRevenue = g.Sum(order => order.TotalAmount)
				})
				.OrderBy(report => report.Year)
				.ThenBy(report => report.Month)
				.ToList();
			return monthlyReport;
		}
	}
}
