using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Calculator;
using WebCalculator.OperatorPlugin;

namespace WebCalculator.Tests.Calculator
{
	public class MultipleOperator : IOperator
	{
		public string Code
		{
			get
			{
				return "multiple";
			}
		}

		public string Description
		{
			get
			{
				return "Multiple sum";
			}
		}

		public string Name
		{
			get
			{
				return "Multiple";
			}
		}

		public int ParameterCount
		{
			get
			{
				return 4;
			}
		}

		public int Priority
		{
			get
			{
				return 1000;
			}
		}

		public string UICode
		{
			get
			{
				return "Multiple";
			}
		}

		public double DoOperation(IList<double> parameters)
		{
			return parameters[0] + parameters[1] + parameters[2] + parameters[3];
        }
	}

	public class Multiple2Operator : IOperator
	{
		public string Code
		{
			get
			{
				return "**";
			}
		}

		public string Description
		{
			get
			{
				return "Multiple";
			}
		}

		public string Name
		{
			get
			{
				return "Multiple";
			}
		}

		public int ParameterCount
		{
			get
			{
				return 4;
			}
		}

		public int Priority
		{
			get
			{
				return 1000;
			}
		}

		public string UICode
		{
			get
			{
				return "**";
			}
		}

		public double DoOperation(IList<double> parameters)
		{
			return parameters[0] + parameters[1] + parameters[2] + parameters[3];
		}
	}
}
