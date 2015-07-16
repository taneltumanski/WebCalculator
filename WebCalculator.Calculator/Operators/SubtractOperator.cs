using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator.Operators
{
	public class SubtractOperator : IOperator
	{
		public string Code
		{
			get
			{
				return "-";
			}
		}

		public string Description
		{
			get
			{
				return "Subtracts 2 numbers";
			}
		}

		public string Name
		{
			get
			{
				return "Subtraction";
			}
		}

		public string UICode
		{
			get
			{
				return "-";
			}
		}

		public int ParameterCount
		{
			get
			{
				return 2;
			}
		}

		public int Priority
		{
			get
			{
				return 100;
			}
		}

		public double DoOperation(IList<double> parameters)
		{
			return parameters[0] - parameters[1];
		}
	}
}
