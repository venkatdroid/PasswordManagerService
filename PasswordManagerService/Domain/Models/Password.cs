namespace PasswordManagerService.Domain.Models
{
    public class Password
    {
        public long Id { get; set; }

        public string? Category { get; set; }

        public string? App { get; set; }

        public string? Username { get; set; }

        public string? EncryptedPassword { get; set; }

    }

    public class DecryptedPassword : Password 
    {
        public string? DecryptedPasswordValue { get; set; }

    }
}
