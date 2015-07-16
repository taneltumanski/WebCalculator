using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public class CalculatorException : Exception
	{
		public CalculatorException(string msg) : base(msg) {}
	}
}
