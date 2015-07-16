using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public class OperatorFactory : IOperatorFactory
	{
		private readonly ReadOnlyCollection<IOperator> _operators;

		public IEnumerable<IOperator> Operators { get { return _operators; } }

		public OperatorFactory(IEnumerable<Type> operatorTypes) : this(TypesToOperators(operatorTypes)) { }
		public OperatorFactory(IEnumerable<IOperator> operators)
		{
			var operatorList = operators.ToList();

			ValidateOperators(operatorList);

            this._operators = new ReadOnlyCollection<IOperator>(operatorList);
		}

		private static IEnumerable<IOperator> TypesToOperators(IEnumerable<Type> operatorTypes)
		{
			return operatorTypes.Select(TypeToOperator);
		}

		private static IOperator TypeToOperator(Type type)
		{
			if (!type.GetInterfaces().Contains(typeof(IOperator))) {
				throw new ArgumentException(string.Format("The type {0} must implement IOperator", type.FullName));
			}

			return (IOperator)Activator.CreateInstance(type);
		}

		private void ValidateOperators(IEnumerable<IOperator> operatorList)
		{
			var codes = operatorList.Select(op => op.Code).ToList();
			var duplicateCodes = operatorList.Count() != operatorList.Distinct().Count();

			if (duplicateCodes) {
				var groupedCodes = operatorList.GroupBy(x => x.Code);
				var moreThanOneCodeGroups = groupedCodes.Where(x => x.Count() > 1);

				var sb = new StringBuilder();
				sb.AppendLine("Operators contain duplicate codes:");

				foreach (var group in moreThanOneCodeGroups) {
					var groupString = string.Format("\\tThe code {0} is present {1} times in operators {2}", group.Key, group.Count(), string.Join(", ", group.Select(x => x.GetType().Name)));
					sb.AppendLine(groupString);
				}

				throw new ArgumentException(sb.ToString());
			}

			if (codes.Any(x => string.IsNullOrWhiteSpace(x))) {
				var whitespaceCodes = operatorList.Where(x => string.IsNullOrWhiteSpace(x.Code));

				var sb = new StringBuilder();
				sb.AppendLine("Operators contain empty codes:");

				foreach (var op in whitespaceCodes) {
					var groupString = string.Format("\\tThe code of {0} is empty", op.GetType().Name);
					sb.AppendLine(groupString);
				}

				throw new ArgumentException(sb.ToString());
			}
		}

		public IOperator GetOperatorByCode(string code)
		{
			return Operators.Single(op => op.Code == code);
		}

		public bool HasOperatorForCode(string code)
		{
			return Operators.Any(op => op.Code == code);
		}
	}
}
