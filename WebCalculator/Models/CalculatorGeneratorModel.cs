using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Calculator;

namespace WebCalculator.Models
{
	public class CalculatorGeneratorModel
	{
		public IEnumerable<IOperator> Operators { get; set; }
		public IEnumerable<CalculatorParameter> Parameters { get; set; }
		public string Input { get; set; }
	}
}
