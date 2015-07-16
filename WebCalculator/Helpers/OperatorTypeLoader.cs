using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Calculator;

namespace WebCalculator.Helpers
{
	public class OperatorTypeLoader
	{
		public IEnumerable<Type> LoadOperators()
		{
			var operatorTypes = AppDomain.CurrentDomain.GetAssemblies()
																.SelectMany(ass => ass.GetTypes())
																.Where(type => type.GetInterfaces().Contains(typeof(IOperator)) && !type.IsAbstract && !type.IsInterface && type.IsPublic);

			return operatorTypes;

		}
	}
}
