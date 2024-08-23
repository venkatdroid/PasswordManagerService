using PasswordManagerService.Repository.Models;

namespace PasswordManagerService.Repository
{
    public interface IPasswordManagerQueryRepository
    {
        List<Password> GetAllPasswords();
        Password GetPasswordById(long id);

    }
}
