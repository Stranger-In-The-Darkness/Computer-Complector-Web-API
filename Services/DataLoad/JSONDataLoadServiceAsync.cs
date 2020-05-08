using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Add;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ComputerComplectorWebAPI.Services
{
	/// <summary>
	/// Loads data from JSON string
	/// </summary>
	public class JSONDataLoadServiceAsync : IDataLoadServiceAsync
	{
		private ILogger<JSONDataLoadServiceAsync> _logger;

		private IComponentsServiceAsync _components;

		public JSONDataLoadServiceAsync(IComponentsServiceAsync service, ILogger<JSONDataLoadServiceAsync> logger)
		{
			_components = service;
			_logger = logger;
		}

		public async Task<IEnumerable<Body>> LoadBodies(string data)
		{
			IEnumerable<Body> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<Body>>(data);

				foreach(Body b in result)
				{
					try
					{
						await _components.AddBody(new AddBodyRequest(b));
					}
					catch
					{
						_logger.LogError("Cannot add new Body: {0}", b);
					}
				}
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize Body. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<Charger>> LoadChargers(string data)
		{
			IEnumerable<Charger> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<Charger>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize Charger. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<Cooler>> LoadCoolers(string data)
		{
			IEnumerable<Cooler> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<Cooler>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize Cooler. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<CPU>> LoadCPUs(string data)
		{
			IEnumerable<CPU> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<CPU>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize CPU. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<HDD>> LoadHDDs(string data)
		{
			IEnumerable<HDD> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<HDD>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize HDD. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<Motherboard>> LoadMotherboards(string data)
		{
			IEnumerable<Motherboard> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<Motherboard>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize Motherboard. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<RAM>> LoadRAMs(string data)
		{
			IEnumerable<RAM> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<RAM>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize RAM. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<SSD>> LoadSSDs(string data)
		{
			IEnumerable<SSD> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<SSD>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize SSD. Exception: {0}", exception);
			}
			return result;
		}

		public async Task<IEnumerable<Videocard>> LoadVideocards(string data)
		{
			IEnumerable<Videocard> result = null;
			try
			{
				result = JsonConvert.DeserializeObject<IEnumerable<Videocard>>(data);
				///TODO
				//Insert validation code
			}
			catch (JsonSerializationException exception)
			{
				_logger.LogError("Cannot deserialize Videocard. Exception: {0}", exception);
			}
			return result;
		}

		public static T LoadObjects<T>(string data)
		{
			T t = default(T);

			t = JsonConvert.DeserializeObject<T>(data);

			return t;
		}
	}
}
