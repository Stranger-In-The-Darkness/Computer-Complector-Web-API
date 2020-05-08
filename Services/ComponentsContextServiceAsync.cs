using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Add;
using ComputerComplectorWebAPI.Models.Requests.Get;
using ComputerComplectorWebAPI.Models.Requests.Remove;
using ComputerComplectorWebAPI.Models.Requests.Update;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using NLua;

using Entity = ComputerComplectorWebAPI.EntityFramework.Models.Data;

namespace ComputerComplectorWebAPI.Services
{
	/// <summary>
	/// Asynchronous Entity Framework component service
	/// </summary>
	public class ComponentsContextServiceAsync : IComponentsServiceAsync
	{
		/// <summary>
		/// DB Context for components data
		/// </summary>
		private ComponentsContext _dbContext;

		/// <summary>
		/// Cache
		/// </summary>
		private IMemoryCache _cache;

		/// <summary>
		/// Logger
		/// </summary>
		private ILogger<ComponentsContextServiceAsync> _logger;

		/// <summary>
		/// Main Lua object
		/// </summary>
		private Lua _lua = new Lua();

		/// <summary>
		/// Compatibility Lua metatable
		/// </summary>
		private LuaTable _compatibility;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dbContext">Components DB context</param>
		/// <param name="cache">Cache</param>
		/// <param name="logger">Logger</param>
		public ComponentsContextServiceAsync(ComponentsContext dbContext, IMemoryCache cache, ILogger<ComponentsContextServiceAsync> logger)
		{
			_dbContext = dbContext;

			_cache = cache;

			_logger = logger;

			_compatibility = _lua.DoFile(@"Scripts\Compatibility.lua")[0] as LuaTable;
		}

		#region GET

		#region GET BY ID
		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="Body"/>. Null if incorrect ID</returns>
		public async Task<Body> GetBody(int id = -1)
		{
			var bodyFormfactor = await _dbContext.BodyFormfactor.Where(e => e.BodyID == id).ToListAsync();
			_logger.LogInformation("Loaded BodyFormfactor records: {0}", bodyFormfactor.Count);
			var body = await _dbContext.Bodies.FirstOrDefaultAsync(e => e.ID == id);
			_logger.LogInformation("Loaded Body records: {0}", body == null ? 0 : 1);
			return body;
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="Charger"/>. Null if incorrect ID</returns>
		public async Task<Charger> GetCharger(int id = -1)
		{
			var charger = await _dbContext.Chargers.FirstOrDefaultAsync(e => e.ID == id);
			_logger.LogInformation("Loaded Charger records: {0}", charger == null ? 0 : 1);
			return charger;
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="Cooler"/>. Null if incorrect ID</returns>
		public async Task<Cooler> GetCooler(int id = -1)
		{
			await _dbContext.CoolerSockets.LoadAsync();
			return await _dbContext.Coolers.FirstOrDefaultAsync(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="CPU"/>. Null if incorrect ID</returns>
		public async Task<CPU> GetCPU(int id = -1)
		{
			return await _dbContext.CPUs.FirstOrDefaultAsync(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="HDD"/>. Null if incorrect ID</returns>
		public async Task<HDD> GetHDD(int id = -1)
		{
			await _dbContext.HDDInterfaces.LoadAsync();
			return await _dbContext.HDDs.FirstOrDefaultAsync(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="Motherboard"/>. Null if incorrect ID</returns>
		public async Task<Motherboard> GetMotherboard(int id = -1)
		{
			await _dbContext.MotherboardSlots.LoadAsync();
			return await _dbContext.Motherboards.FirstOrDefaultAsync(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="RAM"/>. Null if incorrect ID</returns>
		public async Task<RAM> GetRAM(int id = -1)
		{
			return await _dbContext.RAMs.FirstOrDefaultAsync(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="SSD"/>. Null if incorrect ID</returns>
		public async Task<SSD> GetSSD(int id = -1)
		{
			await _dbContext.SSDInterfaces.LoadAsync();
			return await _dbContext.SSDs.FirstOrDefaultAsync(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns><see cref="Videocard"/>. Null if incorrect ID</returns>
		public async Task<Videocard> GetVideocard(int id = -1)
		{
			await _dbContext.VideocardConnectors.LoadAsync();
			return await _dbContext.Videocards.FirstOrDefaultAsync(e => e.ID == id);
		}
		#endregion

		#region GET ALL
		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{Body}"/> of <see cref="Body"/></returns>
		public async Task<IEnumerable<Body>> GetBodies()
		{
			await _dbContext.BodyFormfactor.LoadAsync();
			return (await _dbContext.Bodies.ToListAsync()).Select(e => (Body)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{Charger}"/> of <see cref="Charger"/></returns>
		public async Task<IEnumerable<Charger>> GetChargers()
		{
			return (await _dbContext.Chargers.ToListAsync()).Select(e => (Charger)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{Cooler}"/> of <see cref="Cooler"/></returns>
		public async Task<IEnumerable<Cooler>> GetCoolers()
		{
			await _dbContext.CoolerSockets.LoadAsync();
			return (await _dbContext.Coolers.ToListAsync()).Select(e => (Cooler)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{CPU}"/> of <see cref="CPU"/></returns>
		public async Task<IEnumerable<CPU>> GetCPUs()
		{
			return (await _dbContext.CPUs.ToListAsync()).Select(e => (CPU)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{HDD}"/> of <see cref="HDD"/></returns>
		public async Task<IEnumerable<HDD>> GetHDDs()
		{
			await _dbContext.HDDInterfaces.LoadAsync();
			return (await _dbContext.HDDs.ToListAsync()).Select(e => (HDD)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{Motherboard}"/> of <see cref="Motherboard"/></returns>
		public async Task<IEnumerable<Motherboard>> GetMotherboards()
		{
			await _dbContext.MotherboardSlots.LoadAsync();
			return (await _dbContext.Motherboards.ToListAsync()).Select(e => (Motherboard)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{RAM}"/> of <see cref="RAM"/></returns>
		public async Task<IEnumerable<RAM>> GetRAMs()
		{
			return (await _dbContext.RAMs.ToListAsync()).Select(e => (RAM)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{SSD}"/> of <see cref="SSD"/></returns>
		public async Task<IEnumerable<SSD>> GetSSDs()
		{
			await _dbContext.SSDInterfaces.LoadAsync();
			return (await _dbContext.SSDs.ToListAsync()).Select(e => (SSD)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{Videocard}"/> of <see cref="Videocard"/></returns>
		public async Task<IEnumerable<Videocard>> GetVideocards()
		{
			await _dbContext.VideocardConnectors.LoadAsync();
			return (await _dbContext.Videocards.ToListAsync()).Select(e => (Videocard)e).ToList();
		}
		#endregion

		#region GET BY REQUEST
		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<Body>> GetBodies(GetBodiesRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetBodies();
			}

			var bodies = await GetBodies();
			bodies = bodies.Where(e => request.BuildInCharger?.Any() ?? false ? request.BuildInCharger.ToList().Contains(e.BuildInCharger) : true).
				Where(e => request.ChargerPower?.Any() ?? false ? request.ChargerPower.Any(p => (e.ChargerPower >= int.Parse(p.Split('-')[0]) && 
						e.ChargerPower <= int.Parse(p.Split('-')[1]))) : true).
				Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Formfactor?.Any() ?? false ? e.Formfactor.Any(f => request.Formfactor.Contains(f)) : true).
				Where(e => request.Type?.Any() ?? false ? request.Type.Contains(e.Type) : true).
				Where(e => request.Usb3Ports?.Any() ?? false ? request.Usb3Ports.Contains(e.USB3Amount.ToString()) : true).
				ToList();

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await GetMotherboard(request.SelectedMotherboard ?? -1);
				bodies = 
					bodies.Select(e => 
					{
						e.Compatible = CheckCompatibility("body", "motherboard", e, motherboard, out string comp);
						if (comp != "")
						{
							e.Incompatible = new Dictionary<string, string>
							{
								{ "motherboard", comp }
							};
						}
						return e;
					}).ToList();
			}

			if (request.SelectedVideocard != null)
			{
				var videocard = await GetVideocard(request.SelectedVideocard ?? -1);
				bodies = 
					bodies.Select(e =>
					{
						e.Compatible &= CheckCompatibility("body", "videocard", e, videocard, out string comp);
						if (comp != "")
						{
							if (e.Incompatible == null)
							{
								e.Incompatible = new Dictionary<string, string>();
							}
							e.Incompatible.Add("videocard", comp);
						}
						return e;
					}).ToList();
			}

			return bodies;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<Charger>> GetChargers(GetChargersRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetChargers();
			}

			var chargers = await GetChargers();

			chargers = chargers.Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.ConnectorType?.Any() ?? false ? request.ConnectorType.Contains(e.ConnectorType) : true).
				Where(e => request.IDEAmount?.Any() ?? false ? request.IDEAmount.Contains(e.IDEAmount.ToString()) : true).
				Where(e => request.MotherboardConnector?.Any() ?? false ? request.MotherboardConnector.Contains(e.MotherboardConnector) : true).
				Where(e => request.Power?.Any() ?? false ? request.Power.Contains(e.Power.ToString()) : true).
				Where(e => request.SATAAmount?.Any() ?? false ? request.SATAAmount.Contains(e.SATAAmount.ToString()) : true).
				Where(e => request.Series?.Any() ?? false ? request.Series.Contains(e.Series) : true).
				Where(e => request.Sertificate?.Any() ?? false ? request.Sertificate.Contains(e.Sertificate80Plus) : true).
				Where(e => request.VideoConnectorsAmount?.Any() ?? false ? request.VideoConnectorsAmount.Contains(e.VideoConnectorsAmount) : true).
				ToList();

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await GetMotherboard(request.SelectedMotherboard ?? -1);
				chargers = chargers.Select(e =>
				{
					e.Compatible = CheckCompatibility("charger", "motherboard", e, motherboard, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "motherboard", comp }
						};
					}
					return e;
				}).ToList();
			}

			if (request.SelectedVideocard != null)
			{
				var videocard = await GetVideocard(request.SelectedVideocard ?? -1);
				chargers = chargers.Select(e =>
				{
					e.Compatible &= CheckCompatibility("charger", "videocard", e, videocard, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("videocard", comp);
					}
					return e;
				}).ToList();
			}

			return chargers;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<Cooler>> GetCoolers(GetCoolersRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetCoolers();
			}

			var coolers = await GetCoolers();

			coolers = coolers.Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Material?.Any() ?? false ? request.Material.Contains(e.Material) : true).
				Where(e => request.Purpose?.Any() ?? false ? request.Purpose.Contains(e.Purpose) : true).
				Where(e => request.Socket?.Any() ?? false ? e.Socket.All(s => request.Socket.Contains(s)) : true).
				Where(e => request.TurnAdj?.Any() ?? false ? request.TurnAdj.Contains(e.TurnAdj.ToString()) : true).
				Where(e => request.Type?.Any() ?? false ? request.Type.Contains(e.Type) : true).
				Where(e => request.VentDiam?.Any() ?? false ? request.VentDiam.Contains(e.VentDiam.ToString()) : true).
				ToList();

			if (request.SelectedCpu != null)
			{
				var cpu = await GetCPU(request.SelectedCpu ?? -1);
				coolers = coolers.Select(e =>
				{

					e.Compatible = CheckCompatibility("cooler", "cpu", e, cpu, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "cpu", comp }
						};
					}
					return e;
				}).ToList();
			}

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await GetMotherboard(request.SelectedMotherboard ?? -1);
				coolers = coolers.Select(e =>
				{

					e.Compatible &= CheckCompatibility("cooler", "motherboard", e, motherboard, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("motherboard", comp);
					}
					return e;
				}).ToList();
			}

			return coolers;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<CPU>> GetCPUs(GetCPUsRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetCPUs();
			}

			var cpus = await GetCPUs();

			cpus = cpus.Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Core?.Any() ?? false ? request.Core.Contains(e.Core) : true).
				Where(e => request.CoresAmount?.Any() ?? false ? request.CoresAmount.Contains(e.CoresAmount) : true).
				Where(e => request.DeliveryType?.Any() ?? false ? request.DeliveryType.Contains(e.DeliveryType) : true).
				Where(e => request.IntegratedGraphics?.Any() ?? false ? request.IntegratedGraphics.Contains(e.IntegratedGraphics) : true).
				Where(e => request.Overclocking?.Any() ?? false ? request.Overclocking.Contains(e.Overcloacking) : true).
				Where(e => request.Series?.Any() ?? false ? request.Series.Contains(e.Series) : true).
				Where(e => request.Socket?.Any() ?? false ? request.Socket.Contains(e.Socket) : true).
				Where(e => request.ThreadsAmount?.Any() ?? false ? request.ThreadsAmount.Contains(e.ThreadsAmount) : true).
				ToList();

			if (request.SelectedCooler != null)
			{
				var cooler = await GetCooler(request.SelectedCooler ?? -1);
				cpus = cpus.Select(e =>
				{

					e.Compatible = CheckCompatibility("cpu", "cooler", e, cooler, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "cooler", comp }
						};
					}
					return e;
				}).ToList();
			}

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await GetMotherboard(request.SelectedMotherboard ?? -1);
				cpus = cpus.Select(e =>
				{

					e.Compatible &= CheckCompatibility("cpu", "motherboard", e, motherboard, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("motherboard", comp);
					}
					return e;
				}).ToList();
			}

			return cpus;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<HDD>> GetHDDs(GetHDDsRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetHDDs();
			}

			var hdds = await GetHDDs();

			hdds = hdds.Where(e => request.BufferVolume?.Any() ?? false ? request.BufferVolume.Contains(e.BufferVolume) : true).
				Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Formfactor?.Any() ?? false ? request.Formfactor.Contains(e.Formfactor) : true).
				Where(e => request.Interface?.Any() ?? false ? e.Interface.All(i => request.Interface.Contains(i)) : true).
				Where(e => request.Series?.Any() ?? false ? request.Series.Contains(e.Series) : true).
				Where(e => request.Speed?.Any() ?? false ? request.Speed.Contains(e.Speed) : true).
				Where(e => request.Volume?.Any() ?? false ? request.Volume.Contains(e.Capacity.ToString()) : true).
				ToList();

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await GetMotherboard(request.SelectedMotherboard ?? -1);
				hdds = hdds.Select(e =>
				{

					e.Compatible = CheckCompatibility("hdd", "motherboard", e, motherboard, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "motherboard", comp }
						};
					}
					return e;
				}).ToList();
			}

			return hdds;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<Motherboard>> GetMotherboards(GetMotherboardsRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetMotherboards();
			}

			var motherboards = await GetMotherboards();

			motherboards = motherboards.Where(e => request.Chipset?.Any() ?? false ? request.Chipset.Contains(e.Chipset) : true).
				Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Formfactor?.Any() ?? false ? request.Formfactor.Contains(e.Formfactor) : true).
				Where(e => request.MaxMemory?.Any() ?? false ? request.MaxMemory.Contains(e.MaxMemory) : true).
				Where(e => request.Memory?.Any() ?? false ? request.Memory.Contains(e.MemoryType) : true).
				Where(e => request.MemoryChanelsAmount?.Any() ?? false ? request.MemoryChanelsAmount.Contains(e.MemoryChanelsAmount) : true).
				Where(e => request.MemorySlotsAmount?.Any() ?? false ? request.MemorySlotsAmount.Contains(e.MemorySlotsAmount) : true).
				Where(e => request.RAMMaxFreq?.Any() ?? false ? request.RAMMaxFreq.Contains(e.RAMMaxFreq.ToString()) : true).
				Where(e => request.Series?.Any() ?? false ? request.Series.Contains(e.Series) : true).
				Where(e => request.Slots?.Any() ?? false ? e.Slots.Any(s => request.Slots.Contains(s)) : true).
				Where(e => request.Socket?.Any() ?? false ? request.Socket.Contains(e.Socket) : true).
				ToList();

			if (request.SelectedBody != null)
			{
				var body = await GetBody(request.SelectedBody ?? -1);
				motherboards = motherboards.Select(e =>
				{

					e.Compatible = CheckCompatibility("motherboard", "body", e, body, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "body", comp }
						};
					}
					return e;
				}).ToList();
			}

			if (request.SelectedCharger != null)
			{
				var charger = await GetCharger(request.SelectedCharger ?? -1);
				motherboards = motherboards.Select(e =>
				{

					e.Compatible &= CheckCompatibility("motherboard", "charger", e, charger, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("charger", comp);
					}
					return e;
				}).ToList();
			}

			if (request.SelectedCooler != null)
			{
				var cooler = await GetCooler(request.SelectedCooler ?? -1);
				motherboards = motherboards.Select(e =>
				{
					e.Compatible &= CheckCompatibility("motherboard", "cooler", e, cooler, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("cooler", comp);
					}
					return e;
				}).ToList();
			}

			if (request.SelectedCpu != null)
			{
				var cpu = await GetCPU(request.SelectedCpu ?? -1);
				motherboards = motherboards.Select(e =>
				{
					e.Compatible &= CheckCompatibility("motherboard", "cpu", e, cpu, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("cpu", comp);
					}
					return e;
				}).ToList();
			}

			if (request.SelectedHdd != null)
			{
				var hdd = await GetHDD(request.SelectedHdd ?? -1);
				motherboards = motherboards.Select(e =>
				{
					e.Compatible &= CheckCompatibility("motherboard", "hdd", e, hdd, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("hdd", comp);
					}
					return e;
				}).ToList();
			}

			if (request.SelectedRam != null)
			{
				var ram = await GetRAM(request.SelectedRam ?? -1);
				motherboards = motherboards.Select(e =>
				{
					e.Compatible &= CheckCompatibility("motherboard", "ram", e, ram, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("ram", comp);
					}
					return e;
				}).ToList();
			}

			if (request.SelectedSsd != null)
			{
				var ssd = await GetSSD(request.SelectedSsd ?? -1);
				motherboards = motherboards.Select(e =>
				{
					e.Compatible &= CheckCompatibility("motherboard", "ssd", e, ssd, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("ssd", comp);
					}
					return e;
				}).ToList();
			}

			return motherboards;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<RAM>> GetRAMs(GetRAMsRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetRAMs();
			}

			var rams = await GetRAMs();

			rams = rams.Where(e => request.CL?.Any() ?? false ? request.CL.Contains(e.CL) : true).
				Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Freq?.Any() ?? false ? request.Freq.Contains(e.Freq) : true).
				Where(e => request.Memory?.Any() ?? false ? request.Memory.Contains(e.MemoryType) : true).
				Where(e => request.ModuleAmount?.Any() ?? false ? request.ModuleAmount.Contains(e.ModuleAmount) : true).
				Where(e => request.Series?.Any() ?? false ? request.Series.Contains(e.Series) : true).
				Where(e => request.Type?.Any() ?? false ? request.Type.Contains(e.Purpose) : true).
				Where(e => request.Volume?.Any() ?? false ? request.Volume.Contains(e.Volume) : true).
				ToList();

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await GetMotherboard(request.SelectedMotherboard ?? -1);
				rams = rams.Select(e =>
				{
					e.Compatible = CheckCompatibility("ram", "motherboard", e, motherboard, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "motherboard", comp }
						};
					}
					return e;
				}).ToList();
			}

			return rams;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<SSD>> GetSSDs(GetSSDsRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetSSDs();
			}

			var ssds = await GetSSDs();

			ssds = ssds.Where(e => request.Capacity?.Any() ?? false ? request.Capacity.Contains(e.Capacity.ToString()) : true).
				Where(e => request.CellType?.Any() ?? false ? request.CellType.Contains(e.CellType) : true).
				Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Formfactor?.Any() ?? false ? request.Formfactor.Contains(e.Formfactor) : true).
				Where(e => request.Interface?.Any() ?? false ? e.Interface.Any(i => request.Interface.Contains(i)) : true).
				Where(e => request.Series?.Any() ?? false ? request.Series.Contains(e.Series) : true).
				ToList();

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await GetMotherboard(request.SelectedMotherboard ?? -1);
				ssds = ssds.Select(e =>
				{

					e.Compatible = CheckCompatibility("ssd", "motherboard", e, motherboard, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "motherboard", comp }
						};
					}
					return e;
				}).ToList();
			}

			return ssds;
		}

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="request">Selection filters</param>
		/// <returns></returns>
		public async Task<IEnumerable<Videocard>> GetVideocards(GetVideocardsRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetVideocards();
			}

			var videocards = await GetVideocards();

			videocards = videocards.Where(e => request.Capacity?.Any() ?? false ? request.Capacity.Contains(e.Capacity) : true).
				Where(e => request.Company?.Any() ?? false ? request.Company.Contains(e.Company) : true).
				Where(e => request.Connector?.Any() ?? false ? e.Connectors.Any(c => request.Connector.Contains(c)) : true).
				Where(e => request.Family?.Any() ?? false ? request.Family.Contains(e.Family) : true).
				Where(e => request.Proccessor?.Any() ?? false ? request.Proccessor.Contains(e.Proccessor) : true).
				Where(e => request.Series?.Any() ?? false ? request.Series.Contains(e.Series) : true).
				Where(e => request.VRAM?.Any() ?? false ? request.VRAM.Contains(e.VRAM.ToString()) : true).
				ToList();

			if (request.SelectedBody != null)
			{
				var body = await GetBody(request.SelectedBody ?? -1);
				videocards = videocards.Select(e =>
				{

					e.Compatible = CheckCompatibility("videocard", "body", e, body, out string comp);

					if (comp != "")
					{
						e.Incompatible = new Dictionary<string, string>
						{
							{ "body", comp }
						};
					}
					return e;
				}).ToList();
			}
			if (request.SelectedCharger != null)
			{
				var charger = await GetCharger(request.SelectedCharger ?? -1);
				videocards = videocards.Select(e =>
				{
					e.Compatible &= CheckCompatibility("videocard", "charger", e, charger, out string comp);

					if (comp != "")
					{
						if (e.Incompatible == null)
						{
							e.Incompatible = new Dictionary<string, string>();
						}
						e.Incompatible.Add("charger", comp);
					}
					return e;
				}).ToList();
			}

			return videocards;
		}
		#endregion

		#region GET PROPERTIES

		public async Task<IEnumerable<Property>> GetProperties(string component)
		{
			await _dbContext.Properties.LoadAsync();
			await _dbContext.PropertyValues.LoadAsync();
			return (await _dbContext.Properties.Select(e => e).Where(e => e.Component == component).ToListAsync()).Select(e => (Property)e).ToList();
		}

		#endregion

		#region GET DESCRIPTIONS

		public IDictionary<string, string> GetDescription(string component)
		{
			if (!_cache.TryGetValue($"{component}-descr", out object res))
			{
				System.Reflection.PropertyInfo[] prop = null;
				switch (component.ToLower())
				{
					case "body":
						prop = typeof(Body).GetProperties();
						break;
					case "cooler":
						prop = typeof(Cooler).GetProperties();
						break;
					case "charger":
						prop = typeof(Charger).GetProperties();
						break;
					case "cpu":
						prop = typeof(CPU).GetProperties();
						break;
					case "hdd":
						prop = typeof(HDD).GetProperties();
						break;
					case "motherboard":
						prop = typeof(Motherboard).GetProperties();
						break;
					case "ram":
						prop = typeof(RAM).GetProperties();
						break;
					case "ssd":
						prop = typeof(SSD).GetProperties();
						break;
					case "videocard":
						prop = typeof(Videocard).GetProperties();
						break;
				}

				res = prop.Select(e => new { e.Name, Type = e.PropertyType.Name }).ToDictionary(e => e.Name, e => e.Type);
				_cache.Set($"{component}-descr", res);
			}
			return (IDictionary<string, string>)res;
		}
		#endregion

		#endregion

		#region ADD

		#region ADD COMPONENT
		/// <summary>
		/// Add new <see cref="Body"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{Body}"/> of <see cref="Body"/></returns>
		public async Task<IEnumerable<Body>> AddBody(AddBodyRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.Bodies.AddAsync(request.Body);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new Body element: {0} (ID)", 
				_dbContext.Bodies.FirstAsync(e => e.Title == request.Body.Title).Id);
			return await GetBodies();
		}

		/// <summary>
		/// Add new <see cref="Charger"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{Charger}"/> of <see cref="Charger"/></returns>
		public async Task<IEnumerable<Charger>> AddCharger(AddChargerRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.Chargers.AddAsync(request.Charger);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new Charger element: {0} (ID)",
				_dbContext.Chargers.FirstAsync(e => e.Title == request.Charger.Title).Id);
			return await GetChargers();
		}

		/// <summary>
		/// Add new <see cref="Cooler"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{Cooler}"/> of <see cref="Cooler"/></returns>
		public async Task<IEnumerable<Cooler>> AddCooler(AddCoolerRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			var cooler = (Entity.Cooler)request.Cooler;
			var sockets = cooler.Socket.ToList();
			cooler.Socket = null;
			await _dbContext.Coolers.AddAsync(cooler);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new Cooler element: {0} (ID)",
				_dbContext.Coolers.FirstAsync(e => e.Title == request.Cooler.Title).Id);

			var nList = await _dbContext.Coolers.ToListAsync();
			var el = nList.First(e => e.Title == cooler.Title);
			for (int i = 0; i<sockets.Count; i++)
			{
				sockets[i].CoolerID = el.ID;
			}
			await _dbContext.CoolerSockets.AddRangeAsync(sockets);
			await _dbContext.SaveChangesAsync();
			for (int i = 0; i<sockets.Count; i++)
			{
				_logger.LogInformation("Added new CoolerSocket element: {0} (ID)",
				_dbContext.CoolerSockets.Where(e => e.CoolerID == el.ID).Select(e => e.ID).ToArray()[i]);
			}
			return await GetCoolers();
		}

		/// <summary>
		/// Add new <see cref="CPU"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{CPU}"/> of <see cref="CPU"/></returns>
		public async Task<IEnumerable<CPU>> AddCPU(AddCpuRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.CPUs.AddAsync(request.CPU);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new CPU element: {0} (ID)",
				_dbContext.CPUs.FirstAsync(e => e.Title == request.CPU.Title).Id);
			return await GetCPUs();
		}

		/// <summary>
		/// Add new <see cref="HDD"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{HDD}"/> of <see cref="HDD"/></returns>
		public async Task<IEnumerable<HDD>> AddHDD(AddHddRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			var hdd = (Entity.HDD)request.HDD;
			var interfaces = hdd.Interface.ToList();
			hdd.Interface = null;
			await _dbContext.HDDs.AddAsync(hdd);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new HDD element: {0} (ID)",
				_dbContext.HDDs.FirstAsync(e => e.Title == request.HDD.Title).Id);

			var nList = await _dbContext.HDDs.ToListAsync();
			var el = nList.First(e => e.Title == hdd.Title);
			for (int i = 0; i<interfaces.Count; i++)
			{
				interfaces[i].HDDID = el.ID;
			}
			await _dbContext.HDDInterfaces.AddRangeAsync(interfaces);
			await _dbContext.SaveChangesAsync();
			for (int i = 0; i < interfaces.Count; i++)
			{
				_logger.LogInformation("Added new HDDInterface element: {0} (ID)",
				_dbContext.HDDInterfaces.Where(e => e.HDDID == el.ID).Select(e => e.ID).ToArray()[i]);
			}
			return await GetHDDs();
		}

		/// <summary>
		/// Add new <see cref="Motherboard"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{Motherboard}"/> of <see cref="Motherboard"/></returns>
		public async Task<IEnumerable<Motherboard>> AddMotherboard(AddMotherboardRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			var motherboard = (Entity.Motherboard)request.Motherboard;
			var slots = motherboard.Slots.ToList();
			motherboard.Slots = null;
			await _dbContext.Motherboards.AddAsync(motherboard);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new Motherboard element: {0} (ID)",
				_dbContext.Motherboards.FirstAsync(e => e.Title == request.Motherboard.Title).Id);

			var nList = await _dbContext.Motherboards.ToListAsync();
			var el = nList.First(e => e.Title == motherboard.Title);
			for (int i = 0; i < slots.Count; i++)
			{
				slots[i].MotherboardID = el.ID;
			}
			await _dbContext.MotherboardSlots.AddRangeAsync(slots);
			await _dbContext.SaveChangesAsync();
			for (int i = 0; i < slots.Count; i++)
			{
				_logger.LogInformation("Added new MotherboardSlot element: {0} (ID)",
				_dbContext.MotherboardSlots.Where(e => e.MotherboardID == el.ID).Select(e => e.ID).ToArray()[i]);
			}
			return null;
		}

		/// <summary>
		/// Add new <see cref="RAM"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{RAM}"/> of <see cref="RAM"/></returns>
		public async Task<IEnumerable<RAM>> AddRAM(AddRamRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.RAMs.AddAsync(request.RAM);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new RAM element: {0} (ID)",
				_dbContext.RAMs.FirstAsync(e => e.Title == request.RAM.Title).Id);
			return null;
		}

		/// <summary>
		/// Add new <see cref="SSD"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{SSD}"/> of <see cref="SSD"/></returns>
		public async Task<IEnumerable<SSD>> AddSSD(AddSsdRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			var ssd = (Entity.SSD)request.SSD;
			var interfaces = ssd.Interface.ToList();
			ssd.Interface = null;
			await _dbContext.SSDs.AddAsync(ssd);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new SSD element: {0} (ID)",
				_dbContext.SSDs.FirstAsync(e => e.Title == request.SSD.Title).Id);

			var nList = await _dbContext.SSDs.ToListAsync();
			var el = nList.First(e => e.Title == ssd.Title);
			for(int i = 0; i<interfaces.Count; i++)
			{
				interfaces[i].SSDID = el.ID;
			}
			await _dbContext.SSDInterfaces.AddRangeAsync(interfaces);
			await _dbContext.SaveChangesAsync();
			for (int i = 0; i < interfaces.Count; i++)
			{
				_logger.LogInformation("Added new SSDInterface element: {0} (ID)",
				_dbContext.SSDInterfaces.Where(e => e.SSDID == el.ID).Select(e => e.ID).ToArray()[i]);
			}
			return await GetSSDs();
		}

		/// <summary>
		/// Add new <see cref="Videocard"/> record from request
		/// </summary>
		/// <param name="request">Add request</param>
		/// <returns>Updated <see cref="IEnumerable{Videocard}"/> of <see cref="Videocard"/></returns>
		public async Task<IEnumerable<Videocard>> AddVideocard(AddVideocardRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			var videocard = (Entity.Videocard)request.Videocard;
			var connectors = videocard.Connectors.ToList();
			videocard.Connectors = null;
			await _dbContext.Videocards.AddAsync(videocard);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Added new Videocard element: {0} (ID)",
				_dbContext.Videocards.FirstAsync(e => e.Title == request.Videocard.Title).Id);

			var nList = await _dbContext.Videocards.ToListAsync();
			var el = nList.First(e => e.Title == videocard.Title);
			for (int i = 0; i<connectors.Count; i++)
			{
				connectors[i].VideocardID = el.ID;
			}
			await _dbContext.VideocardConnectors.AddRangeAsync(connectors);
			await _dbContext.SaveChangesAsync();
			for (int i = 0; i < connectors.Count; i++)
			{
				_logger.LogInformation("Added new VideocardConnector element: {0} (ID)",
				_dbContext.VideocardConnectors.Where(e => e.VideocardID == el.ID).Select(e => e.ID).ToArray()[i]);
			}
			return await GetVideocards();
		}

		#endregion

		#region ADD PROPERTY

		public async Task<IEnumerable<Property>> AddProperty(string component, Property property)
		{
			Entity.Property newProp = new Entity.Property()
			{
				Component = component,
				Description = property.Description,
				Name = property.Name,
				ShowDescription = property.ShowDescription,
				Text = property.Text
			};
			await _dbContext.Properties.AddAsync(newProp);
			await _dbContext.SaveChangesAsync();

			foreach (string value in property.Values)
			{
				await AddValue(component, property.Name, value);
			}

			return await GetProperties(component);
		}

		public async Task<IEnumerable<Property>> AddPropertyValue(string component, string propertyName, string value)
		{
			await AddValue(component, propertyName, value);

			return await GetProperties(component);
		}

		public async Task<IEnumerable<Property>> AddPropertiesFromJSON(string component, string json)
		{
			var jd = JSONDataLoadServiceAsync.LoadObjects<Dictionary<string, Property>>(json);

			foreach (var kv in jd)
			{
				kv.Value.Name = kv.Key;
				await AddProperty(component, kv.Value);
			}

			return await GetProperties(component);
		}

		#endregion

		#endregion

		#region REMOVE

		#region REMOVE COMPONENT

		/// <summary>
		/// Removes <see cref="Body"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{Body}"/> of <see cref="Body"/></returns>
		public async Task<IEnumerable<Body>> RemoveBody(RemoveBodyRequest request)
		{
			var element = await _dbContext.Bodies.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.Bodies.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed Body element: {0} (ID)", request.ID);
			return await GetBodies();
		}

		/// <summary>
		/// Removes <see cref="Charger"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{Charger}"/> of <see cref="Charger"/></returns>
		public async Task<IEnumerable<Charger>> RemoveCharger(RemoveChargerRequest request)
		{
			var element = await _dbContext.Chargers.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.Chargers.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed Charger element: {0} (ID)", request.ID);
			return await GetChargers();
		}

		/// <summary>
		/// Removes <see cref="Cooler"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{Cooler}"/> of <see cref="Cooler"/></returns>
		public async Task<IEnumerable<Cooler>> RemoveCooler(RemoveCoolerRequest request)
		{
			var element = await _dbContext.Coolers.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.Coolers.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed Cooler element: {0} (ID)", request.ID);
			return await GetCoolers();
		}

		/// <summary>
		/// Removes <see cref="CPU"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{CPU}"/> of <see cref="CPU"/></returns>
		public async Task<IEnumerable<CPU>> RemoveCPU(RemoveCPURequest request)
		{
			var element = await _dbContext.CPUs.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.CPUs.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed CPU element: {0} (ID)", request.ID);
			return await GetCPUs();
		}

		/// <summary>
		/// Removes <see cref="HDD"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{HDD}"/> of <see cref="HDD"/></returns>
		public async Task<IEnumerable<HDD>> RemoveHDD(RemoveHDDRequest request)
		{
			var element = await _dbContext.HDDs.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.HDDs.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed HDD element: {0} (ID)", request.ID);
			return await GetHDDs();
		}

		/// <summary>
		/// Removes <see cref="Motherboard"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{Motherboard}"/> of <see cref="Motherboard"/></returns>
		public async Task<IEnumerable<Motherboard>> RemoveMotherboard(RemoveMotherboardRequest request)
		{
			var element = await _dbContext.Motherboards.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.Motherboards.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed Motherboard element: {0} (ID)", request.ID);
			return await GetMotherboards();
		}

		/// <summary>
		/// Removes <see cref="RAM"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{RAM}"/> of <see cref="RAM"/></returns>
		public async Task<IEnumerable<RAM>> RemoveRAM(RemoveRAMRequest request)
		{
			var element = await _dbContext.RAMs.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.RAMs.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed RAM element: {0} (ID)", request.ID);
			return await GetRAMs();
		}

		/// <summary>
		/// Removes <see cref="SSD"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{SSD}"/> of <see cref="SSD"/></returns>
		public async Task<IEnumerable<SSD>> RemoveSSD(RemoveSSDRequest request)
		{
			var element = await _dbContext.SSDs.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.SSDs.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed SSD element: {0} (ID)", request.ID);
			return await GetSSDs();
		}

		/// <summary>
		/// Removes <see cref="Videocard"/> record
		/// </summary>
		/// <param name="request">Remove reqest</param>
		/// <returns>Updated <see cref="IEnumerable{Videocard}"/> of <see cref="Videocard"/></returns>
		public async Task<IEnumerable<Videocard>> RemoveVideocard(RemoveVideocardRequest request)
		{
			var element = await _dbContext.Videocards.FirstOrDefaultAsync(e => e.ID == request.ID);
			if (element == null)
			{
				throw new ArgumentException("Incorrect ID", "request.ID");
			}
			_dbContext.Videocards.Remove(element);
			await _dbContext.SaveChangesAsync();
			_logger.LogInformation("Removed Videocard element: {0} (ID)", request.ID);
			return await GetVideocards();
		}

		#endregion

		#region REMOVE PROPERTY

		public async Task<IEnumerable<Property>> RemoveProperty(string component, Property property)
		{
			Entity.Property prop = await _dbContext.Properties.FirstOrDefaultAsync(e => e.Component == component && e.Name == property.Name);
			_dbContext.Properties.Remove(prop);
			await _dbContext.SaveChangesAsync();

			return await GetProperties(component);
		}

		public async Task<IEnumerable<Property>> RemovePropertyValue(string component, string propertyName, string value)
		{
			await RemoveValue(component, propertyName, value);

			return await GetProperties(component);
		}

		#endregion

		#endregion

		#region PUT

		#region PUT COMPONENT

		/// <summary>
		/// Update <see cref="Body"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{Body}"/> of <see cref="Body"/></returns>
		public async Task<IEnumerable<Body>> ReplaceBody(UpdateBodyRequest request)
		{
			var element = await _dbContext.Bodies.FirstOrDefaultAsync(e => e.ID == request.Body.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			element.CopyParameters(request.Body);
			_dbContext.Bodies.Update(element);
			await _dbContext.SaveChangesAsync();

			_logger.LogInformation("Updated Body record: {0} (ID)", request.Body.ID);

			return await GetBodies();
		}

		/// <summary>
		/// Update <see cref="Charger"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{Charger}"/> of <see cref="Charger"/></returns>
		public async Task<IEnumerable<Charger>> ReplaceCharger(UpdateChargerRequest request)
		{
			var element = await _dbContext.Chargers.FirstOrDefaultAsync(e => e.ID == request.Charger.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			element.CopyParameters(request.Charger);
			_dbContext.Chargers.Update(element);
			await _dbContext.SaveChangesAsync();

			_logger.LogInformation("Updated Charger record: {0} (ID)", request.Charger.ID);

			return await GetChargers();
		}

		/// <summary>
		/// Update <see cref="Cooler"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{Cooler}"/> of <see cref="Cooler"/></returns>
		public async Task<IEnumerable<Cooler>> ReplaceCooler(UpdateCoolerRequest request)
		{
			var element = await _dbContext.Coolers.FirstOrDefaultAsync(e => e.ID == request.Cooler.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			var sockets = await _dbContext.CoolerSockets.Where(e => e.CoolerID == element.ID).ToListAsync();
			var notInRequest = sockets.Where(e => !request.Cooler.Socket.Contains(e.Socket)).ToList();
			var notInDB = request.Cooler.Socket.Where(e => !sockets.Select(s => s.Socket).Contains(e)).ToList();

			foreach (var socket in notInRequest)
			{
				_dbContext.CoolerSockets.Remove(socket);
				_logger.LogInformation("Removed CoolerSocket record: {0} (ID)", socket.ID);
			}

			foreach (var socket in notInDB)
			{
				_dbContext.CoolerSockets.Add(new Entity.CoolerSocket() { Socket = socket, CoolerID = element.ID, Cooler = element });
			}

			element.CopyParameters(request.Cooler);
			_dbContext.Coolers.Update(element);
			await _dbContext.SaveChangesAsync();

			for (int i = 0; i < notInDB.Count; i++)
			{
				_logger.LogInformation("Added CoolerSocket record: {0} (ID)",
					_dbContext.CoolerSockets.Where(e => e.CoolerID == element.ID).ToArray()[i]);
			}

			_logger.LogInformation("Updated Cooler record: {0} (ID)", request.Cooler.ID);

			return await GetCoolers();
		}

		/// <summary>
		/// Update <see cref="CPU"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{CPU}"/> of <see cref="CPU"/></returns>
		public async Task<IEnumerable<CPU>> ReplaceCPU(UpdateCPURequest request)
		{
			var element = await _dbContext.CPUs.FirstOrDefaultAsync(e => e.ID == request.CPU.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			element.CopyParameters(request.CPU);
			_dbContext.Update(element);
			await _dbContext.SaveChangesAsync();

			_logger.LogInformation("Updated CPU record: {0} (ID)", request.CPU.ID);

			return await GetCPUs();
		}

		/// <summary>
		/// Update <see cref="HDD"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{HDD}"/> of <see cref="HDD"/></returns>
		public async Task<IEnumerable<HDD>> ReplaceHDD(UpdateHDDRequest request)
		{
			var element = await _dbContext.HDDs.FirstOrDefaultAsync(e => e.ID == request.HDD.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			var interfaces = await _dbContext.HDDInterfaces.Where(e => e.HDDID == element.ID).ToListAsync();
			var notInRequest = interfaces.Where(e => !request.HDD.Interface.Contains(e.Interface)).ToList();
			var notInDB = request.HDD.Interface.Where(e => !interfaces.Select(s => s.Interface).Contains(e)).ToList();

			foreach (var @interface in notInRequest)
			{
				_dbContext.HDDInterfaces.Remove(@interface);
				_logger.LogInformation("Removed HDDInterface record: {0} (ID)", @interface.ID);
			}

			foreach (var @interface in notInDB)
			{
				_dbContext.HDDInterfaces.Add(new Entity.HDDInterface() { Interface = @interface, HDDID = element.ID, HDD = element });
			}

			element.CopyParameters(request.HDD);
			_dbContext.HDDs.Update(element);
			await _dbContext.SaveChangesAsync();

			for (int i = 0; i < notInDB.Count; i++)
			{
				_logger.LogInformation("Added HDDInterface record: {0} (ID)",
					_dbContext.HDDInterfaces.Where(e => e.HDDID == element.ID).ToArray()[i]);
			}

			_logger.LogInformation("Updated HDD record: {0} (ID)", request.HDD.ID);

			return await GetHDDs();
		}

		/// <summary>
		/// Update <see cref="Motherboard"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{Motherboard}"/> of <see cref="Motherboard"/></returns>
		public async Task<IEnumerable<Motherboard>> ReplaceMotherboard(UpdateMotherboardRequest request)
		{
			var element = await _dbContext.Motherboards.FirstOrDefaultAsync(e => e.ID == request.Motherboard.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			var slots = await _dbContext.MotherboardSlots.Where(e => e.MotherboardID == element.ID).ToListAsync();
			var notInRequest = slots.Where(e => !request.Motherboard.Socket.Contains(e.Slot)).ToList();
			var notInDB = request.Motherboard.Slots.Where(e => !slots.Select(s => s.Slot).Contains(e)).ToList();

			foreach (var slot in notInRequest)
			{
				_dbContext.MotherboardSlots.Remove(slot);
				_logger.LogInformation("Removed MotherboardSlot record: {0} (ID)", slot.ID);
			}

			foreach (var slot in notInDB)
			{
				_dbContext.MotherboardSlots.Add(new Entity.MotherboardSlot() { Slot = slot, MotherboardID = element.ID, Motherboard = element });
			}

			element.CopyParameters(request.Motherboard);
			_dbContext.Motherboards.Update(element);
			await _dbContext.SaveChangesAsync();

			for (int i = 0; i < notInDB.Count; i++)
			{
				_logger.LogInformation("Added MotherboardSlot record: {0} (ID)",
					_dbContext.MotherboardSlots.Where(e => e.MotherboardID == element.ID).ToArray()[i]);
			}

			_logger.LogInformation("Updated Motherboard record: {0} (ID)", request.Motherboard.ID);

			return await GetMotherboards();
		}

		/// <summary>
		/// Update <see cref="RAM"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{RAM}"/> of <see cref="RAM"/></returns>
		public async Task<IEnumerable<RAM>> ReplaceRAM(UpdateRAMRequest request)
		{
			var element = await _dbContext.RAMs.FirstOrDefaultAsync(e => e.ID == request.RAM.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			element.CopyParameters(request.RAM);
			_dbContext.Update(element);
			await _dbContext.SaveChangesAsync();

			_logger.LogInformation("Updated RAM record: {0} (ID)", request.RAM.ID);

			return await GetRAMs();
		}

		/// <summary>
		/// Update <see cref="SSD"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{SSD}"/> of <see cref="SSD"/></returns>
		public async Task<IEnumerable<SSD>> ReplaceSSD(UpdateSSDRequest request)
		{
			var element = await _dbContext.SSDs.FirstOrDefaultAsync(e => e.ID == request.SSD.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}
			var interfaces = await _dbContext.SSDInterfaces.Where(e => e.SSDID == element.ID).ToListAsync();
			var notInRequest = interfaces.Where(e => !request.SSD.Interface.Contains(e.Interface)).ToList();
			var notInDB = request.SSD.Interface.Where(e => !interfaces.Select(s => s.Interface).Contains(e)).ToList();

			foreach (var @interface in notInRequest)
			{
				_dbContext.SSDInterfaces.Remove(@interface);
				_logger.LogInformation("Removed SSDInterfaces record: {0} (ID)", @interface.ID);
			}

			foreach (var @interface in notInDB)
			{
				_dbContext.SSDInterfaces.Add(new Entity.SSDInterface() { Interface = @interface, SSDID = element.ID, SSD = element });
			}

			element.CopyParameters(request.SSD);
			_dbContext.SSDs.Update(element);
			await _dbContext.SaveChangesAsync();

			for (int i = 0; i < notInDB.Count; i++)
			{
				_logger.LogInformation("Added SSDInterfaces record: {0} (ID)",
					_dbContext.SSDInterfaces.Where(e => e.SSDID == element.ID).ToArray()[i]);
			}

			_logger.LogInformation("Updated SSD record: {0} (ID)", request.SSD.ID);

			return await GetSSDs();
		}

		/// <summary>
		/// Update <see cref="Videocard"/> record
		/// </summary>
		/// <param name="request">Update request</param>
		/// <returns>Updated <see cref="IEnumerable{Videocard}"/> of <see cref="Videocard"/></returns>
		public async Task<IEnumerable<Videocard>> ReplaceVideocard(UpdateVideocardRequest request)
		{
			var element = await _dbContext.Videocards.FirstOrDefaultAsync(e => e.ID == request.Videocard.ID);
			if (element == null)
			{
				throw new ArgumentException("Value not found");
			}

			var connectors = await _dbContext.VideocardConnectors.Where(e => e.VideocardID == element.ID).ToListAsync();
			var notInRequest = connectors.Where(e => !request.Videocard.Connectors.Contains(e.Connector)).ToList();
			var notInDB = request.Videocard.Connectors.Where(e => !connectors.Select(s => s.Connector).Contains(e)).ToList();

			//Remove connectors of current record not presented in new record from table
			foreach (var connector in notInRequest)
			{
				_dbContext.VideocardConnectors.Remove(connector);
				_logger.LogInformation("Removed VideocardConnector record: {0} (ID)", connector.ID);
			}

			//Add connectors of new record not presented in current record to table
			foreach (var connector in notInDB)
			{
				_dbContext.VideocardConnectors.Add(new Entity.VideocardConnector() { Connector = connector, VideocardID = element.ID, Videocard = element });
			}

			element.CopyParameters(request.Videocard);
			_dbContext.Videocards.Update(element);
			await _dbContext.SaveChangesAsync();

			for (int i = 0; i<notInDB.Count; i++)
			{
				_logger.LogInformation("Added VideocardConnector record: {0} (ID)", 
					_dbContext.VideocardConnectors.Where(e => e.VideocardID == element.ID).ToArray()[i]);
			}

			_logger.LogInformation("Updated Videocard record: {0} (ID)", request.Videocard.ID);

			return await GetVideocards();
		}

		#endregion

		#region PUT PROPERTY

		public async Task<IEnumerable<Property>> ChangeProperty(string component, Property oldProperty, Property newProperty)
		{
			Entity.Property prop = await _dbContext.Properties.FirstOrDefaultAsync(e => e.Component == component && e.Name == oldProperty.Name);
			prop.Name = newProperty.Name;
			prop.Description = newProperty.Description;
			prop.ShowDescription = newProperty.ShowDescription;
			_dbContext.Properties.Update(prop);
			await _dbContext.SaveChangesAsync();

			foreach (string value in oldProperty.Values)
			{
				if (!newProperty.Values.Contains(value))
				{
					await RemovePropertyValue(component, prop.Name, value);
				}
			}

			foreach (string value in newProperty.Values)
			{
				if (!oldProperty.Values.Contains(value))
				{
					await AddPropertyValue(component, prop.Name, value);
				}
			}

			return await GetProperties(component);
		}

		#endregion

		#endregion

		/// <summary>
		/// Checks compatibility of defined type elements
		/// </summary>
		/// <param name="type1">Type of element checked</param>
		/// <param name="type2">Type of element to check compatibility with</param>
		/// <param name="element1">Checked element</param>
		/// <param name="element2">Element to check compatibily with</param>
		/// <param name="incompatible">Incompatible properties</param>
		/// <returns>True - if components are fully compatible. False - if not</returns>
		private bool CheckCompatibility(string type1, string type2, object element1, object element2, out string incompatible)
		{
			incompatible = "";

			bool res = true;
			LuaTable table = (_compatibility[type1] as LuaTable)[type2] as LuaTable;

			var t1p = table[type1];
			var t2p = table[type2];
			var cond = table["condition"];

			Type t1t = element1.GetType();
			Type t2t = element2.GetType();

			if (t1p is LuaTable)
			{
				var t1ps = t1p as LuaTable;
				var t2ps = t1p as LuaTable;
				var conditions = cond as LuaTable;

				var t1enum = t1ps.Values.GetEnumerator();
				var t2enum = t2ps.Values.GetEnumerator();
				var cenum = conditions.Values.GetEnumerator();
				while (t1enum.MoveNext() && t2enum.MoveNext() && cenum.MoveNext())
				{
					if (!CheckCondition(cenum.Current.ToString(), t1enum.Current, t2enum.Current, t1t, t2t, t1enum.Current.ToString(), t2enum.Current.ToString(), out res))
					{
						incompatible += incompatible == "" ? t2enum.Current.ToString() : $", {t2enum.Current.ToString()}";
					}
				}
			}
			else
			{
				if (!CheckCondition(cond.ToString(), element1, element2, t1t, t2t, t1p.ToString(), t2p.ToString(), out res))
				{
					incompatible += incompatible == "" ? t2p.ToString() : $", {t2p.ToString()}";
				}
			}
			return res;
		}

		/// <summary>
		/// Checks given input type values properties on condition
		/// </summary>
		/// <param name="condition">One of predefined conditions</param>
		/// <param name="element1">First input value</param>
		/// <param name="element2">Second input value</param>
		/// <param name="type1">First input value type</param>
		/// <param name="type2">Second input value type</param>
		/// <param name="property1">First input value propert to check</param>
		/// <param name="property2">Second input value property to check</param>
		/// <param name="result">Current state of global compatibility check</param>
		/// <returns>Result of check</returns>
		bool CheckCondition(string condition, object element1, object element2, Type type1, Type type2, string property1, string property2, out bool result)
		{
			switch (condition)
			{
				case "equal": //Value of element1.property1 equal to element2.property2 value
				{
					var v1 = type1.GetProperty(property1.ToString()).GetValue(element1);
					var v2 = type2.GetProperty(property2.ToString()).GetValue(element2);
					if (v1 != v2)
					{
						result = false;
						return false;
					}
					result = true;
					return true;
				}
				case "less or equal": //Value of element1.property1 is less or equal to element2.property2 value. 
									  //Both values are IComparable
				{
					IComparable v1 = type1.GetProperty(property1.ToString()).GetValue(element1) as IComparable;
					IComparable v2 = type2.GetProperty(property2.ToString()).GetValue(element2) as IComparable;
					if (v1 != null && v2 != null)
					{
						if (v1.CompareTo(v2) > 0)
						{
							result = false;
							return false;
						}
					}
					result = true;
					return true;
				}
				case "greater or equal": //Value of element1.property1 is greater or equal to element2.property2 value. 
										 //Both values are IComparable
				{
					IComparable v1 = type1.GetProperty(property1.ToString()).GetValue(element1) as IComparable;
					IComparable v2 = type2.GetProperty(property2.ToString()).GetValue(element2) as IComparable;
					if (v1 != null && v2 != null)
					{
						if (v1.CompareTo(v2) < 0)
						{
							result = false;
							return false;
						}
					}
					result = true;
					return true;
				}
				case "contain": //Element1.property1 value is IEnumerable and contains element2.property2 value
				{
					IEnumerable<object> v1 = type1.GetProperty(property1.ToString()).GetValue(element1) as IEnumerable<object>;
					var v2 = type2.GetProperty(property2.ToString()).GetValue(element2);
					if (!v1?.Contains(v2) ?? true)
					{
						result = false;
						return false;
					}
					result = true;
					return true;
				}
				case "contained": //Element1.property1 value is contained in IEnumerable element2.property2 value
				{
					var v1 = type1.GetProperty(property1.ToString()).GetValue(element1);
					IEnumerable<object> v2 = type2.GetProperty(property2.ToString()).GetValue(element2) as IEnumerable<object>;
					if (!v2?.Contains(v1) ?? true)
					{
						result = false;
						return false;
					}
					result = true;
					return true;
				}
				case "intersect": //Element1.property1 and element2.property2 values are Ienumerable and has at least one same value
				{
					IEnumerable<object> v1 = type1.GetProperty(property1.ToString()).GetValue(element1) as IEnumerable<object>;
					IEnumerable<object> v2 = type2.GetProperty(property2.ToString()).GetValue(element2) as IEnumerable<object>;
					result = false;
					foreach (var v in v1)
					{
						if (v2?.Contains(v) ?? false)
						{
							result = true;
						}
					}
					return true;
				}
				default:
				{
					result = true;
					return true;
				}
			}
		}

		/// <summary>
		/// Removes property value
		/// </summary>
		/// <param name="component">Component</param>
		/// <param name="propertyName">Property name</param>
		/// <param name="value">Value to remove</param>
		/// <returns></returns>
		private async Task RemoveValue(string component, string propertyName, string value)
		{
			Entity.PropertyValue val = await _dbContext.PropertyValues.FirstOrDefaultAsync(
				e => e.Property.Component == component &&
				e.Property.Name == propertyName &&
				e.Value == value);

			if (val != null)
			{
				_dbContext.PropertyValues.Remove(val);
				await _dbContext.SaveChangesAsync();
			}
		}

		/// <summary>
		/// Adds value to property
		/// </summary>
		/// <param name="component">Component</param>
		/// <param name="propertyName">Property name</param>
		/// <param name="value">Value to add</param>
		/// <returns></returns>
		private async Task AddValue(string component, string propertyName, string value)
		{
			var prop = await _dbContext.Properties.FirstOrDefaultAsync(e => e.Component == component && e.Name == propertyName);

			if (prop != null)
			{
				Entity.PropertyValue val = new Entity.PropertyValue()
				{
					Property = prop,
					PropertyID = prop.ID,
					Value = value
				};
				await _dbContext.PropertyValues.AddAsync(val);
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}