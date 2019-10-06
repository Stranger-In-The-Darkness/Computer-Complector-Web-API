using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
    public class AddMotherboardRequest : AddRequestBase
    {
        private Motherboard _element;
        private List<SqlParameter> _parameters;

        public AddMotherboardRequest(Motherboard element)
        {
            _element = element;

            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@cpuComp", _element.CPUCompany)
            };

            if (Validate(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate(_element.Socket, "element.Socket"))
            {
                _parameters.Add(new SqlParameter("@socket", _element.Socket));
            }

            if (Validate(_element.Chipset, "element.Chipset"))
            {
                _parameters.Add(new SqlParameter("chipset", _element.Chipset));
            }

            if (Validate(_element.Formfactor, "element.Formfactor"))
            {
                _parameters.Add(new SqlParameter("@formfactor", _element.Formfactor));
            }

            if (Validate(_element.MemoryType, "element.MemoryType"))
            {
                _parameters.Add(new SqlParameter("@memoryType", _element.MemoryType));
            }

            if (Validate(_element.MemorySlotsAmount, "element.MemorySlotsAmount"))
            {
                _parameters.Add(new SqlParameter("@memorySlots", _element.MemorySlotsAmount));
            }

            if (Validate(_element.MemoryChanelsAmount, "element.MemoryChanelsAmount"))
            {
                _parameters.Add(new SqlParameter("@memoryChanels", _element.MemoryChanelsAmount));
            }

            if (Validate(_element.MaxMemory, "element.MaxMemory"))
            {
                _parameters.Add(new SqlParameter("@maxMemory", _element.MaxMemory));
            }

            if (Validate(_element.RAMMaxFreq, "element.RAMMaxFreq"))
            {
                _parameters.Add(new SqlParameter("@maxRAMFreq", _element.RAMMaxFreq));
            }

            if (Validate(_element.Series, "element.Series"))
            {
                _parameters.Add(new SqlParameter("@series", _element.Series));
            }

            if (Validate(_element.Pin, "element.Pin"))
            {
                _parameters.Add(new SqlParameter("@pin", _element.Pin));
            }

            if (Validate(_element.CPUPin, "element.CPUPin"))
            {
                _parameters.Add(new SqlParameter("@cpuPin", _element.CPUPin));
            }

            for (int i = 0; i<_element.Slots.Count; i++)
            {
                Expression += $"INSERT INTO MOTHERBOARD_SLOTS VALUES (SELECT TOP ! ID FROM MOTHERBOARD WHERE Title = @title, @slot{i});";
                _parameters.Add(new SqlParameter($"@slot{i}", _element.Slots[i]));
            }
        }

        public string Expression { get; private set; } = 
            "INSERT INTO MOTHERBOARD VALUES (@title, @socket, @company, @chipset, @cpuComp, @formfactor, @memoryType, @memorySlots, @memoryChanels, @maxMemory, @maxRAMFreq, @series, @pin, @cpuPin);";

        public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		public Motherboard Motherboard { get => _element; }
    }
}
