using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Helpers;
using ComputerComplectorWebAPI.Models.Analytics;
using ComputerComplectorWebAPI.Models.Analytics.Requests;
using ComputerComplectorWebAPI.Interfaces;

namespace ComputerComplectorWebAPI.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
		private IAnalyticsServiceAsync _analyticsService;

		private AppSettings _appSettings;

		public AnalyticsController(IAnalyticsServiceAsync analyticsService, AppSettings settings)
		{
			_analyticsService = analyticsService;
			_appSettings = settings;
		}

		[AllowAnonymous]
		[HttpGet, Route("{componentType}")]
		public async Task<IDictionary<int, int>> GetRecommended(
			string componentType, 
			[FromQuery]IDictionary<string, string> properties, 
			[FromQuery]int? amount)
		{
			var request = new GetRecommendationRequest()
			{
				ComponentType = componentType,
				SelectionTimeSpan = _appSettings.SelectionTimeSpan,
				Properties = properties
			};

			return await _analyticsService.GetRecommended(request, amount);
		}

		[AllowAnonymous]
		[HttpPost, Route("{componentType}")]
		public async Task AddSelection(
			string componentType,
			[FromQuery]IDictionary<string, string> properties,
			[FromQuery(Name = "element")]int elementID,
			[FromQuery]DateTime date)
		{
			var request = new AddSelectionRequest()
			{
				ComponentType = componentType,
				Date = date,
				ElementID = elementID,
				Properties = properties
			};

			await _analyticsService.AddSelected(request);
		}

		[AllowAnonymous]
		[HttpGet, Route("{componentType}/properties")]
		public async Task<IEnumerable<string>> GetProperties(string componentType)
		{
			return await _analyticsService.GetProperties(componentType);
		}

		[AllowAnonymous]
		[HttpGet, Route("{componentType}/properties/values")]
		public async Task<IEnumerable<string>> GetPropertyValues(string componentType,
			[FromQuery]string property)
		{
			return await _analyticsService.GetPropertyValues(componentType, property);
		}

		[HttpPost, Route("{componentType}/properties")]
		public async Task AddProperties(
			string componentType,
			[FromQuery] string[] propertyName)
		{
			for (int i = 0; i < propertyName.Length; i++)
			{
				await _analyticsService.AddProperty(componentType, propertyName[i]);
			}
		}

		[HttpPost, Route("{componentType}/properties/values")]
		public async Task AddPropertyValues(
			string componentType,
			[FromQuery] string propertyName,
			[FromQuery] string[] propertyValues)
		{
			for (int i = 0; i < propertyValues.Length; i++)
			{
				await _analyticsService.AddPropertyValue(componentType, propertyName, propertyValues[i]);
			}
		}
	}
}