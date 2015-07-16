using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Ninject;
using Owin;
using WebCalculator.Calculator;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

namespace WebCalculator
{
	public class Startup
	{
		/// <summary>
		/// Creates a configuration.
		/// </summary>
		/// <param name="app">
		/// The app.
		/// </param>
		public void Configuration(IAppBuilder app)
		{
		}
	}
}
