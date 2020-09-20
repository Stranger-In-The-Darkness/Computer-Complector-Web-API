using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Get;
using ComputerComplectorWebAPI.Models.Requests.Add;
using ComputerComplectorWebAPI.Models.Requests.Update;
using ComputerComplectorWebAPI.Models.Requests.Remove;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using ComputerComplectorWebAPI.Models.Data.Special;
using ComputerComplectorWebAPI.Models.Statistics.Requests;

namespace ComputerComplectorWebAPI.Controllers
{
	/// <summary>
	/// Components service user controller
	/// </summary>
	[Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : Controller
    {
		/// <summary>
		/// Components DB service
		/// </summary>
		private IComponentsServiceAsync _componentService;

		/// <summary>
		/// Localization service
		/// </summary>
        private IStringLocalizer<ComponentsController> _localizer;

		/// <summary>
		/// Logger
		/// </summary>
		private ILogger<ComponentsController> _logger;

		private Compatibility _compatibility;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="componentService">Components service</param>
		/// <param name="localizer">Localizer</param>
		/// <param name="logger">Logger</param>
        public ComponentsController(IComponentsServiceAsync componentService, IStringLocalizer<ComponentsController> localizer, ILogger<ComponentsController> logger)
        {
            _componentService = componentService;
            _localizer = localizer;
			_logger = logger;
        }

		#region GET
		#region PROPERTIES

		/// <summary>
		/// Get <see cref="Body"/> properties
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("body/properties")]
        public async Task<string> GetBodyProperties()
        {
			var list = await _componentService.GetProperties("body");
			return FormatProperties(list);
            //return await Task.Run(() => { return _localizer["body"].Value; });
        }

		/// <summary>
		/// Get <see cref="Charger"/> properties
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("charger/properties")]
        public async Task<string> GetChargerProperties()
        {
			var list = await _componentService.GetProperties("charger");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["charger"].Value; });
		}

		/// <summary>
		/// Get localized <see cref="Cooler"/> properties from <see cref="IStringLocalizer{ComponentsController}"/>
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("cooler/properties")]
        public async Task<string> GetCoolerProperties()
        {
			var list = await _componentService.GetProperties("cooler");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["cooler"].Value; });
		}

		/// <summary>
		/// Get localized <see cref="CPU"/> properties from <see cref="IStringLocalizer{ComponentsController}"/>
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("cpu/properties")]
        public async Task<string> GetCPUProperties()
        {
			var list = await _componentService.GetProperties("cpu");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["cpu"].Value; });
		}

		/// <summary>
		/// Get localized <see cref="HDD"/> properties from <see cref="IStringLocalizer{ComponentsController}"/>
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("hdd/properties")]
        public async Task<string> GetHDDProperties()
        {
			var list = await _componentService.GetProperties("hdd");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["hdd"].Value; });
		}

		/// <summary>
		/// Get localized <see cref="Motherboard"/> properties from <see cref="IStringLocalizer{ComponentsController}"/>
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("motherboard/properties")]
        public async Task<string> GetMotherboardProperties()
        {
			var list = await _componentService.GetProperties("motherboard");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["motherboard"].Value; });
		}

		/// <summary>
		/// Get localized <see cref="RAM"/> properties from <see cref="IStringLocalizer{ComponentsController}"/>
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("ram/properties")]
        public async Task<string> GetRAMProperties()
        {
			var list = await _componentService.GetProperties("ram");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["ram"].Value; });
		}

		/// <summary>
		/// Get localized <see cref="SSD"/> properties from <see cref="IStringLocalizer{ComponentsController}"/>
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("ssd/properties")]
        public async Task<string> GetSSDProperties()
        {
			var list = await _componentService.GetProperties("ssd");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["ssd"].Value; });
		}

		/// <summary>
		/// Get localized <see cref="Videocard"/> properties from <see cref="IStringLocalizer{ComponentsController}"/>
		/// </summary>
		/// <returns>Dictionary of properties, their description and options</returns>
		[HttpGet, Route("videocard/properties")]
        public async Task<string> GetVideocardProperties()
        {
			var list = await _componentService.GetProperties("videocard");
			return FormatProperties(list);
			//return await Task.Run(() => { return _localizer["videocard"].Value; });
		}

		private string FormatProperties(IEnumerable<Property> list)
		{
			var res = list.ToDictionary(e => e.Name, e => new { text = e.Text, addition = e.ShowDescription, additionText = e.Description, values = e.Values });

			var json = JsonConvert.SerializeObject(res);

			return json;
		}

		#endregion

		#region RECORDS
		/// <summary>
		/// Get all records
		/// </summary>
		/// <returns><see cref="IEnumerable{Body}"/> of <see cref="Body"/> objects</returns>
		// GET api/components/bodies
		[HttpGet, Route("bodies")]
        public async Task<IEnumerable<Body>> GetAllBodies()
        {            
            return await _componentService.GetBodies();
        }

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="company">Vendor</param>
		/// <param name="formfactor">Formfactor</param>
		/// <param name="type">Type</param>
		/// <param name="buildInCharger">Charger included</param>
		/// <param name="chargerPower">Charger power</param>
		/// <param name="color">Color</param>
		/// <param name="usbPorts">USB ports amount</param>
		/// <param name="selectedMotherboard">Selected motherboard ID</param>
		/// <param name="selectedVideocard">Selected videocard ID</param>
		/// <returns><see cref="IEnumerable{Body}"/> of <see cref="Body"/> objects</returns>
		// GET api/components/body
		[HttpGet, Route("body")]
        public async Task<IEnumerable<Body>> GetBodies(
            [FromQuery(Name = "company")] string[] company,
            [FromQuery(Name = "formfactor")] string[] formfactor,
            [FromQuery(Name = "type")] string[] type,
            [FromQuery(Name = "build-in-charger")] bool[] buildInCharger,
            [FromQuery(Name = "charger-power")] string[] chargerPower,
            [FromQuery(Name = "color")] string[] color,
            [FromQuery(Name = "usb-3.0-amount")] string[] usbPorts,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard,
            [FromQuery(Name = "selected-videocard")]int? selectedVideocard)
        {
            return await _componentService.GetBodies(new GetBodiesRequest(company, formfactor, type, buildInCharger, chargerPower, 
                usbPorts, selectedMotherboard, selectedVideocard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns><see cref="IEnumerable{Charger}"/> of <see cref="Charger"/> objects</returns>
        // GET api/components/chargers
        [HttpGet, Route("chargers")]
        public async Task<IEnumerable<Charger>> GetAllChargers()
        {
            return await _componentService.GetChargers();
        }

		/// <summary>
		/// Get all records that apply
		/// </summary>
		/// <param name="company">Vendor</param>
		/// <param name="series">Series</param>
		/// <param name="power">Power</param>
		/// <param name="sertificate">Sertificate 80 Plus</param>
		/// <param name="videoConnectorsCount">Amount of video connectors</param>
		/// <param name="connectorType">Charger connector</param>
		/// <param name="sataCount">Amount of SATA connectors</param>
		/// <param name="ideCount">Amount of IDE connectors</param>
		/// <param name="motherboardConnector">Motherboard connector</param>
		/// <param name="selectedMotherboard">Selected motherboard ID</param>
		/// <param name="selectedVideocard">Selected videocard ID</param>
		/// <returns><see cref="IEnumerable{Charger}"/> of <see cref="Charger"/> objects</returns>
		// GET api/components/charger
		[HttpGet, Route("charger")]
        public async Task<IEnumerable<Charger>> GetChargers(
            [FromQuery(Name = "company")] string[] company,
            [FromQuery(Name = "series")] string[] series,
            [FromQuery(Name = "power")] string[] power, 
            [FromQuery(Name = "sertificate")] string[] sertificate, 
            [FromQuery(Name = "video-connectors-amount")] int[] videoConnectorsCount,
            [FromQuery(Name = "connector-type")] string[] connectorType,
            [FromQuery(Name = "sata-amount")] string[] sataCount,
            [FromQuery(Name = "ide-amount")] string[] ideCount,
            [FromQuery(Name = "motherboard-connector")] string[] motherboardConnector,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard,
            [FromQuery(Name = "selected-videocard")]int? selectedVideocard)
        {
            return await _componentService.GetChargers(new GetChargersRequest(company, series, power, sertificate, videoConnectorsCount,
                connectorType, sataCount, ideCount, motherboardConnector, selectedMotherboard, selectedVideocard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns><see cref="IEnumerable{Cooler}"/> of <see cref="Cooler"/> objects</returns>
        // GET api/components/coolers
        [HttpGet, Route("coolers")]
        public async Task<IEnumerable<Cooler>> GetAllCoolers()
        {
            return await _componentService.GetCoolers();
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="company">Vendor</param>
        /// <param name="purpose">Purpose (CPU/Videocard/Body/etc)</param>
        /// <param name="type">Type (Air/Water)</param>
        /// <param name="socket">Socket</param>
        /// <param name="material">Material</param>
        /// <param name="diameter">Diameter</param>
        /// <param name="connector">Charger pin</param>
        /// <param name="adjustement">Turns adjustement</param>
        /// <param name="selectedCpu">Selected CPU ID</param>
        /// <param name="selectedMotherboard">Selected Motherboard ID</param>
        /// <returns></returns>
        // GET api/components/cooler
        [HttpGet, Route("cooler")]
        public async Task<IEnumerable<Cooler>> GetCoolers(
            [FromQuery(Name = "company")]string[] company,
            [FromQuery(Name = "purpose")]string[] purpose,
            [FromQuery(Name = "type")]string[] type,
            [FromQuery(Name = "socket")]string[] socket,
            [FromQuery(Name = "material")]string[] material,
            [FromQuery(Name = "diameter")]string[] diameter,
            [FromQuery(Name = "connector")]string[] connector,
            [FromQuery(Name = "adjustement")]string[] adjustement,
            [FromQuery(Name = "selected-cpu")]int? selectedCpu,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard)
        {
            return await _componentService.GetCoolers(new GetCoolersRequest(company, purpose, type, socket, material, diameter, adjustement, selectedCpu, selectedMotherboard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of CPU objects</returns>
        // GET api/components/cpus
        [HttpGet, Route("cpus")]
        public async Task<IEnumerable<CPU>> GetAllCPUs()
        {
            return await _componentService.GetCPUs();
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="company">Vendor</param>
        /// <param name="series">Series</param>
        /// <param name="socket">Socket</param>
        /// <param name="amountOfCores">Amount of cores</param>
        /// <param name="amountOfThreads">Amount of threads</param>
        /// <param name="integratedGraphics">Integrated graphics</param>
        /// <param name="core">Core</param>
        /// <param name="deliveryType">Delivery type</param>
        /// <param name="overclocking">Overclocking</param>
        /// <param name="selectedCooler">Selected cooler ID</param>
        /// <param name="selectedMotherboard">Selected motherboard ID</param>
        /// <returns></returns>
        // GET api/components/cpu
        [HttpGet, Route("cpu")]
        public async Task<IEnumerable<CPU>> GetCPUs(
            [FromQuery(Name = "company")]string[] company,
            [FromQuery(Name = "series")]string[] series,
            [FromQuery(Name = "socket")]string[] socket,
            [FromQuery(Name = "amount-of-cores")]int[] amountOfCores,
            [FromQuery(Name = "amount-of-threads")]int[] amountOfThreads,
            [FromQuery(Name = "integrated-graphics")]bool[] integratedGraphics,
            [FromQuery(Name = "core")]string[] core,
            [FromQuery(Name = "delivery-type")]string[] deliveryType,
            [FromQuery(Name = "overclocking")]bool[] overclocking,
            [FromQuery(Name = "selected-cooler")]int? selectedCooler,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard)
        {
            return await _componentService.GetCPUs(new GetCPUsRequest(company, series, socket, amountOfCores, amountOfThreads, 
                integratedGraphics, core, deliveryType, overclocking, selectedCooler, selectedMotherboard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of HDD objects</returns>
        // GET api/components/hdds
        [HttpGet, Route("hdds")]
        public async Task<IEnumerable<HDD>> GetAllHDDs()
        {
            return await _componentService.GetHDDs();
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="company"></param>
        /// <param name="formfactor"></param>
        /// <param name="volume"></param>
        /// <param name="interface"></param>
        /// <param name="bufferVolume"></param>
        /// <param name="speed"></param>
        /// <param name="series"></param>
        /// <param name="selectedMotherboard"></param>
        /// <returns></returns>
        // GET api/components/hdd
        [HttpGet, Route("hdd")]
        public async Task<IEnumerable<HDD>> GetHDDs(
            [FromQuery(Name = "company")] string[] company,
            [FromQuery(Name = "formfactor")] string[] formfactor,
            [FromQuery(Name = "volume")] string[] volume,
            [FromQuery(Name = "interface")] string[] @interface,
            [FromQuery(Name = "buffer-volume")] int[] bufferVolume,
            [FromQuery(Name = "speed")] int[] speed,
            [FromQuery(Name = "series")] string[] series,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard)
        {
            return await _componentService.GetHDDs(new GetHDDsRequest(company, formfactor, volume, @interface, bufferVolume, speed, 
                series, selectedMotherboard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of Motherboard objects</returns>
        // GET api/components/motherboards
        [HttpGet, Route("motherboards")]
        public async Task<IEnumerable<Motherboard>> GetAllMotherboards()
        {
            return await _componentService.GetMotherboards();
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="company">Vendor</param>
        /// <param name="socket">Socket</param>
        /// <param name="chipset">Chipset</param>
        /// <param name="formfactor">Formfactor</param>
        /// <param name="memory">Memory type</param>
        /// <param name="memorySlots">Amount of memory slots</param>
        /// <param name="memoryChanels">Amount of memory chanels</param>
        /// <param name="maxMemory">Maximum memory</param>
        /// <param name="maxRamFreq">RAM maximum frequency</param>
        /// <param name="slots">Connection slots</param>
        /// <param name="series">Series</param>
        /// <param name="selectedBody">Selected body ID</param>
        /// <param name="selectedCharger">Selected charger ID</param>
        /// <param name="selectedCooler">Selected cooler ID</param>
        /// <param name="selectedCpu">Selected CPU ID</param>
        /// <param name="selectedHdd">Selected HDD ID</param>
        /// <param name="selectedRam">Selected RAM ID</param>
        /// <param name="selectedSsd">Selected SSD ID</param>
        /// <returns></returns>
        // GET api/components/motherboard
        [HttpGet, Route("motherboard")]
        public async Task<IEnumerable<Motherboard>> GetMotherboards(
            [FromQuery(Name = "company")] string[] company,
            [FromQuery(Name = "socket")] string[] socket,
            [FromQuery(Name = "chipset")] string[] chipset,
            [FromQuery(Name = "formfactor")] string[] formfactor,
            [FromQuery(Name = "memory")] string[] memory,
            [FromQuery(Name = "amount-of-memory-slots")] int[] memorySlots,
            [FromQuery(Name = "amount-of-memory-chanels")] int[] memoryChanels,
            [FromQuery(Name = "maximum-memory")] int[] maxMemory,
            [FromQuery(Name = "maximum-ram-frequency")] string[] maxRamFreq,
            [FromQuery(Name = "slots")] string[] slots,
            [FromQuery(Name = "series")] string[] series,
            [FromQuery(Name = "selected-body")]int? selectedBody,
            [FromQuery(Name = "selected-charger")]int? selectedCharger,
            [FromQuery(Name = "selected-cooler")]int? selectedCooler,
            [FromQuery(Name = "selected-cpu")]int? selectedCpu,
            [FromQuery(Name = "selected-hdd")]int? selectedHdd,
            [FromQuery(Name = "selected-ram")]int? selectedRam,
            [FromQuery(Name = "selected-ssd")]int? selectedSsd)
        {
            return await _componentService.GetMotherboards(new GetMotherboardsRequest(company, series, socket, chipset, formfactor, 
                memory, memorySlots, memoryChanels, maxMemory, maxRamFreq, slots, selectedCharger, selectedCooler, selectedRam,
                selectedSsd, selectedBody, selectedHdd, selectedCpu));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of RAM objects</returns>
        // GET api/components/rams
        [HttpGet, Route("rams")]
        public async Task<IEnumerable<RAM>> GetAllRAMs()
        {
            return await _componentService.GetRAMs();
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="company">Vendor</param>
        /// <param name="frequency">Frequecny</param>
        /// <param name="memoryType">Memory type</param>
        /// <param name="purpose">Purpose</param>
        /// <param name="memoryVolume">Memory capacity</param>
        /// <param name="modulesAmount">Amount of modules</param>
        /// <param name="series">Series</param>
        /// <param name="cl">CAS Latency</param>
        /// <param name="selectedMotherboard">Selected motherboard ID</param>
        /// <returns></returns>
        // GET api/components/ram
        [HttpGet, Route("ram")]
        public async Task<IEnumerable<RAM>> GetRAMs(
            [FromQuery(Name="company")]string[] company,
            [FromQuery(Name="frequency")]int[] frequency,
            [FromQuery(Name="memory-type")]string[] memoryType,
            [FromQuery(Name="purpose")]string[] purpose,
            [FromQuery(Name="memory-volume")]int[] memoryVolume,
            [FromQuery(Name="modules-amount")]int[] modulesAmount,
            [FromQuery(Name="series")]string[] series,
            [FromQuery(Name="cas-latency")]string[] cl,
            [FromQuery(Name="selected-motherboard")]int? selectedMotherboard)
        {
            return await _componentService.GetRAMs(new GetRAMsRequest(company, series, memoryType, purpose, memoryVolume, 
                modulesAmount, frequency, cl, selectedMotherboard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of SSD objects</returns>
        // GET api/components/ssds
        [HttpGet, Route("ssds")]
        public async Task<IEnumerable<SSD>> GetAllSSDs()
        {
            return await _componentService.GetSSDs();
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="volume">Memory volume</param>
        /// <param name="company">Vendor</param>
        /// <param name="formfactor">Formfactor</param>
        /// <param name="interface">Connection interface</param>
        /// <param name="cellType">Cell type</param>
        /// <param name="series">Series</param>
        /// <param name="selectedMotherboard">Selected motherboard ID</param>
        /// <returns></returns>
        // GET api/components/ssd
        [HttpGet, Route("ssd")]
        public async Task<IEnumerable<SSD>> GetSSDs(
            [FromQuery(Name = "volume")] string[] volume,
            [FromQuery(Name = "company")] string[] company,
            [FromQuery(Name = "formfactor")] string[] formfactor,
            [FromQuery(Name = "interface")] string[] @interface,
            [FromQuery(Name = "cell-type")] string[] cellType,
            [FromQuery(Name = "series")] string[] series,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard)
        {
            return await _componentService.GetSSDs(new GetSSDsRequest(company, series, volume, formfactor, @interface, cellType,
                selectedMotherboard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of Videocard objects</returns>
        // GET api/components/videocards
        [HttpGet, Route("videocards")]
        public async Task<IEnumerable<Videocard>> GetAllVideocards()
        {
            return await _componentService.GetVideocards();
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="gpu">GPU</param>
        /// <param name="company">Vendor</param>
        /// <param name="capacity">Memory capacity</param>
        /// <param name="vram">VRAM</param>
        /// <param name="connectors">Connectors</param>
        /// <param name="series">Series</param>
        /// <param name="family">Family</param>
        /// <param name="selectedBody">Selected body ID</param>
        /// <param name="selectedCharger">Selected charger ID</param>
        /// <returns></returns>
        // GET api/components/videocard
        [HttpGet, Route("videocard")]
        public async Task<IEnumerable<Videocard>> GetVideocards(
            [FromQuery(Name = "graphical-proccessor")]string[] gpu,
            [FromQuery(Name = "company")]string[] company,
            [FromQuery(Name = "capacity")]int[] capacity,
            [FromQuery(Name = "vram")]string[] vram,
            [FromQuery(Name = "connectors")]string[] connectors,
            [FromQuery(Name = "series")]string[] series,
            [FromQuery(Name = "family")]string[] family,
            [FromQuery(Name = "selected-body")]int? selectedBody,
            [FromQuery(Name = "selected-charger")]int? selectedCharger)
        {
            return await _componentService.GetVideocards(new GetVideocardsRequest(company, series, gpu, vram, capacity, connectors, 
                family, selectedCharger, selectedBody));
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        // GET api/components/body/5
        [HttpGet("body/{id}")]
        public async Task<Body> GetBodyById(int id)
        {
            return await _componentService.GetBody(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("charger/{id}")]
        public async Task<Charger> GetChargerById(int id)
        {
            return await _componentService.GetCharger(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("cooler/{id}")]
        public async Task<Cooler> GetCoolerById(int id)
        {
            return await _componentService.GetCooler(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("cpu/{id}")]
        public async Task<CPU> GetCPUById(int id)
        {
            return await _componentService.GetCPU(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("hdd/{id}")]
        public async Task<HDD> GetHDDById(int id)
        {
            return await _componentService.GetHDD(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("motherboard/{id}")]
        public async Task<Motherboard> GetMotherboardById(int id)
        {
            return await _componentService.GetMotherboard(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("ram/{id}")]
        public async Task<RAM> GetRAMById(int id)
        {
            return await _componentService.GetRAM(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("ssd/{id}")]
        public async Task<SSD> GetSSDById(int id)
        {
            return await _componentService.GetSSD(id);
        }

        /// <summary>
        /// Get record by ID. Return null if ID is incorrect
        /// </summary>
        /// <param name="id">ID of record</param>
        /// <returns>Object or null</returns>
        [HttpGet("videocard/{id}")]
        public async Task<Videocard> GetVideocardById(int id)
        {
            return await _componentService.GetVideocard(id);
        }

		#endregion

		[HttpGet("description/{component}")]
		public IDictionary<string, string> GetComponentDescription(string component)
		{
			return _componentService.GetDescription(component);
		}
		#endregion

		#region POST
		// POST api/values
		[Authorize (Roles = "ADMIN")]
		[HttpPost("body")]
        public async Task<IEnumerable<Body>> PostBody([FromBody]Body value)
        {
			if (User.Identity.IsAuthenticated)
			{
				return await _componentService.AddBody(new AddBodyRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPost("charger")]
        public async Task<IEnumerable<Charger>> PostCharger([FromBody]Charger value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddCharger(new AddChargerRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[Authorize(Roles = "ADMIN")]
		[HttpPost("cooler")]
        public async Task<IEnumerable<Cooler>> PostCooler([FromBody]Cooler value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddCooler(new AddCoolerRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[Authorize(Roles = "ADMIN")]
		[HttpPost("cpu")]
        public async Task<IEnumerable<CPU>> PostCPU([FromBody]CPU value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddCPU(new AddCpuRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[Authorize(Roles = "ADMIN")]
		[HttpPost("hdd")]
        public async Task<IEnumerable<HDD>> PostHDD([FromBody]HDD value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddHDD(new AddHddRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[Authorize(Roles = "ADMIN")]
		[HttpPost("motherboard")]
        public async Task<IEnumerable<Motherboard>> PostMotherboard([FromBody]Motherboard value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddMotherboard(new AddMotherboardRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[Authorize(Roles = "ADMIN")]
		[HttpPost("ram")]
        public async Task<IEnumerable<RAM>> PostRAM([FromBody]RAM value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddRAM(new AddRamRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[Authorize(Roles = "ADMIN")]
		[HttpPost("ssd")]
        public async Task<IEnumerable<SSD>> PostSSD([FromBody]SSD value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddSSD(new AddSsdRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[Authorize(Roles = "ADMIN")]
		[HttpPost("videocard")]
        public async Task<IEnumerable<Videocard>> PostVideocard([FromBody]Videocard value)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _componentService.AddVideocard(new AddVideocardRequest(value));
            }
            else
            {
                throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
            }
        }

		[HttpPost("body/properties")]
		public async Task<string> PostBodyProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("body", dict);

			return await GetBodyProperties();
		}

		[HttpPost("charger/properties")]
		public async Task<string> PostChargerProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("charger", dict);

			return await GetChargerProperties();
		}

		[HttpPost("cooler/properties")]
		public async Task<string> PostCoolerProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("cooler", dict);

			return await GetCoolerProperties();
		}

		[HttpPost("cpu/properties")]
		public async Task<string> PostCPUProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("cpu", dict);

			return await GetCPUProperties();
		}

		[HttpPost("hdd/properties")]
		public async Task<string> PostHDDProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("hdd", dict);

			return await GetHDDProperties();
		}

		[HttpPost("motherboard/properties")]
		public async Task<string> PostMotherboardProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("motherboard", dict);

			return await GetMotherboardProperties();
		}

		[HttpPost("ram/properties")]
		public async Task<string> PostRAMProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("ram", dict);

			return await GetRAMProperties();
		}

		[HttpPost("ssd/properties")]
		public async Task<string> PostSSDProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("ssd", dict);

			return await GetSSDProperties();
		}

		[HttpPost("videocard/properties")]
		public async Task<string> PostVideocardProperties([FromBody]Dictionary<string, Property> dict)
		{
			await PostProperties("videocard", dict);

			return await GetVideocardProperties();
		}

		private async Task PostProperties(string component, Dictionary<string, Property> dict)
		{
			foreach (var kv in dict)
			{
				kv.Value.Name = kv.Key;
				kv.Value.Component = component;
				await _componentService.AddProperty(component, kv.Value);
			}
		}
		#endregion

		#region PUT
		[Authorize(Roles = "ADMIN")]
		[HttpPut("body/{id}")]
        public async Task<IEnumerable<Body>> PutBody(int id, [FromBody]Body value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceBody(new UpdateBodyRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("charger/{id}")]
        public async Task<IEnumerable<Charger>> PutCharger(int id, [FromBody]Charger value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceCharger(new UpdateChargerRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("cooler/{id}")]
        public async Task<IEnumerable<Cooler>> PutCooler(int id, [FromBody]Cooler value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceCooler(new UpdateCoolerRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("cpu/{id}")]
        public async Task<IEnumerable<CPU>> PutCPU(int id, [FromBody]CPU value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceCPU(new UpdateCPURequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("hdd/{id}")]
        public async Task<IEnumerable<HDD>> PutHDD(int id, [FromBody]HDD value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceHDD(new UpdateHDDRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("motherboard/{id}")]
        public async Task<IEnumerable<Motherboard>> PutMotherboard(int id, [FromBody]Motherboard value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceMotherboard(new UpdateMotherboardRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("ram/{id}")]
        public async Task<IEnumerable<RAM>> PutRAM(int id, [FromBody]RAM value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceRAM(new UpdateRAMRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("ssd/{id}")]
        public async Task<IEnumerable<SSD>> PutSSD(int id, [FromBody]SSD value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceSSD(new UpdateSSDRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("videocard/{id}")]
        public async Task<IEnumerable<Videocard>> PutVideocard(int id, [FromBody]Videocard value)
        {
			if (User.Identity.IsAuthenticated)
			{
				value.ID = id;
				return await _componentService.ReplaceVideocard(new UpdateVideocardRequest(value));
			}
			else
			{
				throw new UnauthorizedAccessException("Only admin can make changes. Have a nice day and fuck off ;)");
			}
		}
		#endregion

		#region DELETE
		// DELETE api/values/5
		[Authorize(Roles = "ADMIN")]
		[HttpDelete("body/{id}")]
        public async Task<IEnumerable<Body>> DeleteBody(int id)
        {
			return await _componentService.RemoveBody(new RemoveBodyRequest(id));
        }

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("charger/{id}")]
        public async Task<IEnumerable<Charger>> DeleteCharger(int id)
        {
			return await _componentService.RemoveCharger(new RemoveChargerRequest(id));
		}

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("cooler/{id}")]
        public async Task<IEnumerable<Cooler>> DeleteCooler(int id)
        {
			return await _componentService.RemoveCooler(new RemoveCoolerRequest(id));
		}

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("cpu/{id}")]
        public async Task<IEnumerable<CPU>> DeleteCPU(int id)
        {
			return await _componentService.RemoveCPU(new RemoveCPURequest(id));
		}

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("hdd/{id}")]
        public async Task<IEnumerable<HDD>> DeleteHDD(int id)
        {
			return await _componentService.RemoveHDD(new RemoveHDDRequest(id));
		}

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("motherboard/{id}")]
        public async Task<IEnumerable<Motherboard>> DeleteMotherboard(int id)
        {
			return await _componentService.RemoveMotherboard(new RemoveMotherboardRequest(id));
		}

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("ram/{id}")]
        public async Task<IEnumerable<RAM>> DeleteRAM(int id)
        {
			return await _componentService.RemoveRAM(new RemoveRAMRequest(id));
		}

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("ssd/{id}")]
        public async Task<IEnumerable<SSD>> DeleteSSD(int id)
        {
			return await _componentService.RemoveSSD(new RemoveSSDRequest(id));
		}

		/// <summary>
		/// Delete <see cref="Videocard"/> with specified ID
		/// </summary>
		/// <param name="id">Record ID</param>
		/// <returns>Updated <see cref="IEnumerable{Videocard}"/> of <see cref="Videocard"/></returns>
		[Authorize(Roles = "ADMIN")]
		[HttpDelete("videocard/{id}")]
        public async Task<IEnumerable<Videocard>> DeleteVideocard(int id)
        {
			return await _componentService.RemoveVideocard(new RemoveVideocardRequest(id));
		}

		[HttpGet("compatibility/rules")]
		public async Task<IEnumerable<Rule>> GetCompatibilityRules([FromQuery(Name = "component")]string component)
		{
			return await _componentService.GetRules(component);
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPost("compatibility/rules")]
		public async Task AddCompatibilityRule([FromBody]Rule rule)
		{
			await _componentService.AddRule(rule);
		}

		[Authorize(Roles = "ADMIN")]
		[HttpDelete("compatibility/rules")]
		public async Task DeleteCompatibilityRule([FromBody]Rule rule)
		{
			await _componentService.DeleteRule(rule);
		}

		[Authorize(Roles = "ADMIN")]
		[HttpPut("compatibility/rules")]
		public async Task UpdateCompatibilityRule([FromBody]Tuple<Rule, Rule> rule)
		{
			await _componentService.UpdateRule(rule.Item1, rule.Item2);
		}

		[HttpGet("compatibility/rules/relations")]
		public async Task<IEnumerable<RuleRelation>> GetCompatibilityRuleRelations()
		{
			return await _componentService.GetRelations();
		}
		#endregion
	}
}