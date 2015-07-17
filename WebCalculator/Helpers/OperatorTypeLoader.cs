using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Calculator;
using WebCalculator.OperatorPlugin;
using MoreLinq;

namespace WebCalculator.Helpers
{
	public class OperatorTypeLoader : IOperatorTypeLoader
	{
		public virtual IEnumerable<IOperator> LoadOperators()
		{
			return LoadOperatorTypes().Select(LoadOperator).DistinctBy(x => x.Code);
		}

		public virtual IEnumerable<IOperator> LoadOperators(Assembly ass)
		{
			return LoadOperatorTypes(ass).Select(LoadOperator).DistinctBy(x => x.Code);
		}

		public IOperator LoadOperator(Type type)
		{
			return (IOperator)Activator.CreateInstance(type);
        }

		public virtual IEnumerable<Type> LoadOperatorTypes()
		{
			return AppDomain.CurrentDomain.GetAssemblies().SelectMany(LoadOperatorTypes);
		}

		public virtual IEnumerable<Type> LoadOperatorTypes(Assembly ass)
		{
			return ass.GetTypes().Where(IsOperatorType);
		}

		public virtual bool IsOperatorType(Type type)
		{
			return type.GetInterfaces().Contains(typeof(IOperator)) && !type.IsAbstract && !type.IsInterface && type.IsPublic;
        }
	}
}
