using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models
{
    /// <summary>
    /// Responce of getting component parameters request
    /// </summary>
    public class GetParametersResponse
    {
        /// <summary>
        /// Name of component
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// Dictionary of filter and it's description
        /// </summary>
        public Dictionary<string, string> Titles; //Key - parameter, value - addition (or null)
        /// <summary>
        /// Options for filter
        /// </summary>
        public IEnumerable<string>[] Values { get; set; }
    }
}
