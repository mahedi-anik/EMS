﻿using EMS.Application.DTOs;
using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Queries.Employees
{
    public class GetEmployeeQuery : IRequest<QueryRecordResponse<EmployeeResponse>>
    {
        public string Id { get; set; }
    }
}
