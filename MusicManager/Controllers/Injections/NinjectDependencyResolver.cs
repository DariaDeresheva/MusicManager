using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MusicManager.BusinessLogic.Services;
using Ninject;
using Ninject.Syntax;

namespace MusicManager.Controllers.Injections
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private readonly IKernel _kernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			_kernel = kernelParam;
			RegisterDependencies(_kernel);
		}

		public object GetService(Type serviceType)
		{
			return _kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.GetAll(serviceType);
		}

		private static void RegisterDependencies(IBindingRoot kernel)
		{
			kernel.Bind<IService>().To<CommonService>();
		}
	}

}