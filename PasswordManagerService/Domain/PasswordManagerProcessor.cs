﻿using PasswordManagerService.Repository;
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

        public bool UpdatePassword(long id, DomainModel.Password passwordToBeUpdated)
        {
            EntityModel.Password exisitingPassword = _queryRepository.GetPasswordById(id);

            if (exisitingPassword == null) 
            {
                throw new InvalidOperationException("Password Not Found");
            }

            exisitingPassword.App = passwordToBeUpdated.App;
            exisitingPassword.Username = passwordToBeUpdated.Username;
            exisitingPassword.Category = passwordToBeUpdated.Category;
            exisitingPassword.EncryptedPassword = Base64Decode(passwordToBeUpdated.EncryptedPassword);            

            return _commandRepository.UpdatePassword(exisitingPassword);
        }

        public bool DeletePassword(long Id)
        {
            return _commandRepository.DeletePassword(Id);
        }

        public List<EntityModel.Password> GetAllPasswords() 
        {
            return _queryRepository.GetAllPasswords();
        }

        public DomainModel.Password GetPasswordById(long Id) 
        {
            EntityModel.Password requestedPassword = _queryRepository.GetPasswordById(Id);

            if (requestedPassword == null) 
            {
                throw new Exception("Id Not Found !");
            }

            DomainModel.Password result = MapEntityModelToDomainModel(requestedPassword);

            return result;
        }

        public DomainModel.DecryptedPassword GetDecryptedPasswordById(long Id)
        {
            EntityModel.Password requestedPassword = _queryRepository.GetPasswordById(Id);

            if (requestedPassword == null)
            {
                throw new Exception("Id Not Found !");
            }

            DomainModel.DecryptedPassword result = new DomainModel.DecryptedPassword()
            {
                App = requestedPassword.App,
                Username = requestedPassword.Username,
                Category = requestedPassword.Category,
                EncryptedPassword = requestedPassword.EncryptedPassword,
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
