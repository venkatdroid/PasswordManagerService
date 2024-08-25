using PasswordManagerService.Domain.Models;

namespace PasswordManagerService.Domain
{
    public interface IPasswordManagerProcessor
    {
        bool CreatePassword(PasswordDetail passwordToBeCreated);
        bool UpdatePassword(long id, PasswordDetail passwordToBeUpdated);
        bool DeletePassword(long Id);
        List<PasswordDetail> GetAllPasswords();
        PasswordDetail GetPasswordById(long Id);
        DecryptedPasswordDetail GetDecryptedPasswordById(long Id);
    }
}
