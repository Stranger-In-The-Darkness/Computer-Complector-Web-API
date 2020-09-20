using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Data.Special
{
	[Serializable]
	[Table("RULE_TYPE")]
	public class RuleType
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Type { get; set; }

		public RuleType()
		{
			Type = string.Empty;
		}

		public RuleType(int id, string type)
		{
			ID = id;
			Type = type;
		}
	}
}
