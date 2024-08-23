using Microsoft.AspNetCore.Mvc;
using PasswordManagerService.Domain;
using PasswordManagerService.Domain.Models;
using System.Net;
using EntityModel = PasswordManagerService.Repository.Models;

namespace PasswordManagerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PasswordManagerController : ControllerBase
    {
        public IPasswordManagerProcessor _processor;
        public PasswordManagerController(IPasswordManagerProcessor processor) 
        {
            _processor = processor;
        }

        [HttpGet("GetAllPasswords")]
        public IActionResult GetAllPasswords()
        {
            try
            {
                List<EntityModel.Password> result = _processor.GetAllPasswords();
                return Ok(result);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error While Fetching GetAllPasswords ,",ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetPasswordById(long id, [FromQuery] bool decryptPassword)
        {
            try
            {
                if (id <= 0) 
                {
                    return BadRequest("Id should be greater than zero");   
                }

                Password result = _processor.GetPasswordById(id, decryptPassword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Fetching GetPasswordById : {id} ,", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePassword([FromBody] Password passwordToBeCreated)
        {
            try
            {
                bool result = _processor.CreatePassword(passwordToBeCreated);
                return CreatedAtRoute(passwordToBeCreated, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Creating Password", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePassword(int id, [FromBody] Password passwordToBeUpdated)
        {
            try
            {
                bool result = _processor.UpdatePassword(id, passwordToBeUpdated);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Updating Password", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePassword(int id)
        {
            try
            {
                bool result = _processor.DeletePassword(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Deleting Password", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
