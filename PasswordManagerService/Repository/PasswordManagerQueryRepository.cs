using Microsoft.EntityFrameworkCore;
using PasswordManagerService.Repository.Models;

namespace PasswordManagerService.Repository
{
    public class PasswordManagerQueryRepository : IPasswordManagerQueryRepository
    {
        private PasswordManagerContext _passwordManagerContext;
        public PasswordManagerQueryRepository(PasswordManagerContext passwordManagerContext)
        {
            _passwordManagerContext = passwordManagerContext;
        }

        public List<Password> GetAllPasswords()
        {
            return _passwordManagerContext.Passwords.AsNoTracking().ToList();
        }

        public Password GetPasswordById(long id)
        {
            return _passwordManagerContext.Passwords.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
    }
}
