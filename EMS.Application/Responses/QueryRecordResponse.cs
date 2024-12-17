using EMS.Application.Requests;

namespace EMS.Application.Responses
{
    public class QueryRecordResponse<TRecord> : CQRSBase
    {
        public string RequestUri { get; set; } = string.Empty;

        public ErrorResponse Errors { get; set; } = new ErrorResponse();

        public TRecord? Result { get; set; }

        public bool IsSuccess => Errors.Errors is null || Errors.Errors.Length == 0;

        public QueryRecordResponse<TRecord> BuildSuccessResponse(TRecord result, string requestUri = "")
        {
            return new QueryRecordResponse<TRecord>()
            {
                Result = result,
                RequestUri = requestUri
            };
        }

        public QueryRecordResponse<TRecord> BuildErrorResponse(ErrorResponse errors, string requestUri = "")
        {
            return new QueryRecordResponse<TRecord>()
            {
                Errors = errors,
                RequestUri = requestUri
            };
        }
    }
}
