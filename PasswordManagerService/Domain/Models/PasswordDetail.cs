namespace PasswordManagerService.Domain.Models
{
    public class PasswordDetail
    {
        public long Id { get; set; }

        public string? Category { get; set; }

        public string? App { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

    }

    public class DecryptedPasswordDetail : PasswordDetail 
    {
        public string? DecryptedPasswordValue { get; set; }

    }
}
