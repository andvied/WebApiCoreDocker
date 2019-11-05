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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _db.Customers;
        }

        // GET api/customers/1234567890
        [HttpGet("{securityNumber}")]
        public IActionResult Get(string securityNumber)
        {

            var response = new CustomerResponse();

            if (!string.IsNullOrEmpty(securityNumber))
            {
                var existing = _db.Customers.Find(securityNumber);
                if (existing == null)
                {
                    response.ErrorMessage = "Customer does not exist.";
                    return BadRequest(response);
                }

                return Ok(existing);
            }

            return BadRequest(response);

        }

        // POST api/customers
        [HttpPost]
        public ActionResult Post([FromForm]Customer value)
        {
            var response = new CustomerResponse();

            if (!string.IsNullOrEmpty(value.SecurityNumber))
            {
                var existing = _db.Customers.Find(value.SecurityNumber);
                if (existing != null)
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
                }
            }

            return BadRequest(response);
        }
    }
}