using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public interface IEquationSolver
	{
		double Solve(string equation);
		double Solve(string equation, IEnumerable<CalculatorParameter> parameters);
	}
}
