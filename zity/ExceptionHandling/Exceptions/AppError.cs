namespace zity.ExceptionHandling.Exceptions
{
    public class AppError : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        public AppError(string message, int statusCode, string errorCode)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
