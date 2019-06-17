//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using ComputerComplectorWebAPI.Interfaces;
//using ComputerComplectorWebAPI.Models;

//namespace ComputerComplectorWebAPI.Services
//{
//    public class ComponentServiceMockAsync : IComponentsServiceAsync
//    {
//        private Dictionary<string, (bool, string, List<string>)> _bodyProp = new Dictionary<string, (bool, string, List<string>)>()
//        {
//            //{ "company", (true, " ", new List<string>(){ "company1", "company2", "company3" }) },
//            //{ "formfactor", (true, "Formfactor of body", new List<string>()) },
//            //{ "type", () }
//            //{ "build-in-charger", () }
//            //{ "charger-power",() }
//            //{ "color", () }
//            //{ "usb-2-ports", ()}
//            //{ "usb-3-ports", ()}
//            //{ "backlight-color", ()}
//            //{ "videoacrd-max-length", ()}
//};
//        private Dictionary<string, (bool, string, List<string>)> _chargerProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };
//        private Dictionary<string, (bool, string, List<string>)> _coolerProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };
//        private Dictionary<string, (bool, string, List<string>)> _cpuProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };
//        private Dictionary<string, (bool, string, List<string>)> _hddProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };
//        private Dictionary<string, (bool, string, List<string>)> _motherboardProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };
//        private Dictionary<string, (bool, string, List<string>)> _ramProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };
//        private Dictionary<string, (bool, string, List<string>)> _ssdProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };
//        private Dictionary<string, (bool, string, List<string>)> _videocardProp = new Dictionary<string, (bool, string, List<string>)>()
//        {

//        };

//        public async Task<Body> GetBody(int id = -1)
//        {
//            if (id <= -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM BODY WHERE ID = @id";
//            var connection = Utility.Connection;

//            Body element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new Body()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Formfactor = reader["Formfactor"].ToString().Trim(),
//                        Type = reader["Type"].ToString().Trim(),
//                        BuildInCharger = (bool)reader["Build-inCharger"],
//                        ChargerPower = (int)reader["ChargerPower"],
//                        Color = reader["Color"].ToString().Trim(),
//                        USB2Ports = (int)reader["USB2.0Amount"],
//                        USB3Ports = (int)reader["USB3.0Amount"],
//                        Additions = reader["Additions"].ToString().Trim(),
//                        VideoacrdMaxLength = (int)reader["VideocardMaxLength"]
//                    };
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<Charger> GetCharger(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM CHARGER b WHERE b.ID = @id";

//            var connection = Utility.Connection;

//            Charger element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new Charger()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Series = reader["Series"].ToString().Trim(),
//                        Power = (int)reader["Power"],
//                        Sertificate = reader["Sertificate80Plus"].ToString().Trim(),
//                        VideoConnectorsAmount = (int)reader["VideoConnectorsAmount"],
//                        ConnectorType = reader["ConnectorType"].ToString().Trim(),
//                        SATAAmount = (int)reader["SATAAmount"],
//                        IDEAmount = (int)reader["IDEAmount"],
//                        MotherboardConnector = reader["MotherboardConnector"].ToString().Trim()
//                    };
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<Cooler> GetCooler(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM COOLER b JOIN COOLER_SOCKET cs ON b.ID = cs.CoolerID WHERE b.ID = @id";
//            var connection = Utility.Connection;

//            Cooler element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new Cooler()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Purpose = reader["Purpose"].ToString().Trim(),
//                        Type = reader["Type"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Material = reader["Material"].ToString().Trim(),
//                        VentDiam = reader["Diameter"] is DBNull ? null : (double?)reader["Diameter"],
//                        TurnAdj = reader["Adjustement"] is DBNull ? null : (bool?)reader["Adjustement"],
//                        Color = reader["Color"].ToString().Trim(),
//                        Socket = new List<string>() { reader["Socket"].ToString().Trim() }
//                    };
//                }
//                while (reader.Read())
//                {
//                    element.Socket.Add(reader["Socket"].ToString().Trim());
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<CPU> GetCPU(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM CPU b WHERE b.ID = @id";
//            var connection = Utility.Connection;

//            CPU element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new CPU()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Series = reader["Series"].ToString().Trim(),
//                        Socket = reader["Socket"].ToString().Trim(),
//                        Frequency = (int)reader["Frequency"],
//                        CoresAmount = (int)reader["AmountOfCores"],
//                        ThreadsAmount = (int)reader["AmountOfThreads"],
//                        IntegratedGraphics = (bool)reader["IntegratedGraphics"],
//                        Core = reader["Core"].ToString().Trim(),
//                        DeliveryType = reader["DeliveryType"].ToString().Trim(),
//                        Overcloacking = (bool)reader["Overclocking"]
//                    };
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<HDD> GetHDD(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM HDD b JOIN HDD_INTERFACE hd on b.ID = hd.HDDID WHERE b.ID = @id";
//            var connection = Utility.Connection;

//            HDD element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new HDD()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Formfactor = reader["Formfactor"].ToString().Trim(),
//                        Capacity = (int)reader["Volume"],
//                        BufferVolume = (int)reader["BufferVolume"],
//                        Speed = (int)reader["Speed"],
//                        Interface = new List<string>() { reader["Interface"].ToString().Trim() }
//                    };

//                    while (reader.Read())
//                    {
//                        element.Interface.Add(reader["Interface"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<Motherboard> GetMotherboard(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM MOTHERBOARD b JOIN MOTHERBOARD_SLOTS ms on b.ID = ms.MotherboardID WHERE b.ID = @id";
//            var connection = Utility.Connection;

//            Motherboard element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new Motherboard()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Socket = reader["Socket"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Chipset = reader["Chipset"].ToString().Trim(),
//                        CPUCompany = reader["CPUCompany"].ToString().Trim(),
//                        Formfactor = reader["Formfactor"].ToString().Trim(),
//                        MemoryType = reader["MemoryType"].ToString().Trim(),
//                        MemorySlotsAmount = (int)reader["AmountOfMemorySlots"],
//                        MemoryChanelsAmount = (int)reader["AmountOfMemoryChanels"],
//                        MaxMemory = (int)reader["MaximumMemory"],
//                        RAMMaxFreq = (int)reader["MaximumRAMFrequency"],
//                        Series = reader["Series"].ToString().Trim(),
//                        Slots = new List<string>() { reader["Slot"].ToString().Trim() },
//                        Pin = reader["Pin"].ToString().Trim(),
//                        CPUPin = reader["CPUPin"].ToString().Trim()
//                    };
//                }
//                while (reader.Read())
//                {
//                    element.Slots.Add(reader["Slot"].ToString().Trim());
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<RAM> GetRAM(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM RAM b WHERE b.ID = @id";
//            var connection = Utility.Connection;

//            RAM element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new RAM()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Series = reader["Series"].ToString().Trim(),
//                        MemoryType = reader["MemoryType"].ToString().Trim(),
//                        Purpose = reader["Purpose"].ToString().Trim(),
//                        Volume = (int)reader["MemoryVolume"],
//                        ModuleAmount = (int)reader["ModulesAmount"],
//                        Freq = (int)reader["Frequency"],
//                        CL = reader["CASLatency"].ToString().Trim()
//                    };
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<SSD> GetSSD(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM SSD b JOIN SSD_INTERFACE si on b.ID = si.SSDID WHERE b.ID = @id";
//            var connection = Utility.Connection;

//            SSD element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new SSD()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Series = reader["Series"].ToString().Trim(),
//                        Capacity = (int)reader["Volume"],
//                        Formfactor = reader["Formfactor"].ToString().Trim(),
//                        CellType = reader["CellType"].ToString().Trim(),
//                        Interface = new List<string>() { reader["Interface"].ToString().Trim() }
//                    };
//                }
//                while (reader.Read())
//                {
//                    element.Interface.Add(reader["Interface"].ToString().Trim());
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<Videocard> GetVideocard(int id = -1)
//        {
//            if (id == -1)
//            {
//                throw new IndexOutOfRangeException("Incorrect index");
//            }
//            string expression = "SELECT * FROM VIDEOCARD b JOIN VIDEOCARD_CONNECTOR vc on b.ID = vc.VideocardID WHERE b.ID = @id";
//            var connection = Utility.Connection;

//            Videocard element = null;

//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();
//                SqlCommand command = new SqlCommand(expression, connection);

//                SqlParameter param = new SqlParameter("@id", id);
//                command.Parameters.Add(param);

//                var reader = await command.ExecuteReaderAsync();

//                if (reader.Read())
//                {
//                    element = new Videocard()
//                    {
//                        ID = (int)reader["ID"],
//                        Title = reader["Title"].ToString().Trim(),
//                        Company = reader["Company"].ToString().Trim(),
//                        Series = reader["Series"].ToString().Trim(),
//                        Proccessor = reader["GraphicalProccessor"].ToString().Trim(),
//                        VRAM = (int)reader["VRAM"],
//                        Memory = reader["Memory"].ToString().Trim(),
//                        Capacity = (int)reader["Capacity"],
//                        Family = reader["Family"].ToString().Trim(),
//                        Connectors = new List<string>() { reader["Connector"].ToString().Trim() },
//                        Length = reader["Size"].ToString().Trim(),
//                        Pin = reader["Pin"].ToString().Trim()
//                    };
//                }
//                while (reader.Read())
//                {
//                    element.Connectors.Add(reader["Connector"].ToString().Trim());
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return element;
//        }

//        public async Task<IEnumerable<Body>> GetBodies()
//        {
//            string expression = "SELECT * FROM BODY";

//            List<Body> bodies = new List<Body>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    bodies.Add(
//                        new Body()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            Type = reader["Type"].ToString().Trim(),
//                            BuildInCharger = (bool)reader["Build-inCharger"],
//                            ChargerPower = (int)reader["ChargerPower"],
//                            Color = reader["Color"].ToString().Trim(),
//                            USB2Ports = (int)reader["USB2.0Amount"],
//                            USB3Ports = (int)reader["USB3.0Amount"],
//                            Additions = reader["Additions"].ToString().Trim(),
//                            VideoacrdMaxLength = (int)reader["VideocardMaxLength"]
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return bodies;
//        }

//        public async Task<IEnumerable<Body>> GetBodies(GetBodiesRequest request)
//        {
//            string expression = "SELECT * FROM BODY";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<Body> bodies = new List<Body>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    bodies.Add(
//                        new Body()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            Type = reader["Type"].ToString().Trim(),
//                            BuildInCharger = (bool)reader["Build-inCharger"],
//                            ChargerPower = (int)reader["ChargerPower"],
//                            Color = reader["Color"].ToString().Trim(),
//                            USB2Ports = (int)reader["USB2.0Amount"],
//                            USB3Ports = (int)reader["USB3.0Amount"],
//                            Additions = reader["Additions"].ToString().Trim(),
//                            VideoacrdMaxLength = (int)reader["VideocardMaxLength"]
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return bodies;
//        }

//        public async Task<IEnumerable<Charger>> GetChargers()
//        {
//            string expression = "SELECT * FROM CHARGER";

//            List<Charger> chargers = new List<Charger>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    chargers.Add(
//                        new Charger()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Power = (int)reader["Power"],
//                            Sertificate = reader["Sertificate80Plus"].ToString().Trim(),
//                            VideoConnectorsAmount = (int)reader["VideoConnectorsAmount"],
//                            ConnectorType = reader["ConnectorType"].ToString().Trim(),
//                            SATAAmount = (int)reader["SATAAmount"],
//                            IDEAmount = (int)reader["IDEAmount"],
//                            MotherboardConnector = reader["MotherboardConnector"].ToString().Trim()
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return chargers;
//        }

//        public async Task<IEnumerable<Charger>> GetChargers(GetChargersRequest request)
//        {
//            string expression = "SELECT * FROM CHARGER";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<Charger> chargers = new List<Charger>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    chargers.Add(
//                        new Charger()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Power = (int)reader["Power"],
//                            Sertificate = reader["Sertificate80Plus"].ToString().Trim(),
//                            VideoConnectorsAmount = (int)reader["VideoConnectorsAmount"],
//                            ConnectorType = reader["ConnectorType"].ToString().Trim(),
//                            SATAAmount = (int)reader["SATAAmount"],
//                            IDEAmount = (int)reader["IDEAmount"],
//                            MotherboardConnector = reader["MotherboardConnector"].ToString().Trim()
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return chargers;
//        }

//        public async Task<IEnumerable<Cooler>> GetCoolers()
//        {
//            string expression = "SELECT * FROM COOLER c JOIN COOLER_SOCKET cs on c.ID = cs.CoolerID";

//            List<Cooler> coolers = new List<Cooler>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (coolers.Count == 0 || (coolers.Count > 0 && coolers.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new Cooler()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Purpose = reader["Purpose"].ToString().Trim(),
//                            Type = reader["Type"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Material = reader["Material"].ToString().Trim(),
//                            VentDiam = reader["Diameter"] is DBNull ? null : (double?)reader["Diameter"],
//                            TurnAdj = reader["Adjustement"] is DBNull ? null : (bool?)reader["Adjustement"],
//                            Color = reader["Color"].ToString().Trim(),
//                            Socket = new List<string>() { reader["Socket"].ToString().Trim() }
//                        };

//                        coolers.Add(element);
//                    }
//                    else if (coolers.Count > 0)
//                    {
//                        coolers.Last().Socket.Add(reader["Socket"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return coolers;
//        }

//        public async Task<IEnumerable<Cooler>> GetCoolers(GetCoolersRequest request)
//        {
//            string expression = "SELECT * FROM COOLER c JOIN COOLER_SOCKET cs on c.ID = cs.CoolerID";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<Cooler> coolers = new List<Cooler>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (coolers.Count == 0 || (coolers.Count > 0 && coolers.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new Cooler()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Purpose = reader["Purpose"].ToString().Trim(),
//                            Type = reader["Type"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Material = reader["Material"].ToString().Trim(),
//                            VentDiam = reader["Diameter"] is DBNull ? null : (double?)reader["Diameter"],
//                            TurnAdj = reader["Adjustement"] is DBNull ? null : (bool?)reader["Adjustement"],
//                            Color = reader["Color"].ToString().Trim(),
//                            Socket = new List<string>() { reader["Socket"].ToString().Trim() }
//                        };

//                        coolers.Add(element);
//                    }
//                    else if (coolers.Count > 0)
//                    {
//                        coolers.Last().Socket.Add(reader["Socket"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return coolers;
//        }

//        public async Task<IEnumerable<CPU>> GetCPUs()
//        {
//            string expression = "SELECT * FROM CPU";

//            List<CPU> cpus = new List<CPU>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    cpus.Add(
//                        new CPU()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Socket = reader["Socket"].ToString().Trim(),
//                            Frequency = (int)reader["Frequency"],
//                            CoresAmount = (int)reader["AmountOfCores"],
//                            ThreadsAmount = (int)reader["AmountOfThreads"],
//                            IntegratedGraphics = (bool)reader["IntegratedGraphics"],
//                            Core = reader["Core"].ToString().Trim(),
//                            DeliveryType = reader["DeliveryType"].ToString().Trim(),
//                            Overcloacking = (bool)reader["Overclocking"]
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return cpus;
//        }

//        public async Task<IEnumerable<CPU>> GetCPUs(GetCPUsRequest request)
//        {
//            string expression = "SELECT * FROM CPU";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<CPU> cpus = new List<CPU>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    cpus.Add(
//                        new CPU()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Socket = reader["Socket"].ToString().Trim(),
//                            Frequency = (int)reader["Frequency"],
//                            CoresAmount = (int)reader["AmountOfCores"],
//                            ThreadsAmount = (int)reader["AmountOfThreads"],
//                            IntegratedGraphics = (bool)reader["IntegratedGraphics"],
//                            Core = reader["Core"].ToString().Trim(),
//                            DeliveryType = reader["DeliveryType"].ToString().Trim(),
//                            Overcloacking = (bool)reader["Overclocking"]
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return cpus;
//        }

//        public async Task<IEnumerable<HDD>> GetHDDs()
//        {
//            string expression = "SELECT * FROM HDD h JOIN HDD_INTERFACE hd on h.ID = hd.HDDID";

//            List<HDD> hdds = new List<HDD>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (hdds.Count == 0 || (hdds.Count > 0 && hdds.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new HDD()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            Capacity = (int)reader["Volume"],
//                            BufferVolume = (int)reader["BufferVolume"],
//                            Speed = (int)reader["Speed"],
//                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
//                        };
//                        hdds.Add(element);
//                    }
//                    else if (hdds.Count > 0)
//                    {
//                        hdds.Last().Interface.Add(reader["Interface"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return hdds;
//        }

//        public async Task<IEnumerable<HDD>> GetHDDs(GetHDDsRequest request)
//        {
//            string expression = "SELECT * FROM HDD h JOIN HDD_INTERFACE hd on h.ID = hd.HDDID";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<HDD> hdds = new List<HDD>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (hdds.Count == 0 || (hdds.Count > 0 && hdds.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new HDD()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            Capacity = (int)reader["Volume"],
//                            BufferVolume = (int)reader["BufferVolume"],
//                            Speed = (int)reader["Speed"],
//                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
//                        };

//                        hdds.Add(element);
//                    }
//                    else if (hdds.Count > 0)
//                    {
//                        hdds.Last().Interface.Add(reader["Interface"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return hdds;
//        }

//        public async Task<IEnumerable<Motherboard>> GetMotherboards()
//        {
//            string expression = "SELECT * FROM MOTHERBOARD m JOIN MOTHERBOARD_SLOTS ms on m.ID = ms.MotherboardID";

//            List<Motherboard> motherboards = new List<Motherboard>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (motherboards.Count == 0 || (motherboards.Count > 0 && motherboards.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new Motherboard()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Socket = reader["Socket"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Chipset = reader["Chipset"].ToString().Trim(),
//                            CPUCompany = reader["CPUCompany"].ToString().Trim(),
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            MemoryType = reader["MemoryType"].ToString().Trim(),
//                            MemorySlotsAmount = (int)reader["AmountOfMemorySlots"],
//                            MemoryChanelsAmount = (int)reader["AmountOfMemoryChanels"],
//                            MaxMemory = (int)reader["MaximumMemory"],
//                            RAMMaxFreq = (int)reader["MaximumRAMFrequency"],
//                            Series = reader["Series"].ToString().Trim(),
//                            Slots = new List<string>() { reader["Slot"].ToString().Trim() },
//                            Pin = reader["Pin"].ToString().Trim(),
//                            CPUPin = reader["CPUPin"].ToString().Trim()
//                        };

//                        motherboards.Add(element);
//                    }
//                    else if (motherboards.Count > 0)
//                    {
//                        motherboards.Last().Slots.Add(reader["Slot"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return motherboards;
//        }

//        public async Task<IEnumerable<Motherboard>> GetMotherboards(GetMotherboardsRequest request)
//        {
//            string expression = "SELECT * FROM MOTHERBOARD m JOIN MOTHERBOARD_SLOTS ms on m.ID = ms.MotherboardID";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<Motherboard> motherboards = new List<Motherboard>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (motherboards.Count == 0 || (motherboards.Count > 0 && motherboards.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new Motherboard()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Socket = reader["Socket"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Chipset = reader["Chipset"].ToString().Trim(),
//                            CPUCompany = reader["CPUCompany"].ToString().Trim(),
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            MemoryType = reader["MemoryType"].ToString().Trim(),
//                            MemorySlotsAmount = (int)reader["AmountOfMemorySlots"],
//                            MemoryChanelsAmount = (int)reader["AmountOfMemoryChanels"],
//                            MaxMemory = (int)reader["MaximumMemory"],
//                            RAMMaxFreq = (int)reader["MaximumRAMFrequency"],
//                            Series = reader["Series"].ToString().Trim(),
//                            Slots = new List<string>() { reader["Slot"].ToString().Trim() },
//                            Pin = reader["Pin"].ToString().Trim(),
//                            CPUPin = reader["CPUPin"].ToString().Trim()
//                        };

//                        motherboards.Add(element);
//                    }
//                    else if (motherboards.Count > 0)
//                    {
//                        motherboards.Last().Slots.Add(reader["Slot"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return motherboards;
//        }

//        public async Task<IEnumerable<RAM>> GetRAMs()
//        {
//            string expression = "SELECT * FROM RAM";

//            List<RAM> rams = new List<RAM>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    rams.Add(
//                        new RAM()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            MemoryType = reader["MemoryType"].ToString().Trim(),
//                            Purpose = reader["Purpose"].ToString().Trim(),
//                            Volume = (int)reader["MemoryVolume"],
//                            ModuleAmount = (int)reader["ModulesAmount"],
//                            Freq = (int)reader["Frequency"],
//                            CL = reader["CASLatency"].ToString().Trim()
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return rams;
//        }

//        public async Task<IEnumerable<RAM>> GetRAMs(GetRAMsRequest request)
//        {
//            string expression = "SELECT * FROM RAM";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<RAM> rams = new List<RAM>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    rams.Add(
//                        new RAM()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            MemoryType = reader["MemoryType"].ToString().Trim(),
//                            Purpose = reader["Purpose"].ToString().Trim(),
//                            Volume = (int)reader["MemoryVolume"],
//                            ModuleAmount = (int)reader["ModulesAmount"],
//                            Freq = (int)reader["Frequency"],
//                            CL = reader["CASLatency"].ToString().Trim()
//                        }
//                    );
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return rams;
//        }

//        public async Task<IEnumerable<SSD>> GetSSDs()
//        {
//            string expression = "SELECT * FROM SSD s JOIN SSD_INTERFACE si ON s.ID = si.SSDID";

//            List<SSD> ssds = new List<SSD>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (ssds.Count == 0 || (ssds.Count > 0 && ssds.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new SSD()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Capacity = (int)reader["Volume"],
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            CellType = reader["CellType"].ToString().Trim(),
//                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
//                        };
//                        ssds.Add(element);
//                    }
//                    else if (ssds.Count > 0)
//                    {
//                        ssds.Last().Interface.Add(reader["Interface"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return ssds;
//        }

//        public async Task<IEnumerable<SSD>> GetSSDs(GetSSDsRequest request)
//        {
//            string expression = "SELECT * FROM SSD s JOIN SSD_INTERFACE si ON s.ID = si.SSDID";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<SSD> ssds = new List<SSD>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (ssds.Count == 0 || (ssds.Count > 0 && ssds.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new SSD()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Capacity = (int)reader["Volume"],
//                            Formfactor = reader["Formfactor"].ToString().Trim(),
//                            CellType = reader["CellType"].ToString().Trim(),
//                            Interface = new List<string>() { reader["Interface"].ToString().Trim() }
//                        };
//                        ssds.Add(element);
//                    }
//                    else if (ssds.Count > 0)
//                    {
//                        ssds.Last().Interface.Add(reader["Interface"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return ssds;
//        }

//        public async Task<IEnumerable<Videocard>> GetVideocards()
//        {
//            string expression = "SELECT * FROM VIDEOCARD v JOIN VIDEOCARD_CONNECTOR vc on v.ID = vc.VideocardID";

//            List<Videocard> videocards = new List<Videocard>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (videocards.Count == 0 || (videocards.Count > 0 && videocards.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new Videocard()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Proccessor = reader["GraphicalProccessor"].ToString().Trim(),
//                            VRAM = (int)reader["VRAM"],
//                            Memory = reader["Memory"].ToString().Trim(),
//                            Capacity = (int)reader["Capacity"],
//                            Family = reader["Family"].ToString().Trim(),
//                            Connectors = new List<string>() { reader["Connector"].ToString().Trim() },
//                            Length = reader["Length"].ToString().Trim(),
//                            Pin = reader["Pin"].ToString().Trim()
//                        };

//                        videocards.Add(element);
//                    }
//                    else if (videocards.Count > 0)
//                    {
//                        videocards.Last().Connectors.Add(reader["Connector"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//            return videocards;
//        }

//        public async Task<IEnumerable<Videocard>> GetVideocards(GetVideocardsRequest request)
//        {
//            string expression = "SELECT * FROM VIDEOCARD v JOIN VIDEOCARD_CONNECTOR vc on v.ID = vc.VideocardID";

//            if (request.Expression != null)
//            {
//                expression = $"{expression} WHERE {request.Expression}";
//            }

//            List<Videocard> videocards = new List<Videocard>();

//            var connection = Utility.Connection;
//            try
//            {
//                if (connection.State != ConnectionState.Open)
//                    await connection.OpenAsync();

//                SqlCommand command = new SqlCommand(expression, connection);

//                foreach (var param in request.Parameters)
//                {
//                    command.Parameters.Add(param);
//                }

//                var reader = await command.ExecuteReaderAsync();

//                while (reader.Read())
//                {
//                    if (videocards.Count == 0 || (videocards.Count > 0 && videocards.Last().ID != (int)reader["ID"]))
//                    {
//                        var element = new Videocard()
//                        {
//                            ID = (int)reader["ID"],
//                            Title = reader["Title"].ToString().Trim(),
//                            Company = reader["Company"].ToString().Trim(),
//                            Series = reader["Series"].ToString().Trim(),
//                            Proccessor = reader["GraphicalProccessor"].ToString().Trim(),
//                            VRAM = (int)reader["VRAM"],
//                            Memory = reader["Memory"].ToString().Trim(),
//                            Capacity = (int)reader["Capacity"],
//                            Family = reader["Family"].ToString().Trim(),
//                            Connectors = new List<string>() { reader["Connector"].ToString().Trim() },
//                            Length = reader["Length"].ToString().Trim(),
//                            Pin = reader["Pin"].ToString().Trim()
//                        };

//                        videocards.Add(element);
//                    }
//                    else if (videocards.Count > 0)
//                    {
//                        videocards.Last().Connectors.Add(reader["Connector"].ToString().Trim());
//                    }
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }

//            return videocards;
//        }

//        public async Task<Dictionary<string, (bool, string, List<string>)>> GetParameters(string component)
//        {
//            string expression = "SELECT * FROM FIELDS";

//            string properties = "SELECT Value FROM PROPERTIES";

//            switch (component.ToLower())
//            {
//                case "body":
//                case "charger":
//                case "cooler":
//                case "cpu":
//                case "hdd":
//                case "motherboard":
//                case "ram":
//                case "ssd":
//                case "videocard":
//                {
//                    expression = $"{expression} WHERE Component like '%{component.ToLower()}%'";

//                    Dictionary<string, (bool, string, List<string>)> fields = new Dictionary<string, (bool Addition, string Text, List<string> Values)>();

//                    var connection = Utility.Connection;
//                    try
//                    {
//                        if (connection.State != ConnectionState.Open)
//                            await connection.OpenAsync();

//                        SqlCommand command = new SqlCommand(expression, connection);

//                        var reader = await command.ExecuteReaderAsync();

//                        while (reader.Read())
//                        {
//                            fields.Add(reader["Field"].ToString().Trim(), ((bool)reader["Addition"], reader["AdditionText"].ToString().Trim(), new List<string>()));
//                        }

//                        reader.Close();

//                        for (int i = 0; i < fields.Count; i++)
//                        {
//                            var prop = $"{properties} WHERE ID = '{component.ToLower()}_{fields.ElementAt(i).Key}'";

//                            SqlCommand getProperties = new SqlCommand(prop, connection);

//                            reader = getProperties.ExecuteReader();

//                            while (reader.Read())
//                            {
//                                fields.ElementAt(i).Value.Item3.Add(reader["Value"].ToString().Trim());
//                            }
//                            reader.Close();
//                        }
//                    }
//                    finally
//                    {
//                        connection.Close();
//                    }
//                    return fields;
//                }
//                default:
//                return null;
//            }
//        }
//    }
//}
