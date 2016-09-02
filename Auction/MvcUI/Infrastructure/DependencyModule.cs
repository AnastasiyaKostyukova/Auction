using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface;
using Ninject.Modules;
using DAL.Interface.Repositories;
using EFDAL.Repositories;
using BLL;

namespace MvcUI.Infrastructure
{
	public class DependencyModule: NinjectModule
	{
		public override void Load()
		{
			Bind<ILotRepository>().To<WrongLotRepo>();
			Bind<ITestWriter>().To<TestWriter>();
		}
	}
}