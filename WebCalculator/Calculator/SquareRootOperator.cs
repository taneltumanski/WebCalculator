using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public class SquareRootOperator : IOperator
	{
		public string Code
		{
			get
			{
				return "Sqrt";
			}
		}

		public string Description
		{
			get
			{
				return "Takes the square root from the value";
			}
		}

		public string Name
		{
			get
			{
				return "Square root";
			}
		}

		public int ParameterCount
		{
			get
			{
				return 1;
			}
		}

		public int Priority
		{
			get
			{
				return 10000;
			}
		}

		public string UICode
		{
			get
			{
				return "√";
			}
		}

		public double DoOperation(IList<double> parameters)
		{
			return Math.Sqrt(parameters[0]);
		}
	}
}
