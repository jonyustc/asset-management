namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetStatusCodeMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request,Somethings Wrong, ",
                401 => "Unauthorized, You are not authorized",
                404 => "Not found, Content not found, May be End Point Does't exists",
                500 => "Internal Server Error,Please Check Null Exception",
                _ => null
            };
        }
    }
}