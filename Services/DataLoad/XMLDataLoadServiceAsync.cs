using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Services
{
	public class XMLDataLoadServiceAsync : IDataLoadServiceAsync
	{
		public Task<IEnumerable<Body>> LoadBodies(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Charger>> LoadChargers(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Cooler>> LoadCoolers(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CPU>> LoadCPUs(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<HDD>> LoadHDDs(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Motherboard>> LoadMotherboards(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<RAM>> LoadRAMs(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SSD>> LoadSSDs(string data)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Videocard>> LoadVideocards(string data)
		{
			throw new NotImplementedException();
		}
	}
}
