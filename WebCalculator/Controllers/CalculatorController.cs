using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalculator.Calculator;
using WebCalculator.Helpers;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
	public class CalculatorController : Controller
	{
		private readonly IEquationSolver Solver;

		public CalculatorController(IEquationSolver solver)
		{
			this.Solver = solver;
		}

		public ActionResult Index(string input = null, IEnumerable<CalculatorParameter> parameters = null)
		{
			var model = GenerateModel(input, parameters);

			return View(model);
		}

		private CalculatorGeneratorModel GenerateModel(string input = null, IEnumerable<CalculatorParameter> parameters = null)
		{
			var operatorTypes = new OperatorTypeLoader().LoadOperators();
			var operatorFactory = new OperatorFactory(operatorTypes);

			return new CalculatorGeneratorModel() {
				Input = input ?? "",
				Operators = operatorFactory.Operators,
				Parameters = parameters ?? new List<CalculatorParameter>() {  new CalculatorParameter() {  Name = "a", Equation = "20" } }
			};
		}

		[HttpPost]
		public ActionResult UploadNewOperator()
		{
			throw new NotImplementedException();
		}

		public JsonResult Calculate(CalculatorInput model)
		{
			var returnResult = new CalculatorResult();

			if (ModelState.IsValid) {
				try {
					var parameters = model.Parameters.Select(x => new CalculatorParameter() { Name = x.Key, Equation = x.Value });

					var result = Solver.Solve(model.Input, parameters);

					returnResult.Result = result.ToString();
				} catch (CalculatorException e) {
					returnResult.Error = e.Message;
				} catch (Exception) {
					returnResult.Error = "Server error";
				}
			} else {
				returnResult.Error = "Invalid input";
			}

			return Json(returnResult, JsonRequestBehavior.AllowGet);
		}
	}
}