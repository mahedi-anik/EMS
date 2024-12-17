using AutoMapper;
using EMS.Application.DTOs;
using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Mapper
{
    public class MasterDataAutoMapperProfile : Profile
    {
        public MasterDataAutoMapperProfile()
        {
            CreateMap<Employee, EmployeeResponse>();

        }
    }
}
