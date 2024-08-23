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

        public bool CreatePassword(DomainModel.Password passwordToBeCreated)
        {
            return _commandRepository.CreateNewPassword(MapDomainModelToEntityModel(passwordToBeCreated));
        }

        public bool UpdatePassword(DomainModel.Password passwordToBeUpdated)
        {
            return _commandRepository.UpdatePassword(MapDomainModelToEntityModel(passwordToBeUpdated));
        }

        public bool DeletePassword(long Id)
        {
            return _commandRepository.DeletePassword(Id);
        }

        public List<EntityModel.Password> GetAllPasswords() 
        {
            return _queryRepository.GetAllPasswords();
        }

        public DomainModel.Password GetPasswordById(long Id, bool decryptPassword) 
        {
            EntityModel.Password requestedPassword = _queryRepository.GetPassword(Id);

            if (requestedPassword == null) 
            {
                throw new Exception("Id Not Found !");
            }

            DomainModel.Password result = MapEntityModelToDomainModel(requestedPassword);

            if (decryptPassword) 
            {
                result.DecryptedPassword = Base64Decode(result.EncryptedPassword);
            }

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

        private DomainModel.Password MapEntityModelToDomainModel(EntityModel.Password entityModel) 
        {
            DomainModel.Password domainModel = new DomainModel.Password();
            domainModel.Id = entityModel.Id;
            domainModel.App = entityModel.App;
            domainModel.Username = entityModel.Username;
            domainModel.EncryptedPassword = entityModel.EncryptedPassword;

            return domainModel;
        }

        private EntityModel.Password MapDomainModelToEntityModel(DomainModel.Password domainModel)
        {
            EntityModel.Password entityModel = new EntityModel.Password();
            entityModel.Id = domainModel.Id;
            entityModel.App = domainModel.App;
            entityModel.Username = domainModel.Username;
            entityModel.EncryptedPassword = Base64Encode(domainModel.EncryptedPassword);

            return entityModel;
        }

        #endregion
    }
}
