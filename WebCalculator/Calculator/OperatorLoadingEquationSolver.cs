using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Helpers;

namespace WebCalculator.Calculator
{
	public class OperatorLoadingEquationSolver : IEquationSolver
	{
		private readonly IEquationSolver DefaultSolver;

		public OperatorLoadingEquationSolver()
		{
			var types = new OperatorTypeLoader().LoadOperatorTypes();

            DefaultSolver = new EquationSolver(types);
        }

		public double Solve(string equation)
		{
			return DefaultSolver.Solve(equation);
		}

		public double Solve(string equation, IEnumerable<CalculatorParameter> parameters)
		{
			return DefaultSolver.Solve(equation, parameters);
		}
	}
}
