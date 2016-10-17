using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using StructureMap;

namespace TalentManager.Web
{
    public class StructureMapContainer : StructureMapDependencyScope, IDependencyResolver
    {
        private readonly IContainer container = null;

        public StructureMapContainer(IContainer container)
            : base(container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(container.GetNestedContainer());
        }
    }
}