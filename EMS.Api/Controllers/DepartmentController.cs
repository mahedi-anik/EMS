using EMS.Application.Commands.Departments;
using EMS.Application.DTOs;
using EMS.Application.Queries.Departments;
using EMS.Application.Responses;
using EMS.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor
        public DepartmentController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Methods
        [HttpGet(EndpointRoutes.Action_GetDepartment)]
        public async Task<QueryRecordResponse<DepartmentResponse>> GetDepartment([FromQuery] GetDepartmentQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet(EndpointRoutes.Action_GetDepartments)]
        public async Task<QueryRecordsResponse<DepartmentResponse>> GetDepartments([FromQuery] GetDepartmentsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost(EndpointRoutes.Action_CreateDepartment)]
        public async Task<ServiceResponse> CreateDepartment(CreateDepartmentCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(EndpointRoutes.Action_UpdateDepartment)]
        public async Task<ServiceResponse> UpdateDepartment(UpdateDepartmentCommand command)
        {
            return await _mediator.Send(command);
        }
        #endregion
    }
}
