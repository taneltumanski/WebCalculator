using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Models
{
	public class CalculatorInput
	{
		[Required]
		public string Input { get; set; }

		public Dictionary<string, string> Parameters { get; set; }

		public CalculatorInput()
		{
			Parameters = new Dictionary<string, string>();
		}
	}
}
