using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRegistration.Entities;
using CustomerRegistration.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        CustomerRepository repository;

        public CustomerController()
        {
            repository = new CustomerRepository();
        }

        [HttpGet("{cpf}")]
        public IActionResult Get(string cpf)
        {
            try
            {
                return Ok(repository.Select(cpf));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                repository.Insert(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Customer customer)
        {
            try
            {
                repository.Update(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            try
            {
                repository.Delete(cpf);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
