using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator.Operators
{
	public class DivideOperator : IOperator
	{
		public string Code
		{
			get
			{
				return "/";
			}
		}

		public string Description
		{
			get
			{
				return "Divides 2 numbers";
			}
		}

		public string Name
		{
			get
			{
				return "Division";
			}
		}

		public string UICode
		{
			get
			{
				return "/";
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
				return 1000;
			}
		}

		public double DoOperation(IList<double> parameters)
		{
			return parameters[0] / parameters[1];
		}
	}
}
