using PasswordManagerService.Repository.Models;

namespace PasswordManagerService.Repository
{
    public interface IPasswordManagerCommandRepository
    {
        bool CreateNewPassword(Password passwordToSave);
        bool UpdatePassword(Password passwordToUpdate);
        bool DeletePassword(long Id);
    }
}
