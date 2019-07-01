using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models;

using Microsoft.Extensions.Configuration;

namespace ComputerComplectorWebAPI.Controllers
{
    /// <summary>
    /// Components service user controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : Controller
    {
        private IComponentsServiceAsync _componentService;

        public ComponentsController(IComponentsServiceAsync componentService)
        {
            _componentService = componentService;
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("body/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetBodyProperties()
        {
            return await _componentService.GetParameters("body");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("charger/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetChargerProperties()
        {
            return await _componentService.GetParameters("charger");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("cooler/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetCoolerProperties()
        {
            return await _componentService.GetParameters("cooler");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("cpu/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetCPUProperties()
        {
            return await _componentService.GetParameters("cpu");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("hdd/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetHDDProperties()
        {
            return await _componentService.GetParameters("hdd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("motherboard/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetMotherboardProperties()
        {
            return await _componentService.GetParameters("motherboard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("ram/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetRAMProperties()
        {
            return await _componentService.GetParameters("ram");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("ssd/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetSSDProperties()
        {
            return await _componentService.GetParameters("ssd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Dictionary of properties? their description and options</returns>
        [HttpGet, Route("videocard/properties")]
        public async Task<Dictionary<string, (bool, string, List<string>)>> GetVideocardProperties()
        {
            return await _componentService.GetParameters("videocard");
        }
        #endregion

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of Body objects</returns>
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
        /// <returns></returns>
        // GET api/components/body
        [HttpGet, Route("body")]
        public async Task<IEnumerable<Body>> GetBodies(
            [FromQuery(Name = "company")] string company,
            [FromQuery(Name = "formfactor")] string formfactor,
            [FromQuery(Name = "type")] string type,
            [FromQuery(Name = "build-in-charger")] bool? buildInCharger,
            [FromQuery(Name = "charger-power")] string chargerPower,
            [FromQuery(Name = "color")] string color,
            [FromQuery(Name = "usb-3.0-amount")] string usbPorts,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard,
            [FromQuery(Name = "selected-videocard")]int? selectedVideocard)
        {
            return await _componentService.GetBodies(new GetBodiesRequest(company, formfactor, type, buildInCharger, chargerPower, 
                usbPorts, selectedMotherboard, selectedVideocard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of Charger objects</returns>
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
        /// <returns></returns>
        // GET api/components/charger
        [HttpGet, Route("charger")]
        public async Task<IEnumerable<Charger>> GetChargers(
            [FromQuery(Name = "company")] string company,
            [FromQuery(Name = "series")] string series,
            [FromQuery(Name = "power")] string power, 
            [FromQuery(Name = "sertificate")] string sertificate, 
            [FromQuery(Name = "video-connectors-amount")] int? videoConnectorsCount,
            [FromQuery(Name = "connector-type")] string connectorType,
            [FromQuery(Name = "sata-amount")] string sataCount,
            [FromQuery(Name = "ide-amount")] string ideCount,
            [FromQuery(Name = "motherboard-connector")] string motherboardConnector,
            [FromQuery(Name = "selected-motherboard")]int? selectedMotherboard,
            [FromQuery(Name = "selected-videocard")]int? selectedVideocard)
        {
            return await _componentService.GetChargers(new GetChargersRequest(company, series, power, sertificate, videoConnectorsCount,
                connectorType, sataCount, ideCount, motherboardConnector, selectedMotherboard, selectedVideocard));
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>IEnumerable of Cooler objects</returns>
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
            [FromQuery(Name = "company")]string company,
            [FromQuery(Name = "purpose")]string purpose,
            [FromQuery(Name = "type")]string type,
            [FromQuery(Name = "socket")]string socket,
            [FromQuery(Name = "material")]string material,
            [FromQuery(Name = "diameter")]string diameter,
            [FromQuery(Name = "connector")]string connector,
            [FromQuery(Name = "adjustement")]string adjustement,
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
            [FromQuery(Name = "company")]string company,
            [FromQuery(Name = "series")]string series,
            [FromQuery(Name = "socket")]string socket,
            [FromQuery(Name = "amount-of-cores")]int? amountOfCores,
            [FromQuery(Name = "amount-of-threads")]int? amountOfThreads,
            [FromQuery(Name = "integrated-graphics")]bool? integratedGraphics,
            [FromQuery(Name = "core")]string core,
            [FromQuery(Name = "delivery-type")]string deliveryType,
            [FromQuery(Name = "overclocking")]bool? overclocking,
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
            [FromQuery(Name = "company")] string company,
            [FromQuery(Name = "formfactor")] string formfactor,
            [FromQuery(Name = "volume")] string volume,
            [FromQuery(Name = "interface")] string @interface,
            [FromQuery(Name = "buffer-volume")] int? bufferVolume,
            [FromQuery(Name = "speed")] int? speed,
            [FromQuery(Name = "series")] string series,
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
            [FromQuery(Name = "company")] string company,
            [FromQuery(Name = "socket")] string socket,
            [FromQuery(Name = "chipset")] string chipset,
            [FromQuery(Name = "formfactor")] string formfactor,
            [FromQuery(Name = "memory")] string memory,
            [FromQuery(Name = "amount-of-memory-slots")] int? memorySlots,
            [FromQuery(Name = "amount-of-memory-chanels")] int? memoryChanels,
            [FromQuery(Name = "maximum-memory")] int? maxMemory,
            [FromQuery(Name = "maximum-ram-frequency")] string maxRamFreq,
            [FromQuery(Name = "slots")] string slots,
            [FromQuery(Name = "series")] string series,
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
            [FromQuery(Name="company")]string company,
            [FromQuery(Name="frequency")]int? frequency,
            [FromQuery(Name="memory-type")]string memoryType,
            [FromQuery(Name="purpose")]string purpose,
            [FromQuery(Name="memory-volume")]int? memoryVolume,
            [FromQuery(Name="modules-amount")]int? modulesAmount,
            [FromQuery(Name="series")]string series,
            [FromQuery(Name="cas-latency")]string cl,
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
            [FromQuery(Name = "volume")] string volume,
            [FromQuery(Name = "company")] string company,
            [FromQuery(Name = "formfactor")] string formfactor,
            [FromQuery(Name = "interface")] string @interface,
            [FromQuery(Name = "cell-type")] string cellType,
            [FromQuery(Name = "series")] string series,
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
            [FromQuery(Name = "graphical-proccessor")]string gpu,
            [FromQuery(Name = "company")]string company,
            [FromQuery(Name = "capacity")]int? capacity,
            [FromQuery(Name = "vram")]string vram,
            [FromQuery(Name = "connectors")]string connectors,
            [FromQuery(Name = "series")]string series,
            [FromQuery(Name = "family")]string family,
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

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

    }
}