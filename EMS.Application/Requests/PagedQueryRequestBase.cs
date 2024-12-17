using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Requests
{
    public class PagedQueryRequestBase<T> : IRequest<QueryRecordsResponse<T>>
    {
        /// <summary>
        /// Page index determines the page number of the query. Starts at zero.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Page size determines how many records to return from this query.
        /// </summary>
        public int PageSize { get; set; }

        public string SortField { get; set; } = string.Empty;

        public string SortOrder { get; set; } = string.Empty;
    }
}
