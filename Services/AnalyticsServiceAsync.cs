using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Statistics;
using ComputerComplectorWebAPI.Models.Statistics.Requests;
using ComputerComplectorWebAPI.Models.Data.Special;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ComputerComplectorWebAPI.Services
{
	/// <summary>
	/// Asynchronous analytic data service
	/// </summary>
	public class AnalyticsServiceAsync : IStatisticsServiceAsync
	{
		/// <summary>
		/// Analytic data DB context
		/// </summary>
		private ComponentsContext _dbContext;

		/// <summary>
		/// Logger
		/// </summary>
		private ILogger<AnalyticsServiceAsync> _logger;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dbContext">Analytic data DB context</param>
		/// <param name="componentsService">Components data DB context</param>
		/// <param name="logger">Logger</param>
		public AnalyticsServiceAsync(ComponentsContext dbContext, ILogger<AnalyticsServiceAsync> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task AddSelected(AddSelectionRequest request)
		{
			Selection selection = new Selection()
			{
				ComponentType = request.ComponentType,
				Date = request.Date,
				ElementID = request.ElementID
			};

			await _dbContext.Selections.AddAsync(selection);

			for (int i = 0; i < request.Properties.Count; i++)
			{
				var property = await _dbContext.Properties.Where(e => e.Component == request.ComponentType && e.Name == request.Properties.Keys.ElementAt(i)).FirstOrDefaultAsync();
				var value = property.Values.Where(e => e.Value == request.Properties[property.Name]).FirstOrDefault();
				SelectionProperties selectionProperties = new SelectionProperties()
				{
					PropertyValue = value,
					Selection = selection
				};

				await _dbContext.SelectionProperties.AddAsync(selectionProperties);
			}

			await _dbContext.SaveChangesAsync();
		}

		public async Task<IDictionary<int, int>> GetRecommended(GetRecommendationRequest request, int? amount = null)
		{
			await _dbContext.Properties.ToListAsync();
			await _dbContext.PropertyValues.ToListAsync();
			await _dbContext.SelectionProperties.ToListAsync();

			DateTime thresholdDate = DateTime.Today;
			switch (request.SelectionTimeSpan)
			{
				case SelectionTimeSpan.Year:
				{
					thresholdDate = DateTime.Now.Subtract(new TimeSpan(365, 0, 0, 0));
					break;
				}
				case SelectionTimeSpan.Month:
				{
					thresholdDate = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
					break;
				}
				case SelectionTimeSpan.Week:
				{
					thresholdDate = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
					break;
				}
				case SelectionTimeSpan.Day:
				{
					thresholdDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
					break;
				}
			}

			if (amount.HasValue && amount.Value > 0)
			{
				var list = await _dbContext.Selections.
					Where(e => e.ComponentType == request.ComponentType && //Select only component types that apply
						e.Properties.All(v => request.Properties.ContainsKey(v.PropertyValue.Property.Name) && //Check if all of the properties are in the selection record
						request.Properties[v.PropertyValue.Property.Name] == v.PropertyValue.Value) && //And properties values are equal
						e.Date >= thresholdDate). //Select only records with date newer than threshold
					GroupBy(e => e.ElementID). //Group by element ID
					Select(e => new { e.Key, Sum = e.Sum(v => 1) }). //Select only element ID and amount of selections
					OrderByDescending(e => e.Sum). //Order by descending to get top elements
					Take(amount.Value).
					ToDictionaryAsync(e => e.Key, e => e.Sum);
				return list;
			}
			else
			{
				var list = await _dbContext.Selections.
					Where(e => e.ComponentType == request.ComponentType && //Select only component types that apply
						e.Properties.All(v => request.Properties.ContainsKey(v.PropertyValue.Property.Name) && //Check if all of the properties are in the selection record
						request.Properties[v.PropertyValue.Property.Name] == v.PropertyValue.Value) && //And properties values are equal
						e.Date >= thresholdDate). //Select only records with date newer than threshold
					GroupBy(e => e.ElementID). //Group by element ID
					Select(e => new { e.Key, Sum = e.Sum(v => 1) }). //Select only element ID and amount of selections
					OrderByDescending(e => e.Sum). //Order by descending to get top elements
					ToDictionaryAsync(e => e.Key, e => e.Sum);
				return list;
			}
		}
	}
}
