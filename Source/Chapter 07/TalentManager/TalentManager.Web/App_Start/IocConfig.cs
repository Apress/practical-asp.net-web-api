using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using StructureMap;
using TalentManager.Data;

namespace TalentManager.Web
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
                        .Where(a => a.GetName().Name.StartsWith("TalentManager"))
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