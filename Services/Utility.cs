using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace ComputerComplectorWebAPI.Services
{
    public class Utility
    {        
        //public static SqlConnection Connection { get; private set; } = new SqlConnection(@"Data Source = LAPTOP-PF5NTLFM\SQLEXPRESS; Initial Catalog = PC-components; User=pcComponentsReadOnly; Password=pcComponent#ReadOnly");
        public static SqlConnection Connection { get; private set; } = new SqlConnection(@"Data Source = LAPTOP-PF5NTLFM\SQLEXPRESS; Initial Catalog = PC-components; Integrated Security = True");
    }
}
