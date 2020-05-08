using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Services
{
	public class CSVDataLoadServiceAsync : IDataLoadServiceAsync
	{
		public Task<IEnumerable<Body>> LoadBodies(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}

			for (int i = 1; i < input.Length; i++)
			{
				string[] values = input[i].Split(",");
			}

			throw new NotImplementedException();
		}

		public Task<IEnumerable<Charger>> LoadChargers(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Cooler>> LoadCoolers(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CPU>> LoadCPUs(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}

		public Task<IEnumerable<HDD>> LoadHDDs(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Motherboard>> LoadMotherboards(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}

		public Task<IEnumerable<RAM>> LoadRAMs(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SSD>> LoadSSDs(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Videocard>> LoadVideocards(string data)
		{
			string[] input = data.Split(";");

			List<string> properties = new List<string>();

			foreach (string s in input[0].Split(","))
			{
				properties.Add(s);
			}
			throw new NotImplementedException();
		}
	}
}
