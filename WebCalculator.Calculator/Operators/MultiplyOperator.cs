﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.OperatorPlugin;

namespace WebCalculator.Calculator.Operators
{
	public class MultiplyOperator : IOperator
	{
		public string Code
		{
			get
			{
				return "*";
			}
		}

		public string Description
		{
			get
			{
				return "Multiplies 2 numbers";
			}
		}

		public string Name
		{
			get
			{
				return "Multiplication";
			}
		}

		public string UICode
		{
			get
			{
				return "*";
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
			return parameters[0] * parameters[1];
		}
	}
}
