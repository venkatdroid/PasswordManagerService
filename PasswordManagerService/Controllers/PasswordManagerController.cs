using Microsoft.AspNetCore.Mvc;
using PasswordManagerService.Domain;
using PasswordManagerService.Domain.Models;
using System.Net;

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

        /// <summary>
        /// Gets All the passwords 
        /// </summary>
        /// <returns> List of Passwords </returns>
        [HttpGet("GetAllPasswords")]
        public IActionResult GetAllPasswords()
        {
            try
            {
                List<PasswordDetail> result = _processor.GetAllPasswords();
                
                return Ok(result);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error While Fetching GetAllPasswords ,",ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Get Password By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns Requested Password </returns>
        [HttpGet("{id}")]
        public IActionResult GetPasswordById(long id)
        {
            try
            {
                if (id <= 0) 
                {
                    return BadRequest("Id should be greater than zero");   
                }

                PasswordDetail result = _processor.GetPasswordById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Fetching GetPasswordById : {id} ,", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get Decrypted Password By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Returns Requested Deccrypted Password </returns>
        [HttpGet("DecryptedPasswordDetail/{id}")]
        public IActionResult GetDecryptedPasswordById(long id) 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id should be greater than zero");
                }

                PasswordDetail result = _processor.GetDecryptedPasswordById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Fetching GetDecryptedPasswordById : {id} ,", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a New password
        /// </summary>
        /// <param name="passwordToBeCreated"></param>
        /// <returns>True if password is created</returns>
        [HttpPost]
        public IActionResult CreatePassword([FromBody] PasswordDetail passwordToBeCreated)
        {
            try
            {
                if (passwordToBeCreated == null) 
                {
                    return BadRequest();                    
                }

                bool result = _processor.CreatePassword(passwordToBeCreated);
                return CreatedAtRoute(passwordToBeCreated, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Creating Password", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the given password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordToBeUpdated"></param>
        /// <returns>True if password is updated</returns>
        [HttpPut("{id}")]
        public IActionResult UpdatePassword(int id, [FromBody] PasswordDetail passwordToBeUpdated)
        {
            try
            {
                if (id <= 0 && passwordToBeUpdated == null) 
                {
                    return BadRequest();
                }

                bool result = _processor.UpdatePassword(id, passwordToBeUpdated);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Updating Password", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes Password
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the password is Deleted</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePassword(int id)
        {
            try
            {
                if (id <= 0) 
                {
                    return BadRequest();    
                }

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
