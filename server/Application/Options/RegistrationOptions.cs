namespace Application.Options;

public enum RegistrationMode
{
    Open,
    InviteOnly,
    Disabled
}

public class RegistrationOptions
{
    public RegistrationMode Mode { get; set; } = RegistrationMode.Open;
    public int DefaultStorageLimitGb { get; set; } = 1;
}
