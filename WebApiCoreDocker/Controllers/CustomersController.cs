using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreDocker.DAL;
using WebApiCoreDocker.Models;

namespace WebApiCoreDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CustomersController(AppDbContext db)
        {
            _db = db;
        }       

        // GET api/customers/1234567890
        [HttpGet("{securityNumber}")]
        public IActionResult Get(string securityNumber)
        {   
            if (!string.IsNullOrEmpty(securityNumber))
            {
                var customer = _db.Customers.Find(securityNumber);
                if (customer == null)
                {                   
                    return NotFound();
                }

                return Ok(customer);
            }

            return BadRequest();
        }

        // POST api/customers
        [HttpPost]
        public ActionResult Post([FromForm]Customer value)
        {
            var response = new CustomerResponse();

            if (!string.IsNullOrEmpty(value.SecurityNumber))
            {
                var customer = _db.Customers.Find(value.SecurityNumber);
                if (customer != null)
                {
                    response.ErrorMessage = "Customer already exists.";
                    return BadRequest(response);
                }

                try
                {
                    _db.Customers.Add(value);
                    _db.SaveChanges();

                    return Created("", response);
                }
                catch (Exception ex)
                {
                    // Log message ex
                    response.ErrorMessage = "Error on Customer saving ...";

                    return BadRequest(response);
                }
            }

            return BadRequest();
        }

        // PUT api/customers/1234567890
        [HttpPut("{securityNumber}")]
        public IActionResult Put(string securityNumber, [FromForm] Customer value)
        {
            if (!string.IsNullOrEmpty(securityNumber) && securityNumber == value.SecurityNumber)
            {

                var customer = _db.Customers.Find(securityNumber);

                if (customer != null)
                {
                    try
                    {
                        customer.PhoneNumber = value.PhoneNumber;
                        customer.EmailAddress = value.EmailAddress;
                        _db.Update(customer);
                        _db.SaveChanges();

                        return Ok();
                    }
                    catch (Exception ex)
                    {                        
                        // Log message
                        var response = new CustomerResponse();
                        response.ErrorMessage = "Error on Customer updating ...";

                        return BadRequest(response);
                    }
                }
            }

            return BadRequest();
        }

        // DELETE api/customers/1234567890
        [HttpDelete("{securityNumber}")]
        public IActionResult Delete(string securityNumber)
        {
            if (!string.IsNullOrEmpty(securityNumber))
            {
                var customer = _db.Customers.Find(securityNumber);

                if (customer != null)
                {
                    try
                    {
                        _db.Remove(customer);
                        _db.SaveChanges();

                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        // Log message                        
                        var response = new CustomerResponse();
                        response.ErrorMessage = "Error on Customer deleting ...";

                        return BadRequest(response);
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }
    }
}