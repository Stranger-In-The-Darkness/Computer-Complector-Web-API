using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Helpers;
using ComputerComplectorWebAPI.Models.Statistics;
using ComputerComplectorWebAPI.Models.Statistics.Requests;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Data.Special;

namespace ComputerComplectorWebAPI.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
		private IStatisticsServiceAsync _analyticsService;

		private AppSettings _appSettings;

		public StatisticsController(IStatisticsServiceAsync analyticsService, AppSettings settings)
		{
			_analyticsService = analyticsService;
			_appSettings = settings;
		}

		[HttpGet("{type}/recommendations")]
		public async Task<Dictionary<int, int>> GetRecomended(string type,
			[FromQuery(Name = "properties")]string[] props,
			[FromQuery(Name = "time-span")]string timeSpan,
			[FromQuery(Name = "amount")]int? amount)
		{
			GetRecommendationRequest request = new GetRecommendationRequest()
			{
				ComponentType = type,
				Properties = props.ToDictionary(e => e.Split('-')[0], e => e.Split('-')[1]),
				SelectionTimeSpan = (SelectionTimeSpan)Enum.Parse(typeof(SelectionTimeSpan), timeSpan)
			};
			return (await _analyticsService.GetRecommended(request, amount)).ToDictionary(e => e.Key, e => e.Value);
		}
	}
}