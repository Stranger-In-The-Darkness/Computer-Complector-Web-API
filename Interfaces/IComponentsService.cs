using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.Models;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Get;

namespace ComputerComplectorWebAPI.Interfaces
{
    /// <summary>
    /// Interface of synchronous components provider service
    /// </summary>
    public interface IComponentsService
    {
        IEnumerable<Body> GetBodies();
        IEnumerable<Body> GetBodies(GetBodiesRequest request);
        Body GetBody(int id);

        IEnumerable<Charger> GetChargers();
        IEnumerable<Charger> GetChargers(GetChargersRequest request);
        Charger GetCharger(int id);

        IEnumerable<Cooler> GetCoolers();
        IEnumerable<Cooler> GetCoolers( GetCoolersRequest request);
        Cooler GetCooler(int id);

        IEnumerable<CPU> GetCPUs();
        IEnumerable<CPU> GetCPUs(GetCPUsRequest request);
        CPU GetCPU(int id);

        IEnumerable<HDD> GetHDDs();
        IEnumerable<HDD> GetHDDs(GetHDDsRequest request);
        HDD GetHDD(int id);

        IEnumerable<Motherboard> GetMotherboards();
        IEnumerable<Motherboard> GetMotherboards(GetMotherboardsRequest request);
        Motherboard GetMotherboard(int id);

        IEnumerable<RAM> GetRAMs();
        IEnumerable<RAM> GetRAMs(GetRAMsRequest request);
        RAM GetRAM(int id);

        IEnumerable<SSD> GetSSDs();
        IEnumerable<SSD> GetSSDs(GetSSDsRequest request);
        SSD GetSSD(int id);

        IEnumerable<Videocard> GetVideocards();
        IEnumerable<Videocard> GetVideocards(GetVideocardsRequest request);
        Videocard GetVideocard(int id);

        string GetParameters(string component, string language);
    }
}
