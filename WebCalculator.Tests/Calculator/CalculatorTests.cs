using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCalculator.Calculator;
using WebCalculator;

namespace WebCalculator.Tests.Calculator
{
	[TestClass]
	public class CalculatorTests
	{
		private IEquationSolver EquationSolver;

		[TestInitialize]
		public void Initialize()
		{
			var webCalcItem = new WebCalculator.Models.CalculatorInput();

            var operatorTypes = AppDomain.CurrentDomain.GetAssemblies()
																.SelectMany(ass => ass.GetTypes())
																.Where(type => type.GetInterfaces().Contains(typeof(IOperator)) && !type.IsAbstract && !type.IsInterface && type.IsPublic);

			this.EquationSolver = new EquationSolver(operatorTypes);
		}

		[TestMethod]
		public void AdditionTest()
		{
			var equations = new List<string>() {
				"1+2",
				"1 +2",
				"1 + 2",
				" 1+2",
				" 1 +		2		"
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(3, result, "Additions are not same: " + eq);
			}
		}

		[TestMethod]
		public void SubtractionTest()
		{
			var equations = new List<string>() {
				"1-2",
				"1 -2",
				"1 - 2",
				" 1-2",
				" 1 -		2		"
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(-1, result, "Subtractions are not same: " + eq);
			}
		}

		[TestMethod]
		public void MultiplyTest()
		{
			var equations = new List<string>() {
				"1*2",
				"1 *2",
				"1 * 2",
				" 1*2",
				" 1 *		2		"
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(2, result, "Multiplications are not same: " + eq);
			}
		}

		[TestMethod]
		public void DivisionTest()
		{
			var equations = new List<string>() {
				"1/2",
				"1 /2",
				"1 / 2",
				" 1/2",
				" 1 /		2		"
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(1/2d, result, "Divisions are not same: " + eq);
            }
		}

		[TestMethod]
		public void ParenthesisTest()
		{
			var equations = new List<string>() {
				"(1 + 2) * 3",
				"3 * (1 + 2)",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(9, result, "Parenthesis are not same: " + eq);
			}
		}

		[TestMethod]
		public void ParenthesisTest2()
		{
			var equations = new List<string>() {
				"(2 * (1 + 4)) * 3",
				"(1 + 4) * (2 + 4)",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(30, result, "Parenthesis are not same: " + eq);
			}
		}

		[TestMethod]
		public void SqrtOperatorTest()
		{
			var equations = new List<string>() {
				"Sqrt 3",
				"Sqrt(3)",
				"Sqrt(1 + 2)",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(Math.Sqrt(3), result, "Parenthesis are not same: " + eq);
			}
		}

		[TestMethod]
		public void SqrtOperatorTest2()
		{
			var equations = new List<string>() {
				"Sqrt 3 + 2",
				"Sqrt(3) + 2",
				"Sqrt(1 + 2) + 2",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(Math.Sqrt(3) + 2, result, "Parenthesis are not same: " + eq);
			}
		}

		[TestMethod]
		public void SqrtOperatorTest3()
		{
			var equations = new List<string>() {
				"a * 6 + 5 - Sqrt 4",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq, new[] { new CalculatorParameter() { Name = "a", Equation = "20" } });

				Assert.AreEqual(123, result, "Parenthesis are not same: " + eq);
			}
		}

		[TestMethod]
		public void MultipleOperatorTest()
		{
			var equations = new List<string>() {
				"multiple 3 2 4 3",
				"multiple(3 2 4 3)",
				"multiple 4 3 2 3",
				"multiple 4, 3, 2, 3",
				"multiple(4, 3, 2, 3)",
                "multiple 3,2,4,3",
				"multiple(3,2,4,3)",
				"multiple (3,2,4,3) ",
				"multiple( 3,2,4,3 )",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(12, result, "Parenthesis are not same: " + eq);
			}
		}

		[TestMethod]
		public void MultipleOperatorTest2()
		{
			var equations = new List<string>() {
				"multiple 3 2 4 3 + 10",
				"multiple(3 2 4 3)+ 10",
				"multiple 4 3 2 3  + 10",
				"multiple 4, 3, 2, 3+ 10",
				"multiple(4, 3, 2, 3+10)",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(22, result, "Parenthesis are not same");
			}
		}

		[TestMethod]
		public void MultipleOperatorTest3()
		{
			var equations = new List<string>() {
                "30 + multiple 3 2 4 3 + 10",
				"30 + multiple(3 2 4 3)+ 10",
				"30+ multiple 4 3 2 3  + 10",
				"30+multiple 4, 3, 2, 3+ 10",
				"30 +	 multiple(4, 3, 2, 3+10)",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(52, result, "Parenthesis are not same");
			}
		}
		[TestMethod]
		public void MultipleOperatorTest4()
		{
			var equations = new List<string>() {
				"multiple 3 (2*10) 4 3",
				//"multiple(3 2 * 10 4 3)",
				//"multiple 4 3 2 * 10 3",
				//"multiple 4, 3, 2 * 10, 3",
				//"multiple(4, 3, 2 * 10, 3)",
				//"multiple 3,2 * 10,4,3",
				//"multiple(3,2 * 10,4,3)",
				//"multiple (3,2 * 10,4,3) ",
				//"multiple( 3,2 * 10,4,3 )",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(30, result, "Parenthesis are not same: " + eq);
			}
		}

		[TestMethod]
		public void ParametersTest()
		{
			var parameters = new List<CalculatorParameter>() {
				new CalculatorParameter() { Name = "a", Equation = "20" },
				new CalculatorParameter() { Name = "b", Equation = "5" },
				new CalculatorParameter() { Name = "c", Equation = "5 * 5" },
				new CalculatorParameter() { Name = "d", Equation = "multiple 5,5,5,5" }
			};

			double result;

			result = EquationSolver.Solve("a + b", parameters);
			Assert.AreEqual(25, result, "Parameters are not same");

			result = EquationSolver.Solve("b + a", parameters);
			Assert.AreEqual(25, result, "Parameters are not same");

			result = EquationSolver.Solve("a + b + b", parameters);
			Assert.AreEqual(30, result, "Parameters are not same");

			result = EquationSolver.Solve("a + b + c", parameters);
			Assert.AreEqual(50, result, "Parameters are not same");

			result = EquationSolver.Solve("d * c + a", parameters);
			Assert.AreEqual(520, result, "Parameters are not same");
		}

		[TestMethod]
		public void Multiple2OperatorTest()
		{
			var equations = new List<string>() {
				"** 3 2 4 3",
				"**(3 2 4 3)",
				"** 4 3 2 3",
				"** 4, 3, 2, 3",
				"**(4, 3, 2, 3)",
				"** 3,2,4,3",
				"**(3,2,4,3)",
				"** (3,2,4,3) ",
				"**( 3,2,4,3 )",
			};

			foreach (var eq in equations) {
				var result = EquationSolver.Solve(eq);

				Assert.AreEqual(12, result, "Parenthesis are not same: " + eq);
			}
		}

		//[TestMethod]
		//public void NestingParametersTest()
		//{
		//	var parameters = new List<CalculatorParameter>() {
		//		new CalculatorParameter() { Name = "a", Equation = "20" },
		//		new CalculatorParameter() { Name = "b", Equation = "5" },
		//		new CalculatorParameter() { Name = "c", Equation = "5 * 5" },
		//		new CalculatorParameter() { Name = "d", Equation = "multiple 5,5,5,5" }
		//	};

		//	double result;

		//	result = EquationSolver.Solve("a + b", parameters);
		//	Assert.AreEqual(25, result, "Parameters are not same");

		//	result = EquationSolver.Solve("b + a", parameters);
		//	Assert.AreEqual(25, result, "Parameters are not same");

		//	result = EquationSolver.Solve("a + b + b", parameters);
		//	Assert.AreEqual(30, result, "Parameters are not same");

		//	result = EquationSolver.Solve("a + b + c", parameters);
		//	Assert.AreEqual(50, result, "Parameters are not same");

		//	result = EquationSolver.Solve("d * c + a", parameters);
		//	Assert.AreEqual(520, result, "Parameters are not same");
		//}
	}
}
