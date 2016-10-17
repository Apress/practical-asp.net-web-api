using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Robusta.TalentManager.Domain;
using Robusta.TalentManager.WebApi.Dto;

namespace Robusta.TalentManager.WebApi.Core.Configuration
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
