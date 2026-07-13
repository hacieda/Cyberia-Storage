namespace Application.Errors;

public class AppException : Exception
{
    public ErrorCode Code { get; }

    public AppException(ErrorCode code, string message)
        : base(message)
    {
        Code = code;
    }
}
