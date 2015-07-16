using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculator.Calculator
{
	public interface IOperator
	{
		/// <summary>
		/// Name of the operator
		/// </summary>
		/// <returns></returns>
		string Name
		{ get; }

		/// <summary>
		/// Description of the operator
		/// </summary>
		/// <returns></returns>
		string Description
		{ get; }

		/// <summary>
		/// Operator code that is used to identify it in the equation
		/// </summary>
		/// <returns></returns>
		string Code
		{ get; }

		/// <summary>
		/// Operator code that is used to in the equation shown to the users, should be a user friendly name
		/// </summary>
		/// <returns></returns>
		string UICode
		{ get; }

		/// <summary>
		/// Defines the priority of the operation
		/// </summary>
		/// <returns></returns>
		int Priority
		{ get; }

		/// <summary>
		/// Defines the parameter count
		/// </summary>
		/// <returns></returns>
		int ParameterCount
		{ get; }

		double DoOperation(IList<double> parameters);
	}
}
