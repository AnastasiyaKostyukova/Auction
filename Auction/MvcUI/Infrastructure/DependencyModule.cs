using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface;
using Ninject.Modules;
using DAL.Interface.Repositories;
using EFDAL.Repositories;
using BLL;
using BLL.Interface.Services;
using BLL.Services;

namespace MvcUI.Infrastructure
{
	public class DependencyModule: NinjectModule
	{
		public override void Load()
		{
			Bind<ILotService>().To<LotService>();
            Bind<ICRUDLotService>().To<CRUDLotService>();
            Bind<ICRUDUserService>().To<CRUDUserService>();
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
        }
	}
}