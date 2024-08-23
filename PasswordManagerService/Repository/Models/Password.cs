namespace PasswordManagerService.Repository.Models;

public partial class Password
{
    public long Id { get; set; }

    public string? Category { get; set; }

    public string? App { get; set; }

    public string? Username { get; set; }

    public string? EncryptedPassword { get; set; }
}
