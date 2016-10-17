using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TalentManager.Domain;
using TalentManager.Web.Models;

namespace TalentManager.Web
{
    public static class DtoMapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<EmployeeDto, Employee>();
            Mapper.CreateMap<Employee, EmployeeDto>();
        }
    }
}