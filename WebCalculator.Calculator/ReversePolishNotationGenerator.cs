using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public class ReversePolishNotationGenerator
	{
		public Stack<string> Generate(IEnumerable<string> tokens, IOperatorFactory operatorFactory, IEnumerable<string> parameterNames)
		{
			var operationStack = new Stack<string>();
			var RPNStack = new Stack<string>();
			var parameterNamesSet = new HashSet<string>(parameterNames);

			foreach (var token in tokens) {
				if (token == "(") {
					operationStack.Push(token);
				} else if (token == ")") {
					while (operationStack.Count != 0 && operationStack.Peek() != "(") {
						RPNStack.Push(operationStack.Pop());
					}

					operationStack.Pop();

					if (operationStack.Count != 0 && operatorFactory.HasOperatorForCode(operationStack.Peek())) {
						RPNStack.Push(operationStack.Pop());
					}
				} else if (IsNumber(token)) {
					RPNStack.Push(token);
				} else if (operatorFactory.HasOperatorForCode(token)) {
					while (operationStack.Count != 0
							&& operatorFactory.HasOperatorForCode(operationStack.Peek())
							&& operatorFactory.GetOperatorByCode(token).Priority <= operatorFactory.GetOperatorByCode(operationStack.Peek()).Priority) {
						RPNStack.Push(operationStack.Pop());
					}

					operationStack.Push(token);
				} else if (operatorFactory.HasOperatorForCode(token)) {
					operationStack.Push(token);
				} else if (parameterNamesSet.Contains(token)) {
					RPNStack.Push(token);
				} else {
					throw new CalculatorException(string.Format("Unrecognized token: {0}", token));
				}
			}

			while (operationStack.Count != 0) {
				RPNStack.Push(operationStack.Pop());
			}

			// Reverse the stack
			RPNStack = new Stack<string>(RPNStack);

			return RPNStack;
		}

		private bool IsNumber(string str)
		{
			double d;

			return double.TryParse(str.Replace('.',','), out d);
		}
	}
}
