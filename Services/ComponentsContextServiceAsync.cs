using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;

using Microsoft.Extensions.Caching.Memory;

using ComputerComplectorWebAPI.DataContext;
using ComputerComplectorWebAPI.Interfaces;
using Entity = ComputerComplectorWebAPI.EntityFramework.Models.Data;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Get;
using ComputerComplectorWebAPI.Models.Requests.Add;
using Microsoft.EntityFrameworkCore;
using ComputerComplectorWebAPI.Models.Requests.Remove;
using ComputerComplectorWebAPI.Models.Requests.Update;

namespace ComputerComplectorWebAPI.Services
{
	/// <summary>
	/// Asynchronous User component service
	/// </summary>
	public class ComponentsContextServiceAsync : IComponentsServiceAsync
	{
		private ComponentsContext _dbContext;

		private IMemoryCache _cache;

		public ComponentsContextServiceAsync(ComponentsContext dbContext, IMemoryCache cache)
		{
			_dbContext = dbContext;

			_cache = cache;
		}

		#region GET

		#region GET BY ID
		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<Body> GetBody(int id = -1)
		{
			var list = await _dbContext.Bodies.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<Charger> GetCharger(int id = -1)
		{
			var list = await _dbContext.Chargers.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<Cooler> GetCooler(int id = -1)
		{
			var list = await _dbContext.Coolers.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<CPU> GetCPU(int id = -1)
		{
			var list = await _dbContext.CPUs.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<HDD> GetHDD(int id = -1)
		{
			var list = await _dbContext.HDDs.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<Motherboard> GetMotherboard(int id = -1)
		{
			var list = await _dbContext.Motherboards.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<RAM> GetRAM(int id = -1)
		{
			var list = await _dbContext.RAMs.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<SSD> GetSSD(int id = -1)
		{
			var list = await _dbContext.SSDs.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}

		/// <summary>
		/// Get record by ID. Null if incorrect ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns></returns>
		public async Task<Videocard> GetVideocard(int id = -1)
		{
			var list = await _dbContext.Videocards.ToListAsync();
			return list.FirstOrDefault(e => e.ID == id);
		}
		#endregion

		#region GET ALL
		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Body>> GetBodies()
		{
			return (await _dbContext.Bodies.ToListAsync()).Select(e => (Body)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Charger>> GetChargers()
		{
			return (await _dbContext.Chargers.ToListAsync()).Select(e => (Charger)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Cooler>> GetCoolers()
		{
			return (await _dbContext.Coolers.ToListAsync()).Select(e => (Cooler)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<CPU>> GetCPUs()
		{
			return (await _dbContext.CPUs.ToListAsync()).Select(e => (CPU)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<HDD>> GetHDDs()
		{
			return (await _dbContext.HDDs.ToListAsync()).Select(e => (HDD)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Motherboard>> GetMotherboards()
		{
			return (await _dbContext.Motherboards.ToListAsync()).Select(e => (Motherboard)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<RAM>> GetRAMs()
		{
			return (await _dbContext.RAMs.ToListAsync()).Select(e => (RAM)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<SSD>> GetSSDs()
		{
			return (await _dbContext.SSDs.ToListAsync()).Select(e => (SSD)e).ToList();
		}

		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Videocard>> GetVideocards()
		{
			return (await _dbContext.Videocards.ToListAsync()).Select(e => (Videocard)e).ToList();
		}
		#endregion

		#region GET BY REQUEST
		public async Task<IEnumerable<Body>> GetBodies(GetBodiesRequest request)
		{
			if (!request.Parameters.Any())
			{
				return await GetBodies();
			}

			List<Body> bodies;
			bodies = (await _dbContext.Bodies.ToListAsync()).Select(e => (Body)e).
				Where(
					e => request.BuildInCharger != null &&
					request.BuildInCharger.ToList().Contains(e.BuildInCharger)).
				Where(
					e => request.ChargerPower != null &&
					request.ChargerPower.Any(
						p => (
							e.ChargerPower >= int.Parse(p.Split('-')[0]) &&
							e.ChargerPower <= int.Parse(p.Split('-')[1])))).
				Where(
					e => request.Company != null &&
					request.Company.Contains(e.Company)).
				Where(
					e => request.Formfactor != null &&
					request.Formfactor.Contains(e.Formfactor)).
				Where(
					e => request.Type != null && request.Type.Contains(e.Type)).
				Where(
					e => request.Usb3Ports != null && request.Usb3Ports.Contains(e.USB3Amount.ToString())).
				ToList();

			if (request.SelectedMotherboard != null)
			{
				var motherboard = await _dbContext.Motherboards.Where(e => e.ID == request.SelectedMotherboard).FirstOrDefaultAsync();
				bodies = bodies.Where(e => e.Formfactor == (motherboard?.Formfactor ?? e.Formfactor)).ToList();
			}

			if (request.SelectedVideocard != null)
			{
				var videocard = await _dbContext.Videocards.Where(e => e.ID == request.SelectedVideocard).FirstOrDefaultAsync();
				bodies = bodies.Where(e => e.VideocardMaxLength >= int.Parse(videocard?.Length ?? e.VideocardMaxLength.ToString())).ToList();
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

			List<Charger> chargers;

			chargers = (await _dbContext.Chargers.ToListAsync()).Select(e => (Charger)e).
				Where(e => request.Company != null && request.Company.Contains(e.Company)).
				Where(e => request.ConnectorType != null && request.ConnectorType.Contains(e.ConnectorType)).
				Where(e => request.IDEAmount != null && request.IDEAmount.Contains(e.IDEAmount.ToString())).
				Where(e => request.MotherboardConnector != null && request.MotherboardConnector.Contains(e.MotherboardConnector)).
				Where(e => request.Power != null && request.Power.Contains(e.Power.ToString())).
				Where(e => request.SATAAmount != null && request.SATAAmount.Contains(e.SATAAmount.ToString())).
				Where(e => request.Series != null && request.Series.Contains(e.Series)).
				Where(e => request.Sertificate != null && request.Sertificate.Contains(e.Sertificate80Plus)).
				Where(e => request.VideoConnectorsAmount != null && request.VideoConnectorsAmount.Contains(e.VideoConnectorsAmount)).
				ToList();

			if (request.SelectedMotherboard != null)
			{
				var motherboard = (await _dbContext.Motherboards.ToListAsync()).First(e => e.ID == request.SelectedMotherboard);
				chargers = chargers.Where(e => e.MotherboardConnector == motherboard.Pin && e.ConnectorType == motherboard.CPUPin).ToList();
			}

			if (request.SelectedVideocard != null)
			{
				var videocard = (await _dbContext.Videocards.ToListAsync()).First(e => e.ID == request.SelectedVideocard);
				chargers = chargers.Where(e => e.VideocardConnector == videocard.Pin).ToList();
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

			List<Cooler> coolers = (await _dbContext.Coolers.ToListAsync()).Select(e => (Cooler)e).ToList();

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

			List<CPU> cpus = (await _dbContext.CPUs.ToListAsync()).Select(e => (CPU)e).ToList();

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

			List<HDD> hdds = (await _dbContext.HDDs.ToListAsync()).Select(e => (HDD)e).ToList();

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

			List<Motherboard> motherboards = (await _dbContext.Motherboards.ToListAsync()).Select(e => (Motherboard)e).ToList();

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

			List<RAM> rams = (await _dbContext.RAMs.ToListAsync()).Select(e => (RAM)e).ToList();

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

			List<SSD> ssds = (await _dbContext.SSDs.ToListAsync()).Select(e => (SSD)e).ToList();

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

			List<Videocard> videocards = (await _dbContext.Videocards.ToListAsync()).Select(e => (Videocard)e).ToList();

			return videocards;
		}
		#endregion

		#endregion

		#region ADD
		public async Task<IEnumerable<Body>> AddBody(AddBodyRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.Bodies.AddAsync(request.Body);
			await _dbContext.SaveChangesAsync();
			return await GetBodies();
		}

		public async Task<IEnumerable<Charger>> AddCharger(AddChargerRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.Chargers.AddAsync(request.Charger);
			await _dbContext.SaveChangesAsync();
			return await GetChargers();
		}

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

			var nList = await _dbContext.Coolers.ToListAsync();
			var el = nList.First(e => e.Title == cooler.Title);
			for (int i = 0; i<sockets.Count; i++)
			{
				sockets[i].CoolerID = el.ID;
			}
			await _dbContext.CoolerSockets.AddRangeAsync(sockets);
			await _dbContext.SaveChangesAsync();

			return await GetCoolers();
		}

		public async Task<IEnumerable<CPU>> AddCPU(AddCpuRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.CPUs.AddAsync(request.CPU);
			await _dbContext.SaveChangesAsync();
			return await GetCPUs();
		}

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

			var nList = await _dbContext.HDDs.ToListAsync();
			var el = nList.First(e => e.Title == hdd.Title);
			for (int i = 0; i<interfaces.Count; i++)
			{
				interfaces[i].HDDID = el.ID;
			}
			await _dbContext.HDDInterfaces.AddRangeAsync(interfaces);
			await _dbContext.SaveChangesAsync();
			return await GetHDDs();
		}

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

			var nList = await _dbContext.Motherboards.ToListAsync();
			var el = nList.First(e => e.Title == motherboard.Title);
			for (int i = 0; i < slots.Count; i++)
			{
				slots[i].MotherboardID = el.ID;
			}
			await _dbContext.MotherboardSlots.AddRangeAsync(slots);
			return null;
		}

		public async Task<IEnumerable<RAM>> AddRAM(AddRamRequest request)
		{
			if (!request.Parameters.Any())
			{
				throw new ArgumentException("Invalid model to add. Empty parameters", "request");
			}

			await _dbContext.RAMs.AddAsync(request.RAM);
			await _dbContext.SaveChangesAsync();
			return null;
		}

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

			var nList = await _dbContext.SSDs.ToListAsync();
			var el = nList.First(e => e.Title == ssd.Title);
			for(int i = 0; i<interfaces.Count; i++)
			{
				interfaces[i].SSDID = el.ID;
			}
			await _dbContext.SSDInterfaces.AddRangeAsync(interfaces);
			await _dbContext.SaveChangesAsync();
			return await GetSSDs();
		}

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

			var nList = await _dbContext.Videocards.ToListAsync();
			var el = nList.First(e => e.Title == videocard.Title);
			for (int i = 0; i<connectors.Count; i++)
			{
				connectors[i].VideocardID = el.ID;
			}
			await _dbContext.VideocardConnectors.AddRangeAsync(connectors);
			await _dbContext.SaveChangesAsync();
			return await GetVideocards();
		}

		#endregion

		#region REMOVE

		public async Task<IEnumerable<Body>> RemoveBody(RemoveBodyRequest request)
		{
			var element = await _dbContext.Bodies.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.Bodies.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetBodies();
		}

		public async Task<IEnumerable<Charger>> RemoveCharger(RemoveChargerRequest request)
		{
			var element = await _dbContext.Chargers.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.Chargers.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetChargers();
		}

		public async Task<IEnumerable<Cooler>> RemoveCooler(RemoveCoolerRequest request)
		{
			var element = await _dbContext.Coolers.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.Coolers.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetCoolers();
		}

		public async Task<IEnumerable<CPU>> RemoveCPU(RemoveCPURequest request)
		{
			var element = await _dbContext.CPUs.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.CPUs.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetCPUs();
		}

		public async Task<IEnumerable<HDD>> RemoveHDD(RemoveHDDRequest request)
		{
			var element = await _dbContext.HDDs.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.HDDs.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetHDDs();
		}

		public async Task<IEnumerable<Motherboard>> RemoveMotherboard(RemoveMotherboardRequest request)
		{
			var element = await _dbContext.Motherboards.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.Motherboards.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetMotherboards();
		}

		public async Task<IEnumerable<RAM>> RemoveRAM(RemoveRAMRequest request)
		{
			var element = await _dbContext.RAMs.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.RAMs.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetRAMs();
		}

		public async Task<IEnumerable<SSD>> RemoveSSD(RemoveSSDRequest request)
		{
			var element = await _dbContext.SSDs.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.SSDs.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetSSDs();
		}

		public async Task<IEnumerable<Videocard>> RemoveVideocard(RemoveVideocardRequest request)
		{
			var element = await _dbContext.Videocards.FirstOrDefaultAsync(e => e.ID == request.ID);
			_dbContext.Videocards.Remove(element);
			await _dbContext.SaveChangesAsync();
			return await GetVideocards();
		}

		#endregion

		#region PUT

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
			return await GetBodies();
		}

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
			return await GetChargers();
		}

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
			}

			foreach (var socket in notInDB)
			{
				_dbContext.CoolerSockets.Add(new Entity.CoolerSocket() { Socket = socket, CoolerID = element.ID, Cooler = element });
			}

			element.CopyParameters(request.Cooler);
			_dbContext.Coolers.Update(element);
			await _dbContext.SaveChangesAsync();
			return await GetCoolers();
		}

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
			return await GetCPUs();
		}

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
			}

			foreach (var @interface in notInDB)
			{
				_dbContext.HDDInterfaces.Add(new Entity.HDDInterface() { Interface = @interface, HDDID = element.ID, HDD = element });
			}

			element.CopyParameters(request.HDD);
			_dbContext.HDDs.Update(element);
			await _dbContext.SaveChangesAsync();
			return await GetHDDs();
		}

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
			}

			foreach (var slot in notInDB)
			{
				_dbContext.MotherboardSlots.Add(new Entity.MotherboardSlot() { Slot = slot, MotherboardID = element.ID, Motherboard = element });
			}

			element.CopyParameters(request.Motherboard);
			_dbContext.Motherboards.Update(element);
			await _dbContext.SaveChangesAsync();
			return await GetMotherboards();
		}

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
			return await GetRAMs();
		}

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
			}

			foreach (var @interface in notInDB)
			{
				_dbContext.SSDInterfaces.Add(new Entity.SSDInterface() { Interface = @interface, SSDID = element.ID, SSD = element });
			}

			element.CopyParameters(request.SSD);
			_dbContext.SSDs.Update(element);
			await _dbContext.SaveChangesAsync();
			return await GetSSDs();
		}

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

			foreach (var connector in notInRequest)
			{
				_dbContext.VideocardConnectors.Remove(connector);
			}

			foreach (var connector in notInDB)
			{
				_dbContext.VideocardConnectors.Add(new Entity.VideocardConnector() { Connector = connector, VideocardID = element.ID, Videocard = element });
			}

			element.CopyParameters(request.Videocard);
			_dbContext.Videocards.Update(element);
			await _dbContext.SaveChangesAsync();
			return await GetVideocards();
		}

		#endregion
	}
}