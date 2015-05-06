using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DynamicFieldsDemo.Models
{
	public class StrongViewModel
	{
		[Required]
		[MinLength(5)]
		public string Name { get; set; }

		[Required]
		[Phone]
		public string Value { get; set; }

		public override string ToString()
		{
			return string.Format("Name = {0}; Value = {1}", Name, Value);
		}
	}
}