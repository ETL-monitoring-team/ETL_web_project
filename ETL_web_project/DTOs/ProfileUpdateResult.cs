public class ProfileUpdateResult
{
    public bool Success { get; }
    public string ErrorMessage { get; }
    public string ErrorField { get; }

    public ProfileUpdateResult(bool success, string? msg = null, string? field = null)
    {
        Success = success;
        ErrorMessage = msg ?? "";
        ErrorField = field;
    }

    public static ProfileUpdateResult Ok()
        => new ProfileUpdateResult(true);

    public static ProfileUpdateResult Fail(string msg, string? field = null)
        => new ProfileUpdateResult(false, msg, field);
}
