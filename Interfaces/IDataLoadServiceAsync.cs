using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Interfaces
{
	/// <summary>
	/// Interface of data loading provider service
	/// </summary>
	public interface IDataLoadServiceAsync
	{
		Task<IEnumerable<Body>> LoadBodies(string data);
		Task<IEnumerable<Charger>> LoadChargers(string data);
		Task<IEnumerable<Cooler>> LoadCoolers(string data);
		Task<IEnumerable<CPU>> LoadCPUs(string data);
		Task<IEnumerable<HDD>> LoadHDDs(string data);
		Task<IEnumerable<Motherboard>> LoadMotherboards(string data);
		Task<IEnumerable<RAM>> LoadRAMs(string data);
		Task<IEnumerable<SSD>> LoadSSDs(string data);
		Task<IEnumerable<Videocard>> LoadVideocards(string data);
	}
}
