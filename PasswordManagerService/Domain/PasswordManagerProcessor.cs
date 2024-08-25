using PasswordManagerService.Repository;
using DomainModel = PasswordManagerService.Domain.Models;
using EntityModel = PasswordManagerService.Repository.Models;

namespace PasswordManagerService.Domain
{
    public class PasswordManagerProcessor : IPasswordManagerProcessor
    {
        private IPasswordManagerCommandRepository _commandRepository;
        private IPasswordManagerQueryRepository _queryRepository;

        public PasswordManagerProcessor(IPasswordManagerCommandRepository commandRepository, IPasswordManagerQueryRepository queryRepository) 
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository; 
        }

        public bool CreatePassword(DomainModel.PasswordDetail passwordToBeCreated)
        {
            return _commandRepository.CreateNewPassword(MapDomainModelToEntityModel(passwordToBeCreated));
        }

        public bool UpdatePassword(long id, DomainModel.PasswordDetail passwordToBeUpdated)
        {
            EntityModel.Password exisitingPassword = _queryRepository.GetPasswordById(id);

            if (exisitingPassword == null) 
            {
                throw new InvalidOperationException("Password Not Found");
            }

            exisitingPassword.App = passwordToBeUpdated.App;
            exisitingPassword.Username = passwordToBeUpdated.Username;
            exisitingPassword.Category = passwordToBeUpdated.Category;
            exisitingPassword.EncryptedPassword = Base64Decode(passwordToBeUpdated.Password);            

            return _commandRepository.UpdatePassword(exisitingPassword);
        }

        public bool DeletePassword(long Id)
        {
            return _commandRepository.DeletePassword(Id);
        }

        public List<DomainModel.PasswordDetail> GetAllPasswords() 
        {
            return _queryRepository.GetAllPasswords();
        }

        public DomainModel.PasswordDetail GetPasswordById(long Id) 
        {
            EntityModel.Password requestedPassword = _queryRepository.GetPasswordById(Id);

            if (requestedPassword == null) 
            {
                throw new Exception("Id Not Found !");
            }

            DomainModel.PasswordDetail result = MapEntityModelToDomainModel(requestedPassword);

            return result;
        }

        public DomainModel.DecryptedPasswordDetail GetDecryptedPasswordById(long Id)
        {
            EntityModel.Password requestedPassword = _queryRepository.GetPasswordById(Id);

            if (requestedPassword == null)
            {
                throw new Exception("Id Not Found !");
            }

            DomainModel.DecryptedPasswordDetail result = new DomainModel.DecryptedPasswordDetail()
            {
                App = requestedPassword.App,
                Username = requestedPassword.Username,
                Category = requestedPassword.Category,
                Password = requestedPassword.EncryptedPassword,
                DecryptedPasswordValue = Base64Decode(requestedPassword.EncryptedPassword)
            };

            return result;
        }


        #region Private Methods

        private static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private DomainModel.PasswordDetail MapEntityModelToDomainModel(EntityModel.Password entityModel) 
        {
            DomainModel.PasswordDetail domainModel = new DomainModel.PasswordDetail();
            domainModel.Id = entityModel.Id;
            domainModel.App = entityModel.App;
            domainModel.Username = entityModel.Username;
            domainModel.Category = entityModel.Category;
            domainModel.Password = entityModel.EncryptedPassword;

            return domainModel;
        }

        private EntityModel.Password MapDomainModelToEntityModel(DomainModel.PasswordDetail domainModel)
        {
            EntityModel.Password entityModel = new EntityModel.Password();
            entityModel.Id = domainModel.Id;
            entityModel.App = domainModel.App;
            entityModel.Username = domainModel.Username;
            entityModel.Category = domainModel.Category;
            entityModel.EncryptedPassword = Base64Encode(domainModel.Password);

            return entityModel;
        }

        #endregion
    }
}
