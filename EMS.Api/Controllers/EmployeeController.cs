using EMS.Application.Attributes;
using EMS.Application.Commands.Employees;
using EMS.Application.DTOs;
using EMS.Application.Queries.Employees;
using EMS.Application.Responses;
using EMS.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Api.Controllers
{
    [ApiController]
    [AuthorizationNotRequired]
    [Route("api/[controller]")]
    public class EmployeeController
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor
        public EmployeeController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods
        [HttpGet(EndpointRoutes.Action_GetEmployee)]
        public async Task<QueryRecordResponse<EmployeeResponse>> GetEmployee([FromQuery] GetEmployeeQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet(EndpointRoutes.Action_GetEmployees)]
        public async Task<QueryRecordsResponse<EmployeeResponse>> GetEmployees([FromQuery] GetEmployeesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost(EndpointRoutes.Action_CreateEmployee)]
        public async Task<ServiceResponse> CreateEmployee(CreateEmployeeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(EndpointRoutes.Action_UpdateEmployee)]
        public async Task<ServiceResponse> UpdateEmployee(UpdateEmployeeCommand command)
        {
            return await _mediator.Send(command);
        }
        #endregion
    }
}
