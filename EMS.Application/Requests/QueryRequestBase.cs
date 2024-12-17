using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Requests
{
    public class QueryRequestBase<T> : IRequest<QueryRecordsResponse<T>>
    {
    }
}
