using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using StructureMap;

namespace TalentManager.Web
{
    public class StructureMapDependencyScope : IDependencyScope
    {
        private readonly IContainer container = null;

        public StructureMapDependencyScope(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            bool isConcrete = !serviceType.IsAbstract && !serviceType.IsInterface;

            return isConcrete ?
                        container.GetInstance(serviceType) :
                            container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances<object>()
                        .Where(s => s.GetType() == serviceType);
        }

        public void Dispose()
        {
            if (container != null)
                container.Dispose();
        }
    }
}