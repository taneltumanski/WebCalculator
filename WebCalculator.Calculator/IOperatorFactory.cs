using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public interface IOperatorFactory
	{
		IEnumerable<IOperator> Operators { get; }

		IOperator GetOperatorByCode(string code);

		bool HasOperatorForCode(string code);
	}
}
