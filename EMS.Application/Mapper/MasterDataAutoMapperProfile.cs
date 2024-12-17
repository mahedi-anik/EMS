using AutoMapper;
using EMS.Application.Commands.Departments;
using EMS.Application.Commands.Employees;
using EMS.Application.Commands.PerformanceReviews;
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
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<UpdateEmployeeCommand, Employee>();

            CreateMap<Department, DepartmentResponse>()
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.ManagerId))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName));
            CreateMap<CreateDepartmentCommand, Department>();
            CreateMap<UpdateDepartmentCommand, Department>();

            CreateMap<PerformanceReview, PerformanceReviewResponse>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName));
            CreateMap<CreatePerformanceReviewCommand, PerformanceReview>();
            CreateMap<UpdatePerformanceReviewCommand, PerformanceReview>();

        }
    }
}
