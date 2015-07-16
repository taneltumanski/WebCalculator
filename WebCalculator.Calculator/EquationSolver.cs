using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public class EquationSolver : IEquationSolver
	{
		private readonly IOperatorFactory OperatorFactory;

		public EquationSolver(IEnumerable<Type> operatorTypes) : this(new OperatorFactory(operatorTypes)) { }
		public EquationSolver(IEnumerable<IOperator> operators) : this(new OperatorFactory(operators)) { }
		public EquationSolver(IOperatorFactory operatorFactory)
		{
			if (operatorFactory == null) {
				throw new ArgumentNullException("operatorFactory");
			}

			this.OperatorFactory = operatorFactory;
		}

		public double Solve(string equation)
		{
			return Solve(equation, new List<CalculatorParameter>());
		}

		public double Solve(string equation, IEnumerable<CalculatorParameter> parameters)
		{
			parameters = parameters ?? new List<CalculatorParameter>();	// We dont care if we have parameters or not

			var parameterValues = SolveParameters(parameters);

			return Solve(equation, parameterValues);
		}

		private double Solve(string equation, IEnumerable<SolvedCalculatorParameter> parameters)
		{
			if (string.IsNullOrWhiteSpace(equation)) {
				throw new ArgumentNullException("equation");
			}

			equation = NormalizeEquation(equation);

			var tokens = equation.Split(' ');

			var RPNStack = new ReversePolishNotationGenerator().Generate(tokens, this.OperatorFactory, parameters.Select(x => x.Name));

			var result = CalculateRPNValue(RPNStack, parameters);

			return result;
		}

		private double CalculateRPNValue(Stack<string> RPNStack, IEnumerable<SolvedCalculatorParameter> parameters)
		{
			var resultStack = new Stack<double>();

			var parameterSet = new HashSet<SolvedCalculatorParameter>(parameters);

			while (RPNStack.Count > 0) {
				var val = RPNStack.Pop();

				if (IsNumber(val)) {
					resultStack.Push(double.Parse(val.Replace('.', ',')));
				} else if (parameterSet.Any(x => x.Name == val)) {
					var paramVal = parameterSet.Single(x => x.Name == val);
					resultStack.Push(paramVal.Value);
				} else {
					var op = this.OperatorFactory.GetOperatorByCode(val);

					if (resultStack.Count < op.ParameterCount) {
						throw new CalculatorException(string.Format("Not enough parameters for function {0}", op.Name));
					}

					var operatorParams = new List<double>();

					for (int i = 0; i < op.ParameterCount; i++) {
						operatorParams.Add(resultStack.Pop());
					}

					operatorParams.Reverse();

					var operationResult = op.DoOperation(operatorParams);

					resultStack.Push(operationResult);
				}
			}

			if (resultStack.Count > 1) {
				throw new CalculatorException("The equation has too many values");
			}

			return resultStack.Pop();
		}

		private IEnumerable<SolvedCalculatorParameter> SolveParameters(IEnumerable<CalculatorParameter> parameters)
		{
			var parameterValues = new List<SolvedCalculatorParameter>();

			foreach (var parameter in parameters) {
				var parameterParameters = parameterValues.TakeWhile(x => x.Name != parameter.Name).ToList();

				var solvedParam = new SolvedCalculatorParameter() {
					Name = parameter.Name,
					Value = Solve(parameter.Equation, parameterParameters)
				};

				parameterValues.Add(solvedParam);
			}

			return parameterValues;
		}

		private bool IsNumber(string str)
		{
			double d;

			return double.TryParse(str.Replace('.', ','), out d);
		}

		private string NormalizeEquation(string equation)
		{
			// Add spaces infront and behind of all alphanumeric characters
			equation = Regex.Replace(equation, @"([\w\.]+)", " $1 ");

			// Add spaces infront and behind of all ( and ) characters
			equation = Regex.Replace(equation, @"([\(\)])", " $1 ");

			// Remove all commas
			equation = equation.Replace(",", "");

			// Replace all whitespace with single spaces
			equation = Regex.Replace(equation, @"(\s+)", " ");

			// Replace spaces around dots with nothing
			equation = equation.Replace(" . ", ".");

			equation = equation.Trim();

			return equation;
		}

		
	}

	internal class SolvedCalculatorParameter
	{
		public string Name
		{ get; set; }
		public double Value
		{ get; set; }
	}
}
