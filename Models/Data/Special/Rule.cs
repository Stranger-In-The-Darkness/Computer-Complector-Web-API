using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerComplectorWebAPI.Models.Data.Special
{
	[Serializable]
	[Table("RULES")]
	public class Rule
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string FirstComponent { get; set; }
		[Required]
		public string FirstProperty { get; set; }
		[Required]
		public string SecondComponent { get; set; }
		[Required]
		public string SecondProperty { get; set; }
		[Required]
		public int RelationID { get; set; }
		[ForeignKey("RelationID")]
		public RuleRelation Relation { get; set; }
		[Required]
		public int RuleTypeID { get; set; }
		[ForeignKey("RuleTypeID")]
		public RuleType RuleType { get; set; }

		public Rule()
		{
			FirstComponent = string.Empty;
			FirstProperty = string.Empty;
			SecondComponent = string.Empty;
			SecondProperty = string.Empty;
			RelationID = 1;
			RuleTypeID = 1;
		}

		public Rule(int id, string firstComponent, string firstProperty, string secondComponent,  string secondProperty, int relation, int ruleType)
		{
			FirstComponent = firstComponent;
			FirstProperty = firstProperty;
			SecondComponent = secondComponent;
			SecondProperty = secondProperty;
			RelationID = relation;
			RuleTypeID = ruleType;
		}

		public override bool Equals(object obj)
		{
			if (obj is Rule rule)
			{
				return (FirstComponent?.Equals(rule?.FirstComponent ?? "") ?? false) &&
					(FirstProperty?.Equals(rule?.FirstProperty ?? "") ?? false) &&
					(Relation?.Relation.Equals(rule?.Relation.Relation ?? "") ?? false) &&
					(SecondComponent?.Equals(rule?.SecondComponent ?? "") ?? false) &&
					(SecondProperty?.Equals(rule?.SecondProperty ?? "") ?? false);
			}
			return false;
		}

		public override string ToString()
		{
			return $"{FirstComponent}.{FirstProperty} {Relation.Relation} {SecondComponent}.{SecondProperty}";
		}
	}
}
