using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.Models;

namespace ComputerComplectorWebAPI.Interfaces
{
    /// <summary>
    /// Asynchronous user-controller
    /// </summary>
    public interface IComponentsServiceAsync
    {
        Task<IEnumerable<Body>> GetBodies();
        Task<IEnumerable<Body>> GetBodies(GetBodiesRequest request);
        Task<Body> GetBody(int id);

        Task<IEnumerable<Charger>> GetChargers();
        Task<IEnumerable<Charger>> GetChargers(GetChargersRequest request);
        Task<Charger> GetCharger(int id);

        Task<IEnumerable<Cooler>> GetCoolers();
        Task<IEnumerable<Cooler>> GetCoolers( GetCoolersRequest request);
        Task<Cooler> GetCooler(int id);

        Task<IEnumerable<CPU>> GetCPUs();
        Task<IEnumerable<CPU>> GetCPUs(GetCPUsRequest request);
        Task<CPU> GetCPU(int id);

        Task<IEnumerable<HDD>> GetHDDs();
        Task<IEnumerable<HDD>> GetHDDs(GetHDDsRequest request);
        Task<HDD> GetHDD(int id);

        Task<IEnumerable<Motherboard>> GetMotherboards();
        Task<IEnumerable<Motherboard>> GetMotherboards(GetMotherboardsRequest request);
        Task<Motherboard> GetMotherboard(int id);

        Task<IEnumerable<RAM>> GetRAMs();
        Task<IEnumerable<RAM>> GetRAMs(GetRAMsRequest request);
        Task<RAM> GetRAM(int id);

        Task<IEnumerable<SSD>> GetSSDs();
        Task<IEnumerable<SSD>> GetSSDs(GetSSDsRequest request);
        Task<SSD> GetSSD(int id);

        Task<IEnumerable<Videocard>> GetVideocards();
        Task<IEnumerable<Videocard>> GetVideocards(GetVideocardsRequest request);
        Task<Videocard> GetVideocard(int id);
    }
}
