using Microsoft.EntityFrameworkCore;
using PasswordManagerService.Repository.Models;
using DomainModel = PasswordManagerService.Domain.Models; 

namespace PasswordManagerService.Repository
{
    public class PasswordManagerQueryRepository : IPasswordManagerQueryRepository
    {
        private PasswordManagerContext _passwordManagerContext;
        public PasswordManagerQueryRepository(PasswordManagerContext passwordManagerContext)
        {
            _passwordManagerContext = passwordManagerContext;
        }

        public List<DomainModel.PasswordDetail> GetAllPasswords()
        {
            List<DomainModel.PasswordDetail> result = (from password in _passwordManagerContext.Passwords.AsNoTracking()
                          select new DomainModel.PasswordDetail
                          {
                              Id = password.Id,
                              Username = password.Username,
                              Password = password.EncryptedPassword,
                              Category = password.Category,
                              App = password.App
                          }).ToList();

            return result;
        }

        public Password GetPasswordById(long id)
        {
            return _passwordManagerContext.Passwords.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
    }
}
