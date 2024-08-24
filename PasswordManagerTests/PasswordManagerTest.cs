using Microsoft.Extensions.DependencyInjection;
using PasswordManagerService.Domain;
using DomainModel = PasswordManagerService.Domain.Models;
using PasswordManagerService.Repository.Models;
using PasswordManagerService.Repository;
using PasswordManagerService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PasswordManagerTests
{
    public class PasswordManagerTest
    {
        protected ServiceCollection services = new ServiceCollection();
        private PasswordManagerController passwordManagerController;

        [SetUp]
        public void Setup()
        {
            AddServices();
            var serviceProvider = services.BuildServiceProvider();
            passwordManagerController = new PasswordManagerController(serviceProvider.GetRequiredService<IPasswordManagerProcessor>());
        }

        [Test]
        public void GetPasswordById_With0AsId()
        {
            var result = passwordManagerController.GetPasswordById(0) as ObjectResult;       
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public void CreateaNewPassword_With_ValidData()
        {
            DomainModel.Password password = new DomainModel.Password();
            password.App = "TestApp";
            password.Username = "TestUsername";
            password.EncryptedPassword = "TestPassword";

            var result = passwordManagerController.CreatePassword(password) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void CreateNewPassword_With_NullValue()
        {
            var result = passwordManagerController.CreatePassword(null) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        private void AddServices() 
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<PasswordManagerContext>(ServiceLifetime.Transient);
            services.AddScoped<IPasswordManagerProcessor, PasswordManagerProcessor>();
            services.AddTransient<IPasswordManagerCommandRepository, PasswordManagerCommandRepository>();
            services.AddTransient<IPasswordManagerQueryRepository, PasswordManagerQueryRepository>();
        }
    }
}