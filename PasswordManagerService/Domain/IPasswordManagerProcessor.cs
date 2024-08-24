using PasswordManagerService.Domain.Models;
using EntityModel = PasswordManagerService.Repository.Models;

namespace PasswordManagerService.Domain
{
    public interface IPasswordManagerProcessor
    {
        bool CreatePassword(Password passwordToBeCreated);
        bool UpdatePassword(long id, Password passwordToBeUpdated);
        bool DeletePassword(long Id);
        List<EntityModel.Password> GetAllPasswords();
        Password GetPasswordById(long Id);
        DecryptedPassword GetDecryptedPasswordById(long Id);
    }
}
