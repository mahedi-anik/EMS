using EMS.Application.DTOs;
using EMS.Application.Responses;
using MediatR;

public class GetEmployeeQuery : IRequest<QueryRecordResponse<EmployeeResponse>>
{
    public string Id { get; set; }
}
