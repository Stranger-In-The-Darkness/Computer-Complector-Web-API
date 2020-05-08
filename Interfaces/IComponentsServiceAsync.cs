using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.Models;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Add;
using ComputerComplectorWebAPI.Models.Requests.Get;
using ComputerComplectorWebAPI.Models.Requests.Remove;
using ComputerComplectorWebAPI.Models.Requests.Update;

namespace ComputerComplectorWebAPI.Interfaces
{
	/// <summary>
	/// Interface of asynchronous components provider service
	/// </summary>
	public interface IComponentsServiceAsync
    {
        Task<IEnumerable<Body>> GetBodies();
        Task<IEnumerable<Body>> GetBodies(GetBodiesRequest request);
        Task<Body> GetBody(int id);
        Task<IEnumerable<Body>> AddBody(AddBodyRequest request);
		Task<IEnumerable<Body>> RemoveBody(RemoveBodyRequest request);
		Task<IEnumerable<Body>> ReplaceBody(UpdateBodyRequest request);

		Task<IEnumerable<Charger>> GetChargers();
        Task<IEnumerable<Charger>> GetChargers(GetChargersRequest request);
        Task<Charger> GetCharger(int id);
        Task<IEnumerable<Charger>> AddCharger(AddChargerRequest request);
		Task<IEnumerable<Charger>> RemoveCharger(RemoveChargerRequest request);
		Task<IEnumerable<Charger>> ReplaceCharger(UpdateChargerRequest request);

		Task<IEnumerable<Cooler>> GetCoolers();
        Task<IEnumerable<Cooler>> GetCoolers( GetCoolersRequest request);
        Task<Cooler> GetCooler(int id);
        Task<IEnumerable<Cooler>> AddCooler(AddCoolerRequest request);
		Task<IEnumerable<Cooler>> RemoveCooler(RemoveCoolerRequest request);
		Task<IEnumerable<Cooler>> ReplaceCooler(UpdateCoolerRequest request);

		Task<IEnumerable<CPU>> GetCPUs();
        Task<IEnumerable<CPU>> GetCPUs(GetCPUsRequest request);
        Task<CPU> GetCPU(int id);
        Task<IEnumerable<CPU>> AddCPU(AddCpuRequest request);
		Task<IEnumerable<CPU>> RemoveCPU(RemoveCPURequest request);
		Task<IEnumerable<CPU>> ReplaceCPU(UpdateCPURequest request);

		Task<IEnumerable<HDD>> GetHDDs();
        Task<IEnumerable<HDD>> GetHDDs(GetHDDsRequest request);
        Task<HDD> GetHDD(int id);
        Task<IEnumerable<HDD>> AddHDD(AddHddRequest request);
		Task<IEnumerable<HDD>> RemoveHDD(RemoveHDDRequest request);
		Task<IEnumerable<HDD>> ReplaceHDD(UpdateHDDRequest request);

		Task<IEnumerable<Motherboard>> GetMotherboards();
        Task<IEnumerable<Motherboard>> GetMotherboards(GetMotherboardsRequest request);
        Task<Motherboard> GetMotherboard(int id);
        Task<IEnumerable<Motherboard>> AddMotherboard(AddMotherboardRequest request);
		Task<IEnumerable<Motherboard>> RemoveMotherboard(RemoveMotherboardRequest request);
		Task<IEnumerable<Motherboard>> ReplaceMotherboard(UpdateMotherboardRequest request);

		Task<IEnumerable<RAM>> GetRAMs();
        Task<IEnumerable<RAM>> GetRAMs(GetRAMsRequest request);
        Task<RAM> GetRAM(int id);
        Task<IEnumerable<RAM>> AddRAM(AddRamRequest request);
		Task<IEnumerable<RAM>> RemoveRAM(RemoveRAMRequest request);
		Task<IEnumerable<RAM>> ReplaceRAM(UpdateRAMRequest request);

		Task<IEnumerable<SSD>> GetSSDs();
        Task<IEnumerable<SSD>> GetSSDs(GetSSDsRequest request);
        Task<SSD> GetSSD(int id);
        Task<IEnumerable<SSD>> AddSSD(AddSsdRequest request);
		Task<IEnumerable<SSD>> RemoveSSD(RemoveSSDRequest request);
		Task<IEnumerable<SSD>> ReplaceSSD(UpdateSSDRequest request);

		Task<IEnumerable<Videocard>> GetVideocards();
        Task<IEnumerable<Videocard>> GetVideocards(GetVideocardsRequest request);
        Task<Videocard> GetVideocard(int id);
        Task<IEnumerable<Videocard>> AddVideocard(AddVideocardRequest request);
		Task<IEnumerable<Videocard>> RemoveVideocard(RemoveVideocardRequest request);
		Task<IEnumerable<Videocard>> ReplaceVideocard(UpdateVideocardRequest request);

		Task<IEnumerable<Property>> GetProperties(string component);
		Task<IEnumerable<Property>> AddProperty(string component, Property property);
		Task<IEnumerable<Property>> AddPropertyValue(string component, string propertyName, string value);
		Task<IEnumerable<Property>> AddPropertiesFromJSON(string component, string json);
		Task<IEnumerable<Property>> RemoveProperty(string component, Property property);
		Task<IEnumerable<Property>> RemovePropertyValue(string component, string propertyName, string value);
		Task<IEnumerable<Property>> ChangeProperty(string component, Property oldProperty, Property newProperty);

		IDictionary<string, string> GetDescription(string component);
	}
}
