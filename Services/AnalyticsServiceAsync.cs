using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Analytics;
using ComputerComplectorWebAPI.Models.Analytics.Requests;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ComputerComplectorWebAPI.Services
{
	/// <summary>
	/// Asynchronous analytic data service
	/// </summary>
	public class AnalyticsServiceAsync : IAnalyticsServiceAsync
	{
		/// <summary>
		/// Analytic data DB context
		/// </summary>
		private StatisticsContext _dbContext;

		/// <summary>
		/// Components data DB context
		/// </summary>
		private IComponentsServiceAsync _componentsService;

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
		public AnalyticsServiceAsync(StatisticsContext dbContext, IComponentsServiceAsync componentsService, ILogger<AnalyticsServiceAsync> logger)
		{
			_dbContext = dbContext;
			_componentsService = componentsService;
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

			for (int i = 0; i< request.Properties.Count; i++)
			{
				var property = await _dbContext.Properties.Where(e => e.ComponentType == request.ComponentType && e.Name == request.Properties.Keys.ElementAt(i)).FirstOrDefaultAsync();
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

		/// <summary>
		/// Return properties of specified component type
		/// </summary>
		/// <param name="componentType">Component type</param>
		/// <returns><see cref="IEnumerable{T}"/> of properties</returns>
		public async Task<IEnumerable<string>> GetProperties(string componentType)
		{
			return await _dbContext.Properties.Where(e => e.ComponentType == componentType).Select(e => e.Name).ToListAsync();
		}

		/// <summary>
		/// Return property values of specified property of specified component type
		/// </summary>
		/// <param name="componentType">Component type</param>
		/// <param name="property">Property name</param>
		/// <returns><see cref="IEnumerable{T}"/> of values of properties</returns>
		public async Task<IEnumerable<string>> GetPropertyValues(string componentType, string property)
		{
			return (await _dbContext.Properties.Where(e => e.ComponentType == componentType && e.Name == property).FirstOrDefaultAsync()).Values.Select(e => e.Value).ToArray();
		}

		/// <summary>
		/// Add property to component type
		/// </summary>
		/// <param name="componentType">Component type</param>
		/// <param name="name">New property name</param>
		/// <returns>Updated <see cref="IEnumerable{T}"/> of properties</returns>
		public async Task<IEnumerable<string>> AddProperty(string componentType, string name)
		{
			Property property = new Property()
			{
				ComponentType = componentType,
				Name = name
			};

			try
			{
				await _dbContext.Properties.AddAsync(property);
				await _dbContext.SaveChangesAsync();
				_logger.LogInformation("Added new property: {0} (ID), {1} (Name)", _dbContext.Properties.FirstOrDefault(e => e.ComponentType == componentType & e.Name == name).ID, name);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Cannot add new property. Component type: {0}. Property name: {1}.", componentType, property);
				throw;
			}

			return await GetProperties(componentType);
		}

		/// <summary>
		/// Add new value to the property of component type
		/// </summary>
		/// <param name="componentType">Component type</param>
		/// <param name="property">Property to add value for</param>
		/// <param name="value">New property value</param>
		/// <returns>Updated <see cref="IEnumerable{T}"/> of values of properties</returns>
		public async Task<IEnumerable<string>> AddPropertyValue(string componentType, string property, string value)
		{
			var prop = await _dbContext.Properties.FirstOrDefaultAsync(e => e.Name == property);

			var val = new PropertyValue()
			{
				Property = prop,
				PropertyID = prop.ID,
				Value = value
			};

			try
			{
				await _dbContext.PropertyValues.AddAsync(val);
				await _dbContext.SaveChangesAsync();
				_logger.LogInformation("Added newproperty value: {0} (ID), {1} (Property), {2} (Value)", _dbContext.PropertyValues.FirstOrDefault(e => e.Property == prop).ID, prop.Name, value);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Cannot add new property value. Component type: {0}. Property name: {1}. Porperty value: {2}.", componentType, property, value);
				throw;
			}

			return await GetPropertyValues(componentType, property);
		}
	}
}
