namespace App_QLPK.Application.Common;

/// <summary>
///     Lỗi nghiệp vụ , map sang HTTP 400 ở tầng Api.
/// </summary>
public class AppException : Exception // kế thừa Exception mặc định của .NET
{
    public AppException(string message) : base(message)
    {
    }
}