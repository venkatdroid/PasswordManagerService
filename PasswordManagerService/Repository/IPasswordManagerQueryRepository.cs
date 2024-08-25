using PasswordManagerService.Repository.Models;
using DomainModel = PasswordManagerService.Domain.Models;

namespace PasswordManagerService.Repository
{
    public interface IPasswordManagerQueryRepository
    {
        List<DomainModel.PasswordDetail> GetAllPasswords();
        Password GetPasswordById(long id);

    }
}
