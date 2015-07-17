using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Calculator;
using System.Collections.Immutable;

namespace WebCalculator.Helpers
{
	public class CachedOperatorTypeLoader : OperatorTypeLoader
	{
		private static ImmutableList<IOperator> _cachedOperators = ImmutableList<IOperator>.Empty;
		private static ImmutableList<Type> _cachedTypes = ImmutableList<Type>.Empty;

		static CachedOperatorTypeLoader()
		{
			var allOperatorTypeLoader = new OperatorTypeLoader();
			var types = allOperatorTypeLoader.LoadOperatorTypes();
			var operators = types.Select(allOperatorTypeLoader.LoadOperator);

			_cachedTypes = _cachedTypes.AddRange(types);
			_cachedOperators = _cachedOperators.AddRange(operators);

			AppDomain.CurrentDomain.AssemblyLoad += (sender, args) => {
				try {
					var operatorTypeLoader = new OperatorTypeLoader();
                    var newAssemblyTypes = operatorTypeLoader.LoadOperatorTypes(args.LoadedAssembly);
					var newOperators = newAssemblyTypes.Select(operatorTypeLoader.LoadOperator);

					_cachedTypes = _cachedTypes.AddRange(newAssemblyTypes);
					_cachedOperators = _cachedOperators.AddRange(newOperators);
				} catch (Exception e) {
					NLog.LogManager.GetCurrentClassLogger().Error(e, "Loading operators from a new assembly failed");
				}
			};
		}

		public override IEnumerable<IOperator> LoadOperators()
		{
			return _cachedOperators;
		}

		public override IEnumerable<Type> LoadOperatorTypes()
		{
			return _cachedTypes;
		}
	}
}
