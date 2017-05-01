using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SampleApplication.Models;
using SampleApplication.Services;

namespace SampleApplication.Controllers.API
{
    [Route("customers")]
    [Authorize]
    public class CustomerApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerApiController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Lists all customers.
        /// </summary>
        [ProducesResponseType(typeof(Customer[]), 200)]
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_customerService.List());
        }

        // Will be created: GET api/customer/get/1
        // We want to have: GET api/customer/1
        [ActionName("")]
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customer = _customerService.Get(id);

            return customer == null ? (IActionResult)NotFound() : Ok(customer);
        }

        // Will be created: PUT api/customer/edit
        // We want to have: PUT api/customer
        [ActionName("")]
        [HttpPut]
        public IActionResult Edit([FromBody] Customer model)
        {
            return Ok(_customerService.Update(model));
        }

        // Will be created: POST api/customer/edit
        // We want to have: POST api/customer
        [ActionName("")]
        [HttpPost]
        public IActionResult Create([FromBody] Customer model)
        {
            return Ok(_customerService.Create(model));
        }

        [Route("throw")]
        [HttpGet]
        public IActionResult Throw()
        {
            throw new Exception("Something bad happend");
        }
    }
}