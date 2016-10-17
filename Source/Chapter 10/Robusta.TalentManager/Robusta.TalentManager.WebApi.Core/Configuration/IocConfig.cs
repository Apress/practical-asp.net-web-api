using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Robusta.TalentManager.Data;
using Robusta.TalentManager.WebApi.Core.Infrastructure;
using StructureMap;

namespace Robusta.TalentManager.WebApi.Core.Configuration
{
    public static class IocConfig
    {
        public static void RegisterDependencyResolver(HttpConfiguration config)
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();

                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a.GetName().Name.StartsWith("Robusta.TalentManager"))
                            .ToList()
                                .ForEach(a => scan.Assembly(a));
                });

                x.For<IMappingEngine>().Use(Mapper.Engine);
                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));

            });

            config.DependencyResolver = new StructureMapContainer(ObjectFactory.Container);
        }
    }
}
