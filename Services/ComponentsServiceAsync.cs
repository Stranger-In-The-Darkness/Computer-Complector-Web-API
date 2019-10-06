using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;

using Microsoft.Extensions.Caching.Memory;

using ComputerComplectorWebAPI.Interfaces;
using ComputerComplectorWebAPI.Models.Data;
using ComputerComplectorWebAPI.Models.Requests.Get;
using ComputerComplectorWebAPI.Models.Requests.Add;
using ComputerComplectorWebAPI.Models.Requests.Remove;
using ComputerComplectorWebAPI.Models.Requests.Update;

namespace ComputerComplectorWebAPI.Services
{
    /// <summary>
    /// Asynchronous User component service
    /// </summary>
    public class ComponentsServiceAsync : IComponentsServiceAsync
    {
        private IUtilityAsync _utility;

        private IMemoryCache _cache;

        public ComponentsServiceAsync(IUtilityAsync utility, IMemoryCache cache)
        {
            _utility = utility;

            _cache = cache;
        }

        #region GET BY ID
        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<Body> GetBody(int id = -1)
        {
            if (id <= -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM BODY WHERE ID = @id";

            Body element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new Body()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Formfactor = reader["Formfactor"].ToString().Trim(),
                        Type = reader["Type"].ToString().Trim(),
                        BuildInCharger = (bool)reader["Build-inCharger"],
                        ChargerPower = (int)reader["ChargerPower"],
                        Color = reader["Color"].ToString().Trim(),
                        USB2Amount = (int)reader["USB2.0Amount"],
                        USB3Amount = (int)reader["USB3.0Amount"],
                        Additions = reader["Additions"].ToString().Trim(),
                        VideocardMaxLength = (int)reader["VideocardMaxLength"]
                    };
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }
            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<Charger> GetCharger(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM CHARGER b WHERE b.ID = @id";

            Charger element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new Charger()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Series = reader["Series"].ToString().Trim(),
                        Power = (int)reader["Power"],
                        Sertificate80Plus = reader["Sertificate80Plus"].ToString().Trim(),
                        VideoConnectorsAmount = (int)reader["VideoConnectorsAmount"],
                        ConnectorType = reader["ConnectorType"].ToString().Trim(),
                        SATAAmount = (int)reader["SATAAmount"],
                        IDEAmount = (int)reader["IDEAmount"],
                        MotherboardConnector = reader["MotherboardConnector"].ToString().Trim()
                    };
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<Cooler> GetCooler(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM COOLER b JOIN COOLER_SOCKET cs ON b.ID = cs.CoolerID WHERE b.ID = @id";

            Cooler element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new Cooler()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Purpose = reader["Purpose"].ToString().Trim(),
                        Type = reader["Type"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Material = reader["Material"].ToString().Trim(),
                        VentDiam = reader["Diameter"] is DBNull ? null : (double?)reader["Diameter"],
                        TurnAdj = reader["Adjustement"] is DBNull ? null : (bool?)reader["Adjustement"],
                        Color = reader["Color"].ToString().Trim(),
                        Socket = new List<string>() { reader["Socket"].ToString().Trim() }
                    };
                }
                while (reader.Read())
                {
                    element.Socket.Add(reader["Socket"].ToString().Trim());
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<CPU> GetCPU(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM CPU b WHERE b.ID = @id";

            CPU element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new CPU()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Series = reader["Series"].ToString().Trim(),
                        Socket = reader["Socket"].ToString().Trim(),
                        Frequency = (int)reader["Frequency"],
                        CoresAmount = (int)reader["AmountOfCores"],
                        ThreadsAmount = (int)reader["AmountOfThreads"],
                        IntegratedGraphics = (bool)reader["IntegratedGraphics"],
                        Core = reader["Core"].ToString().Trim(),
                        DeliveryType = reader["DeliveryType"].ToString().Trim(),
                        Overcloacking = (bool)reader["Overclocking"]
                    };
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<HDD> GetHDD(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM HDD b JOIN HDD_INTERFACE hd on b.ID = hd.HDDID WHERE b.ID = @id";

            HDD element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new HDD()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Formfactor = reader["Formfactor"].ToString().Trim(),
                        Capacity = (int)reader["Volume"],
                        BufferVolume = (int)reader["BufferVolume"],
                        Speed = (int)reader["Speed"],
                        Interface = new List<string>() { reader["Interface"].ToString().Trim() }
                    };

                    while (reader.Read())
                    {
                        element.Interface.Add(reader["Interface"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<Motherboard> GetMotherboard(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM MOTHERBOARD b JOIN MOTHERBOARD_SLOTS ms on b.ID = ms.MotherboardID WHERE b.ID = @id";

            Motherboard element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new Motherboard()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Socket = reader["Socket"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Chipset = reader["Chipset"].ToString().Trim(),
                        CPUCompany = reader["CPUCompany"].ToString().Trim(),
                        Formfactor = reader["Formfactor"].ToString().Trim(),
                        MemoryType = reader["MemoryType"].ToString().Trim(),
                        MemorySlotsAmount = (int)reader["AmountOfMemorySlots"],
                        MemoryChanelsAmount = (int)reader["AmountOfMemoryChanels"],
                        MaxMemory = (int)reader["MaximumMemory"],
                        RAMMaxFreq = (int)reader["MaximumRAMFrequency"],
                        Series = reader["Series"].ToString().Trim(),
                        Slots = new List<string>() { reader["Slot"].ToString().Trim() },
                        Pin = reader["Pin"].ToString().Trim(),
                        CPUPin = reader["CPUPin"].ToString().Trim()
                    };
                }
                while (reader.Read())
                {
                    element.Slots.Add(reader["Slot"].ToString().Trim());
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<RAM> GetRAM(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM RAM b WHERE b.ID = @id";

            RAM element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new RAM()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Series = reader["Series"].ToString().Trim(),
                        MemoryType = reader["MemoryType"].ToString().Trim(),
                        Purpose = reader["Purpose"].ToString().Trim(),
                        Volume = (int)reader["MemoryVolume"],
                        ModuleAmount = (int)reader["ModulesAmount"],
                        Freq = (int)reader["Frequency"],
                        CL = reader["CASLatency"].ToString().Trim()
                    };
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<SSD> GetSSD(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM SSD b JOIN SSD_INTERFACE si on b.ID = si.SSDID WHERE b.ID = @id";

            SSD element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new SSD()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Series = reader["Series"].ToString().Trim(),
                        Capacity = (int)reader["Volume"],
                        Formfactor = reader["Formfactor"].ToString().Trim(),
                        CellType = reader["CellType"].ToString().Trim(),
                        Interface = new List<string>() { reader["Interface"].ToString().Trim() }
                    };
                }
                while (reader.Read())
                {
                    element.Interface.Add(reader["Interface"].ToString().Trim());
                }

                reader.Close();

                _cache.Set((expression, id), element, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }

        /// <summary>
        /// Get record by ID. Null if incorrect ID
        /// </summary>
        /// <param name="id">Record ID</param>
        /// <returns></returns>
        public async Task<Videocard> GetVideocard(int id = -1)
        {
            if (id == -1)
            {
                throw new IndexOutOfRangeException("Incorrect index");
            }
            string expression = "SELECT * FROM VIDEOCARD b JOIN VIDEOCARD_CONNECTOR vc on b.ID = vc.VideocardID WHERE b.ID = @id";

            Videocard element = null;

            SqlParameter param = new SqlParameter("@id", id);

            if (!_cache.TryGetValue((expression, id), out element))
            {
                var reader = await Task.Run(() => _utility.ExecuteReader(expression, param));

                if (reader.Read())
                {
                    element = new Videocard()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString().Trim(),
                        Company = reader["Company"].ToString().Trim(),
                        Series = reader["Series"].ToString().Trim(),
                        Proccessor = reader["GraphicalProccessor"].ToString().Trim(),
                        VRAM = (int)reader["VRAM"],
                        Memory = reader["Memory"].ToString().Trim(),
                        Capacity = (int)reader["Capacity"],
                        Family = reader["Family"].ToString().Trim(),
                        Connectors = new List<string>() { reader["Connector"].ToString().Trim() },
                        Length = reader["Size"].ToString().Trim(),
                        Pin = reader["Pin"].ToString().Trim()
                    };
                }
                while (reader.Read())
                {
                    element.Connectors.Add(reader["Connector"].ToString().Trim());
                }

                reader.Close();

                _cache.Set(
                    (expression, id),
                    element,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return element;
        }
        #endregion

        #region GET ALL
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Body>> GetBodies()
        {
            string expression = "SELECT * FROM BODY";

            List<Body> bodies;

            if (!_cache.TryGetValue(expression, out bodies))
            {
                bodies = new List<Body>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    bodies.Add(
                        new Body()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            Type = reader["Type"].ToString().Trim(),
                            BuildInCharger = (bool)reader["Build-inCharger"],
                            ChargerPower = (int)reader["ChargerPower"],
                            Color = reader["Color"].ToString().Trim(),
                            USB2Amount = (int)reader["USB2.0Amount"],
                            USB3Amount = (int)reader["USB3.0Amount"],
                            Additions = reader["Additions"].ToString().Trim(),
                            VideocardMaxLength = (int)reader["VideocardMaxLength"]
                        }
                    );
                }

                reader.Close();

                _cache.Set(
                    expression,
                    bodies,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return bodies;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Charger>> GetChargers()
        {
            string expression = "SELECT * FROM CHARGER";

            List<Charger> chargers;

            if (!_cache.TryGetValue(expression, out chargers))
            {
                chargers = new List<Charger>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    chargers.Add(
                        new Charger()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Power = (int)reader["Power"],
                            Sertificate80Plus = reader["Sertificate80Plus"].ToString().Trim(),
                            VideoConnectorsAmount = (int)reader["VideoConnectorsAmount"],
                            ConnectorType = reader["ConnectorType"].ToString().Trim(),
                            SATAAmount = (int)reader["SATAAmount"],
                            IDEAmount = (int)reader["IDEAmount"],
                            MotherboardConnector = reader["MotherboardConnector"].ToString().Trim()
                        }
                    );
                }

                reader.Close();

                _cache.Set(expression, chargers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return chargers;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Cooler>> GetCoolers()
        {
            string expression = "SELECT * FROM COOLER c JOIN COOLER_SOCKET cs on c.ID = cs.CoolerID";

            List<Cooler> coolers;

            if (!_cache.TryGetValue(expression, out coolers))
            {
                coolers = new List<Cooler>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    if (coolers.Count == 0 || (coolers.Count > 0 && coolers.Last().ID != (int)reader["ID"]))
                    {
                        var element = new Cooler()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Purpose = reader["Purpose"].ToString().Trim(),
                            Type = reader["Type"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Material = reader["Material"].ToString().Trim(),
                            VentDiam = reader["Diameter"] is DBNull ? null : (double?)reader["Diameter"],
                            TurnAdj = reader["Adjustement"] is DBNull ? null : (bool?)reader["Adjustement"],
                            Color = reader["Color"].ToString().Trim(),
                            Socket = new List<string>() { reader["Socket"].ToString().Trim() }
                        };

                        coolers.Add(element);
                    }
                    else if (coolers.Count > 0)
                    {
                        coolers.Last().Socket.Add(reader["Socket"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    expression,
                    coolers,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return coolers;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CPU>> GetCPUs()
        {
            string expression = "SELECT * FROM CPU";

            List<CPU> cpus;

            if (!_cache.TryGetValue(expression, out cpus))
            {
                cpus = new List<CPU>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    cpus.Add(
                        new CPU()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Socket = reader["Socket"].ToString().Trim(),
                            Frequency = (double)reader["Frequency"],
                            CoresAmount = (int)reader["AmountOfCores"],
                            ThreadsAmount = (int)reader["AmountOfThreads"],
                            IntegratedGraphics = (bool)reader["IntegratedGraphics"],
                            Core = reader["Core"].ToString().Trim(),
                            DeliveryType = reader["DeliveryType"].ToString().Trim(),
                            Overcloacking = (bool)reader["Overclocking"]
                        }
                    );
                }

                reader.Close();

                _cache.Set(
                    expression,
                    cpus,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return cpus;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<HDD>> GetHDDs()
        {
            string expression = "SELECT * FROM HDD h JOIN HDD_INTERFACE hd on h.ID = hd.HDDID";

            List<HDD> hdds;

            if (!_cache.TryGetValue(expression, out hdds))
            {
                hdds = new List<HDD>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    if (hdds.Count == 0 || (hdds.Count > 0 && hdds.Last().ID != (int)reader["ID"]))
                    {
                        var element = new HDD()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            Capacity = (int)reader["Volume"],
                            BufferVolume = (int)reader["BufferVolume"],
                            Speed = (int)reader["Speed"],
                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
                        };
                        hdds.Add(element);
                    }
                    else if (hdds.Count > 0)
                    {
                        hdds.Last().Interface.Add(reader["Interface"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    expression,
                    hdds,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return hdds;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Motherboard>> GetMotherboards()
        {
            string expression = "SELECT * FROM MOTHERBOARD m JOIN MOTHERBOARD_SLOTS ms on m.ID = ms.MotherboardID";

            List<Motherboard> motherboards = new List<Motherboard>();

            if (!_cache.TryGetValue(expression, out motherboards))
            {
                motherboards = new List<Motherboard>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    if (motherboards.Count == 0 || (motherboards.Count > 0 && motherboards.Last().ID != (int)reader["ID"]))
                    {
                        var element = new Motherboard()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Socket = reader["Socket"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Chipset = reader["Chipset"].ToString().Trim(),
                            CPUCompany = reader["CPUCompany"].ToString().Trim(),
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            MemoryType = reader["MemoryType"].ToString().Trim(),
                            MemorySlotsAmount = (int)reader["AmountOfMemorySlots"],
                            MemoryChanelsAmount = (int)reader["AmountOfMemoryChanels"],
                            MaxMemory = (int)reader["MaximumMemory"],
                            RAMMaxFreq = (int)reader["MaximumRAMFrequency"],
                            Series = reader["Series"].ToString().Trim(),
                            Slots = new List<string>() { reader["Slot"].ToString().Trim() },
                            Pin = reader["Pin"].ToString().Trim(),
                            CPUPin = reader["CPUPin"].ToString().Trim()
                        };

                        motherboards.Add(element);
                    }
                    else if (motherboards.Count > 0)
                    {
                        motherboards.Last().Slots.Add(reader["Slot"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    expression,
                    motherboards,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return motherboards;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RAM>> GetRAMs()
        {
            string expression = "SELECT * FROM RAM";

            List<RAM> rams = new List<RAM>();

            if (!_cache.TryGetValue(expression, out rams))
            {
                rams = new List<RAM>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    rams.Add(
                        new RAM()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            MemoryType = reader["MemoryType"].ToString().Trim(),
                            Purpose = reader["Purpose"].ToString().Trim(),
                            Volume = (int)reader["MemoryVolume"],
                            ModuleAmount = (int)reader["ModulesAmount"],
                            Freq = (int)reader["Frequency"],
                            CL = reader["CASLatency"].ToString().Trim()
                        }
                    );
                }

                reader.Close();

                _cache.Set(
                    expression,
                    rams,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return rams;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SSD>> GetSSDs()
        {
            string expression = "SELECT * FROM SSD s JOIN SSD_INTERFACE si ON s.ID = si.SSDID";

            List<SSD> ssds = new List<SSD>();

            if (!_cache.TryGetValue(expression, out ssds))
            {
                ssds = new List<SSD>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    if (ssds.Count == 0 || (ssds.Count > 0 && ssds.Last().ID != (int)reader["ID"]))
                    {
                        var element = new SSD()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Capacity = (int)reader["Volume"],
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            CellType = reader["CellType"].ToString().Trim(),
                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
                        };
                        ssds.Add(element);
                    }
                    else if (ssds.Count > 0)
                    {
                        ssds.Last().Interface.Add(reader["Interface"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    expression,
                    ssds,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return ssds;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Videocard>> GetVideocards()
        {
            string expression = "SELECT * FROM VIDEOCARD v JOIN VIDEOCARD_CONNECTOR vc on v.ID = vc.VideocardID";

            List<Videocard> videocards = new List<Videocard>();

            if (!_cache.TryGetValue(expression, out videocards))
            {
                videocards = new List<Videocard>();

                var reader = await Task.Run(() => _utility.ExecuteReader(expression));

                while (reader.Read())
                {
                    if (videocards.Count == 0 || (videocards.Count > 0 && videocards.Last().ID != (int)reader["ID"]))
                    {
                        var element = new Videocard()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Proccessor = reader["GraphicalProccessor"].ToString().Trim(),
                            VRAM = (int)reader["VRAM"],
                            Memory = reader["Memory"].ToString().Trim(),
                            Capacity = (int)reader["Capacity"],
                            Family = reader["Family"].ToString().Trim(),
                            Connectors = new List<string>() { reader["Connector"].ToString().Trim() },
                            Length = reader["Length"].ToString().Trim(),
                            Pin = reader["Pin"].ToString().Trim()
                        };

                        videocards.Add(element);
                    }
                    else if (videocards.Count > 0)
                    {
                        videocards.Last().Connectors.Add(reader["Connector"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    expression,
                    videocards,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return videocards;
        }
        #endregion

        #region GET BY REQUEST
        public async Task<IEnumerable<Body>> GetBodies(GetBodiesRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetBodies();
            }

            List<Body> bodies;

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out bodies))
            {
                bodies = new List<Body>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    bodies.Add(
                        new Body()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            Type = reader["Type"].ToString().Trim(),
                            BuildInCharger = (bool)reader["Build-inCharger"],
                            ChargerPower = (int)reader["ChargerPower"],
                            Color = reader["Color"].ToString().Trim(),
                            USB2Amount = (int)reader["USB2.0Amount"],
                            USB3Amount = (int)reader["USB3.0Amount"],
                            Additions = reader["Additions"].ToString().Trim(),
                            VideocardMaxLength = (int)reader["VideocardMaxLength"]
                        }
                    );
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    bodies,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return bodies;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<Charger>> GetChargers(GetChargersRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetChargers();
            }

            List<Charger> chargers;

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out chargers))
            {
                chargers = new List<Charger>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    chargers.Add(
                        new Charger()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Power = (int)reader["Power"],
                            Sertificate80Plus = reader["Sertificate80Plus"].ToString().Trim(),
                            VideoConnectorsAmount = (int)reader["VideoConnectorsAmount"],
                            ConnectorType = reader["ConnectorType"].ToString().Trim(),
                            SATAAmount = (int)reader["SATAAmount"],
                            IDEAmount = (int)reader["IDEAmount"],
                            MotherboardConnector = reader["MotherboardConnector"].ToString().Trim()
                        }
                    );
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    chargers,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return chargers;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<Cooler>> GetCoolers(GetCoolersRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetCoolers();
            }

            List<Cooler> coolers;

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out coolers))
            {
                coolers = new List<Cooler>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    if (coolers.Count == 0 || (coolers.Count > 0 && coolers.Last().ID != (int)reader["ID"]))
                    {
                        var element = new Cooler()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Purpose = reader["Purpose"].ToString().Trim(),
                            Type = reader["Type"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Material = reader["Material"].ToString().Trim(),
                            VentDiam = reader["Diameter"] is DBNull ? null : (double?)reader["Diameter"],
                            TurnAdj = reader["Adjustement"] is DBNull ? null : (bool?)reader["Adjustement"],
                            Color = reader["Color"].ToString().Trim(),
                            Socket = new List<string>() { reader["Socket"].ToString().Trim() }
                        };

                        coolers.Add(element);
                    }
                    else if (coolers.Count > 0)
                    {
                        coolers.Last().Socket.Add(reader["Socket"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    coolers,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return coolers;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<CPU>> GetCPUs(GetCPUsRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetCPUs();
            }

            List<CPU> cpus;

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out cpus))
            {
                cpus = new List<CPU>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    cpus.Add(
                        new CPU()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Socket = reader["Socket"].ToString().Trim(),
                            Frequency = (int)reader["Frequency"],
                            CoresAmount = (int)reader["AmountOfCores"],
                            ThreadsAmount = (int)reader["AmountOfThreads"],
                            IntegratedGraphics = (bool)reader["IntegratedGraphics"],
                            Core = reader["Core"].ToString().Trim(),
                            DeliveryType = reader["DeliveryType"].ToString().Trim(),
                            Overcloacking = (bool)reader["Overclocking"]
                        }
                    );
                }

                reader.Close();

                if (cpus != null)
                {
                    _cache.Set(
                        (request.Expression, request.Parameters),
                        cpus,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
                }
            }

            return cpus;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<HDD>> GetHDDs(GetHDDsRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetHDDs();
            }

            List<HDD> hdds = new List<HDD>();

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out hdds))
            {
                hdds = new List<HDD>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    if (hdds.Count == 0 || (hdds.Count > 0 && hdds.Last().ID != (int)reader["ID"]))
                    {
                        var element = new HDD()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            Capacity = (int)reader["Volume"],
                            BufferVolume = (int)reader["BufferVolume"],
                            Speed = (int)reader["Speed"],
                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
                        };

                        hdds.Add(element);
                    }
                    else if (hdds.Count > 0)
                    {
                        hdds.Last().Interface.Add(reader["Interface"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    hdds,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return hdds;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<Motherboard>> GetMotherboards(GetMotherboardsRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetMotherboards();
            }

            List<Motherboard> motherboards = new List<Motherboard>();

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out motherboards))
            {
                motherboards = new List<Motherboard>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    if (motherboards.Count == 0 || (motherboards.Count > 0 && motherboards.Last().ID != (int)reader["ID"]))
                    {
                        var element = new Motherboard()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Socket = reader["Socket"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Chipset = reader["Chipset"].ToString().Trim(),
                            CPUCompany = reader["CPUCompany"].ToString().Trim(),
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            MemoryType = reader["MemoryType"].ToString().Trim(),
                            MemorySlotsAmount = (int)reader["AmountOfMemorySlots"],
                            MemoryChanelsAmount = (int)reader["AmountOfMemoryChanels"],
                            MaxMemory = (int)reader["MaximumMemory"],
                            RAMMaxFreq = (int)reader["MaximumRAMFrequency"],
                            Series = reader["Series"].ToString().Trim(),
                            Slots = new List<string>() { reader["Slot"].ToString().Trim() },
                            Pin = reader["Pin"].ToString().Trim(),
                            CPUPin = reader["CPUPin"].ToString().Trim()
                        };

                        motherboards.Add(element);
                    }
                    else if (motherboards.Count > 0)
                    {
                        motherboards.Last().Slots.Add(reader["Slot"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    motherboards,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return motherboards;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<RAM>> GetRAMs(GetRAMsRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetRAMs();
            }

            List<RAM> rams = new List<RAM>();

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out rams))
            {
                rams = new List<RAM>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    rams.Add(
                        new RAM()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            MemoryType = reader["MemoryType"].ToString().Trim(),
                            Purpose = reader["Purpose"].ToString().Trim(),
                            Volume = (int)reader["MemoryVolume"],
                            ModuleAmount = (int)reader["ModulesAmount"],
                            Freq = (int)reader["Frequency"],
                            CL = reader["CASLatency"].ToString().Trim()
                        }
                    );
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    rams,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return rams;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<SSD>> GetSSDs(GetSSDsRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetSSDs();
            }

            List<SSD> ssds = new List<SSD>();

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out ssds))
            {
                ssds = new List<SSD>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    if (ssds.Count == 0 || (ssds.Count > 0 && ssds.Last().ID != (int)reader["ID"]))
                    {
                        var element = new SSD()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Capacity = (int)reader["Volume"],
                            Formfactor = reader["Formfactor"].ToString().Trim(),
                            CellType = reader["CellType"].ToString().Trim(),
                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
                        };
                        ssds.Add(element);
                    }
                    else if (ssds.Count > 0)
                    {
                        ssds.Last().Interface.Add(reader["Interface"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    ssds,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return ssds;
        }

        /// <summary>
        /// Get all records that apply
        /// </summary>
        /// <param name="request">Selection filters</param>
        /// <returns></returns>
        public async Task<IEnumerable<Videocard>> GetVideocards(GetVideocardsRequest request)
        {
            if (!request.Parameters.Any())
            {
                return await GetVideocards();
            }

            List<Videocard> videocards = new List<Videocard>();

            if (!_cache.TryGetValue((request.Expression, request.Parameters), out videocards))
            {
                videocards = new List<Videocard>();

                var reader = await Task.Run(() => _utility.ExecuteReader(request.Expression, request.Parameters));

                while (reader.Read())
                {
                    if (videocards.Count == 0 || (videocards.Count > 0 && videocards.Last().ID != (int)reader["ID"]))
                    {
                        var element = new Videocard()
                        {
                            ID = (int)reader["ID"],
                            Title = reader["Title"].ToString().Trim(),
                            Company = reader["Company"].ToString().Trim(),
                            Series = reader["Series"].ToString().Trim(),
                            Proccessor = reader["GraphicalProccessor"].ToString().Trim(),
                            VRAM = (int)reader["VRAM"],
                            Memory = reader["Memory"].ToString().Trim(),
                            Capacity = (int)reader["Capacity"],
                            Family = reader["Family"].ToString().Trim(),
                            Connectors = new List<string>() { reader["Connector"].ToString().Trim() },
                            Length = reader["Length"].ToString().Trim(),
                            Pin = reader["Pin"].ToString().Trim()
                        };

                        videocards.Add(element);
                    }
                    else if (videocards.Count > 0)
                    {
                        videocards.Last().Connectors.Add(reader["Connector"].ToString().Trim());
                    }
                }

                reader.Close();

                _cache.Set(
                    (request.Expression, request.Parameters),
                    videocards,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            return videocards;
        }
        #endregion

        #region ADD
        public async Task<Body> AddBody(AddBodyRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetBody((int)res["ID"]);
        }

        public async Task<Charger> AddCharger(AddChargerRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetCharger((int)res["ID"]);
        }

        public async Task<Cooler> AddCooler(AddCoolerRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetCooler((int)res["ID"]);
        }

        public async Task<CPU> AddCPU(AddCpuRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetCPU((int)res["ID"]);
        }

        public async Task<HDD> AddHDD(AddHddRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetHDD((int)res["ID"]);
        }

        public async Task<Motherboard> AddMotherboard(AddMotherboardRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetMotherboard((int)res["ID"]);
        }

        public async Task<RAM> AddRAM(AddRamRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetRAM((int)res["ID"]);
        }

        public async Task<SSD> AddSSD(AddSsdRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetSSD((int)res["ID"]);
        }

        public async Task<Videocard> AddVideocard(AddVideocardRequest request)
        {
            if (!request.Parameters.Any())
            {
                throw new ArgumentException("Invalid model to add. Empty parameters", "request");
            }

            var res = await _utility.ExecuteReader(request.Expression, request.Parameters.ToList());
            await res.ReadAsync();
            return await GetVideocard((int)res["ID"]);
        }

		Task<IEnumerable<Body>> IComponentsServiceAsync.AddBody(AddBodyRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Body>> RemoveBody(RemoveBodyRequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Charger>> IComponentsServiceAsync.AddCharger(AddChargerRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Charger>> RemoveCharger(RemoveChargerRequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Cooler>> IComponentsServiceAsync.AddCooler(AddCoolerRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Cooler>> RemoveCooler(RemoveCoolerRequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<CPU>> IComponentsServiceAsync.AddCPU(AddCpuRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CPU>> RemoveCPU(RemoveCPURequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<HDD>> IComponentsServiceAsync.AddHDD(AddHddRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<HDD>> RemoveHDD(RemoveHDDRequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Motherboard>> IComponentsServiceAsync.AddMotherboard(AddMotherboardRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Motherboard>> RemoveMotherboard(RemoveMotherboardRequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<RAM>> IComponentsServiceAsync.AddRAM(AddRamRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<RAM>> RemoveRAM(RemoveRAMRequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<SSD>> IComponentsServiceAsync.AddSSD(AddSsdRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SSD>> RemoveSSD(RemoveSSDRequest request)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Videocard>> IComponentsServiceAsync.AddVideocard(AddVideocardRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Videocard>> RemoveVideocard(RemoveVideocardRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Body>> ReplaceBody(UpdateBodyRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Charger>> ReplaceCharger(UpdateChargerRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Cooler>> ReplaceCooler(UpdateCoolerRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<CPU>> ReplaceCPU(UpdateCPURequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<HDD>> ReplaceHDD(UpdateHDDRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Motherboard>> ReplaceMotherboard(UpdateMotherboardRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<RAM>> ReplaceRAM(UpdateRAMRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<SSD>> ReplaceSSD(UpdateSSDRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Videocard>> ReplaceVideocard(UpdateVideocardRequest request)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}