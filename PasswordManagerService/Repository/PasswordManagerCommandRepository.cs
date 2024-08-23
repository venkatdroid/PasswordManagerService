using Microsoft.EntityFrameworkCore;
using PasswordManagerService.Repository.Models;
using System.Net.Sockets;

namespace PasswordManagerService.Repository
{
    public class PasswordManagerCommandRepository : IPasswordManagerCommandRepository
    {
        private PasswordManagerContext _passwordManagerContext;
        public PasswordManagerCommandRepository(PasswordManagerContext passwordManagerContext) 
        { 
            _passwordManagerContext = passwordManagerContext;
        }

        public bool CreateNewPassword(Password passwordToSave) 
        {
            _passwordManagerContext.Passwords.Add(passwordToSave);
            _passwordManagerContext.SaveChanges();
            return true;
        }

        public bool UpdatePassword(Password passwordToUpdate)
        {
            _passwordManagerContext.Passwords.Update(passwordToUpdate);
            _passwordManagerContext.SaveChanges();
            return true;
        }

        public bool DeletePassword(long Id)
        {
            Password passwordToBeRemoved = _passwordManagerContext.Passwords.Where(x => x.Id == Id).FirstOrDefault();

            if(passwordToBeRemoved != null)
            {
               _passwordManagerContext.Passwords.Remove(passwordToBeRemoved);
               _passwordManagerContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}


//docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Admin@123" -p 1433:1433--name sql1 --hostname sql1 -d  mcr.microsoft.com/mssql/server:2022 - latest


//Scaffold - DbContext "Server=localhost,1433;Database=PasswordManager;User Id=SA;Password=Admin@123; TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models
