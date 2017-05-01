using Microsoft.AspNetCore.Mvc;
using SampleApplication.Models;
using SampleApplication.Services;

namespace SampleApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View(_customerService.List());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!_customerService.Delete(id))
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View("AddOrEdit");
        }

        [ActionName("Create")]
        [HttpPost]
        public IActionResult CreateConfirmed(Customer model)
        {
            var customer = _customerService.Create(model);

            return RedirectToAction("Edit", new {customer.Id});
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerService.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View("AddOrEdit", customer);
        }

        [ActionName("Edit")]
        [HttpPost]
        public IActionResult EditConfirmed(Customer model)
        {
            var customer = _customerService.Update(model);

            return RedirectToAction("Edit", new {customer.Id });
        }
    }
}