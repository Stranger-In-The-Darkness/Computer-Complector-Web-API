using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Requests.Get
{
    public class GetMotherboardsRequest
    {
        public string[] Company { get; set; }
        public string[] Series { get; set; }
        public string[] Socket { get; set; }
        public string[] Chipset { get; set; }
        public string[] Formfactor { get; set; }
        public string[] Memory { get; set; }
        public int[] MemorySlotsAmount { get; set; }
        public int[] MemoryChanelsAmount { get; set; }
        public int[] MaxMemory { get; set; }
        public string[] RAMMaxFreq { get; set; }
        public string[] Slots { get; set; }

        public int? SelectedBody { get; private set; }
        public int? SelectedCharger { get; private set; }
        public int? SelectedCooler { get; private set; }
        public int? SelectedRam { get; private set; }
        public int? SelectedSsd { get; private set; }
        public int? SelectedHdd { get; private set; }
        public int? SelectedCpu { get; private set; }

        public string Expression { get; } = "SELECT * FROM MOTHERBOARD m JOIN MOTHERBOARD_SLOTS ms on m.ID = ms.MotherboardID";
        public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();

        public GetMotherboardsRequest(string[] company, string[] series, string[] socket, string[] chipset, string[] formfactor,
            string[] memory, int[] memorySlotsAmount, int[] memoryChanelsAmount, int[] maxMemory, string[] ramMaxFreq, string[] slots,
            int? selectedCharger, int? selectedCooler, int? selectedRam, int? selectedSsd, int? selectedBody, int? selectedHdd,
            int? selectedCpu)
        {
            List<string> cond = new List<string>();

            Company = company;
            if (Company != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Company.Length; i++)
                {
                    con.Add($"Company=@company{i}");
                    Parameters.Add(new SqlParameter($"@company{i}", Company[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Series = series;
            if (Series != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Series.Length; i++)
                {
                    con.Add($"Series=@series{i}");
                    Parameters.Add(new SqlParameter($"@series{i}", Series[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Socket = socket;
            if (Socket != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Socket.Length; i++)
                {
                    con.Add($"Socket=@socket{i}");
                    Parameters.Add(new SqlParameter($"@socket{i}", Socket[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Chipset = chipset;
            if (Chipset != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Chipset.Length; i++)
                {
                    con.Add($"Chipset=@chipset{i}");
                    Parameters.Add(new SqlParameter($"@chipset{i}", Chipset[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Formfactor = formfactor;
            if (Formfactor != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Formfactor.Length; i++)
                {
                    con.Add($"Formfactor=@formfactor{i}");
                    Parameters.Add(new SqlParameter($"@formfactor{i}", Formfactor[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            Memory = memory;
            if (Memory != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Memory.Length; i++)
                {
                    con.Add($"MemoryType=@memory{i}");
                    Parameters.Add(new SqlParameter($"@memory{i}", Memory[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            MemorySlotsAmount = memorySlotsAmount;
            if (MemorySlotsAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < MemorySlotsAmount.Length; i++)
                {
                    con.Add($"AmountOfMemorySlots=@memorySlotsAmount{i}");
                    Parameters.Add(new SqlParameter($"@memorySlotsAmount{i}", MemorySlotsAmount[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            MemoryChanelsAmount = memoryChanelsAmount;
            if (MemoryChanelsAmount != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < MemoryChanelsAmount.Length; i++)
                {
                    con.Add($"AmountOfMemoryChanels=@memoryChanelsAmount{i}");
                    Parameters.Add(new SqlParameter($"@memoryChanelsAmount{i}", MemoryChanelsAmount[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            MaxMemory = maxMemory;
            if (MaxMemory != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < MaxMemory.Length; i++)
                {
                    con.Add($"MaximumMemory=@maxMemory{i}");
                    Parameters.Add(new SqlParameter($"@maxMemory{i}", MaxMemory[i]));
                }
                cond.Add(string.Join(" OR ", con));
            }

            RAMMaxFreq = ramMaxFreq;
            if (RAMMaxFreq != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < RAMMaxFreq.Length; i++)
                {
                    if (RAMMaxFreq[i].Contains('<'))
                    {
                        con.Add($"MaximumRAMFrequency<@ramMaxFreq{i}");
                        Parameters.Add(new SqlParameter($"@ramMaxFreq{i}", RAMMaxFreq[i].Replace("<", "")));
                    }
                    else if (RAMMaxFreq[i].Contains('-'))
                    {
                        con.Add($"MaximumRAMFrequency>=@ramMaxFreq1{i} AND MaximumRAMFrequency<=@ramMaxFreq2{i}");
                        Parameters.Add(new SqlParameter($"@ramMaxFreq1{i}", RAMMaxFreq[i].Split('-')[0]));
                        Parameters.Add(new SqlParameter($"@ramMaxFreq2{i}", RAMMaxFreq[i].Split('-')[1]));
                    }
                    else if (RAMMaxFreq[i].Contains('>'))
                    {
                        con.Add($"MaximumRAMFrequency>@ramMaxFreq{i}");
                        Parameters.Add(new SqlParameter($"@ramMaxFreq{i}", RAMMaxFreq[i].Replace(">", "")));
                    }
                }
                cond.Add(string.Join(" OR ", con));
            }

            Slots = slots;
            if (Slots != null)
            {
                List<string> con = new List<string>();
                for (int i = 0; i < Slots.Length; i++)
                {
                    con.Add($"ID IN (SELECT DISTINCT MotherboardID FROM MOTHERBOARD_SLOTS WHERE Slot = @slots{i})");
                    Parameters.Add(new SqlParameter($"@slots{i}", Slots[i]));
                }
                cond.Add(string.Join(" OR ", con));
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
                Expression += $" WHERE {string.Join(" AND ", cond)}";
            }
        }
    }
}
