﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
		private readonly IOperatorTypeLoader OperatorTypeLoader;

		public CalculatorController(IEquationSolver solver, IOperatorTypeLoader typeLoader)
		{
			this.Solver = solver;
			this.OperatorTypeLoader = typeLoader;
		}

		public ActionResult Index(string input = null, IEnumerable<CalculatorParameter> parameters = null)
		{
			var model = GenerateModel(input, parameters);

			return View(model);
		}

		private CalculatorGeneratorModel GenerateModel(string input = null, IEnumerable<CalculatorParameter> parameters = null)
		{
			var operators = this.OperatorTypeLoader.LoadOperators();

			return new CalculatorGeneratorModel() {
				Input = input ?? "",
				Operators = operators,
				Parameters = parameters ?? new List<CalculatorParameter>()
			};
		}

		[HttpPost]
		public ActionResult UploadNewOperator(HttpPostedFileBase file)
		{
			if (file.ContentLength > 0) {
				byte[] assemblyData = null;

				using (var ms = new MemoryStream()) {
					file.InputStream.CopyTo(ms);

					assemblyData = ms.ToArray();
				}

				var newAssembly = Assembly.Load(assemblyData);
				var newOperators = this.OperatorTypeLoader.LoadOperators(newAssembly);

				return Json(new { Operators = newOperators });
			}

			return Json(new { Error = "Could not upload file" });
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