using System;
using System.Collections.Generic;
using System.Reflection;
using WebCalculator.Calculator;
using WebCalculator.OperatorPlugin;

namespace WebCalculator.Helpers
{
	public interface IOperatorTypeLoader
	{
		IEnumerable<IOperator> LoadOperators();
		IEnumerable<IOperator> LoadOperators(Assembly ass);
		IOperator LoadOperator(Type type);

		bool IsOperatorType(Type type);
		IEnumerable<Type> LoadOperatorTypes();
		IEnumerable<Type> LoadOperatorTypes(Assembly ass);
	}
}