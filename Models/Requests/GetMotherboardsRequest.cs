using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    public class GetMotherboardsRequest
    {
        public string   Company             { get; set; }
        public string   Series              { get; set; }
	    public string   Socket              { get; set; }
	    public string   Chipset             { get; set; }
        //public string   CPUCompany          { get; set; }
	    public string   Formfactor          { get; set; }
        public string   Memory              { get; set; }
        public int?     MemorySlotsAmount   { get; set; }
        public int?     MemoryChanelsAmount { get; set; }
        public int?     MaxMemory           { get; set; }
        public string   RAMMaxFreq          { get; set; }
        public string   Slots               { get; set; }

        public int? SelectedBody { get; private set; }
        public int? SelectedCharger { get; private set; }
        public int? SelectedCooler { get; private set; }
        public int? SelectedRam { get; private set; }
        public int? SelectedSsd { get; private set; }
        public int? SelectedHdd { get; private set; }
        public int? SelectedCpu { get; private set; }

        public string Expression { get; } = null;
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetMotherboardsRequest(string company, string series, string socket, string chipset, string formfactor, 
            string memory, int? memorySlotsAmount, int? memoryChanelsAmount, int? maxMemory, string ramMaxFreq, string slots,
            int? selectedCharger, int? selectedCooler, int? selectedRam, int? selectedSsd, int? selectedBody, int? selectedHdd,
            int? selectedCpu)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                cond.Add("Company=@company");
                Parameters.Add(new SqlParameter("@company", Company));
            }

            Series = series;
            if (Series != null)
            {
                cond.Add("Series=@series");
                Parameters.Add(new SqlParameter("@series", Series));
            }

            Socket = socket;
            if (Socket != null)
            {
                cond.Add("Socket=@socket");
                Parameters.Add(new SqlParameter("@socket", Socket));
            }

            Chipset = chipset;
            if (Chipset != null)
            {
                cond.Add("Chipset=@chipset");
                Parameters.Add(new SqlParameter("@chipset", Chipset));
            }

            Formfactor = formfactor;
            if (Formfactor != null)
            {
                cond.Add("Formfactor=@formfactor");
                Parameters.Add(new SqlParameter("@formfactor", Formfactor));
            }

            Memory = memory;
            if (Memory != null)
            {
                cond.Add("MemoryType=@memory");
                Parameters.Add(new SqlParameter("@memory", Memory));
            }

            MemorySlotsAmount = memorySlotsAmount;
            if (MemorySlotsAmount != null)
            {
                cond.Add("AmountOfMemorySlots=@memorySlotsAmount");
                Parameters.Add(new SqlParameter("@memorySlotsAmount", MemorySlotsAmount));
            }

            MemoryChanelsAmount = memoryChanelsAmount;
            if (MemoryChanelsAmount != null)
            {
                cond.Add("AmountOfMemoryChanels=@memoryChanelsAmount");
                Parameters.Add(new SqlParameter("@memoryChanelsAmount", MemoryChanelsAmount));
            }

            MaxMemory = maxMemory;
            if (MaxMemory != null)
            {
                cond.Add("MaximumMemory=@maxMemory");
                Parameters.Add(new SqlParameter("@maxMemory", MaxMemory));
            }

            RAMMaxFreq = ramMaxFreq;
            if (RAMMaxFreq != null)
            {
                if (RAMMaxFreq.Contains('<'))
                {
                    cond.Add("MaximumRAMFrequency<@ramMaxFreq");
                    Parameters.Add(new SqlParameter("@ramMaxFreq", RAMMaxFreq.Replace("<", "")));
                }
                else if (RAMMaxFreq.Contains('-'))
                {
                    cond.Add("MaximumRAMFrequency>=@ramMaxFreq1 AND MaximumRAMFrequency<=@ramMaxFreq1");
                    Parameters.Add(new SqlParameter("@ramMaxFreq1", RAMMaxFreq.Split('-')[0]));
                    Parameters.Add(new SqlParameter("@ramMaxFreq2", RAMMaxFreq.Split('-')[1]));
                }
                else if (RAMMaxFreq.Contains('>'))
                {
                    cond.Add("MaximumRAMFrequency>@ramMaxFreq");
                    Parameters.Add(new SqlParameter("@ramMaxFreq", RAMMaxFreq.Replace(">","")));
                }                
            }

            Slots = slots;
            if (Slots != null)
            {
                cond.Add("ID IN (SELECT DISTINCT MotherboardID FROM MOTHERBOARD_SLOTS WHERE Slot = @slots)");
                Parameters.Add(new SqlParameter("@slots", Slots));
            }

            SelectedCharger = selectedCharger;
            if (SelectedCharger != null)
            {
                cond.Add("Pin = (SELECT TOP 1 MotherboardConnector FROM CHARGER WHERE ID = @cID)");
                cond.Add("CPUPin = (SELECT TOP 1 ConnectorType FROM CHARGER WHERE ID = @cID)");
                Parameters.Add(new SqlParameter("@cID", SelectedCharger));
            }

            SelectedCpu = selectedCpu;
            if (SelectedCpu != null)
            {
                cond.Add("Socket = (SELECT TOP 1 Socket FROM CPU WHERE ID = @cpuID)");
                Parameters.Add(new SqlParameter("@cpuID", SelectedCpu));
            }

            SelectedCooler = selectedCooler;
            if (SelectedCooler != null)
            {
                cond.Add("Socket IN (SELECT Socket FROM COOLER_SOCKET WHERE CoolerID = @coolerID)");
                Parameters.Add(new SqlParameter("@coolerID", SelectedCooler));
            }

            SelectedBody = selectedBody;
            if (SelectedCooler != null)
            {
                cond.Add("Formfactor = (SELECT Formfactor FROM BODY WHERE ID = @bodyID)");
                Parameters.Add(new SqlParameter("@bodyID", SelectedBody));
            }

            SelectedRam = selectedRam;
            if (SelectedRam != null)
            {
                cond.Add("MemoryType = (SELECT TOP 1 MemoryType FROM RAM WHERE ID = @ramID)");
                Parameters.Add(new SqlParameter("@ramID", SelectedRam));
            }

            SelectedSsd = selectedSsd;
            if (SelectedSsd != null)
            {
                cond.Add("NOT (SELECT Count(SLOT) FROM MOTHERBOARD_SLOTS WHERE MotherboardID = ID IN (SELECT Interface FROM SSD_INTERFACE WHERE SSDID = @ssdID)) = 0");
                Parameters.Add(new SqlParameter("@ssdID", SelectedSsd));
            }

            SelectedHdd = selectedHdd;
            if (SelectedHdd != null)
            {
                cond.Add("NOT (SELECT Count(SLOT) FROM MOTHERBOARD_SLOTS WHERE MotherboardID = ID IN (SELECT Interface FROM HDD_INTERFACE WHERE HDDID = @hddID)) = 0");
                Parameters.Add(new SqlParameter("@hddID", SelectedHdd));
            }

            if (cond.Count > 0)
            {
                Expression = string.Join(" AND ", cond);
            }
        }
    }
}
