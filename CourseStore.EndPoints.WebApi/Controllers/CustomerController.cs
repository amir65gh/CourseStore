using CourseStore.Core.Contract;
using CourseStore.Core.Domain;
using CourseStore.Services.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseStore.EndPoints.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICourseRrepository _courseRrepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerService _customerService;
        public CustomerController(ICourseRrepository courseRrepository, ICustomerRepository customerRepository, CustomerService customerService)
        {
            _customerRepository = customerRepository;
            _courseRrepository = courseRrepository;
            _customerService = customerService;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
                return NotFound();
            return Json(customer);
        }
        [HttpGet]
        public JsonResult GetList()
        {
            IReadOnlyList<Customer> customers = _customerRepository.GetList();
            return Json(customers);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (_customerRepository.GetByEmail(customer.Email) != null)
                    return BadRequest("This email address is already registered!");
                customer.Id = 0;
                customer.CustomerStatus = CustomerStatus.Regular;
                _customerRepository.Add(customer);
                _customerRepository.Save();
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, new { error = e.Message });
            }
        }
        [HttpPost]
        [Route("{id}")]
        public IActionResult Update(long id, [FromBody]Customer item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                    return BadRequest("This requrst is not valid!");
                customer.FirstName = item.FirstName;
                customer.LastName = item.LastName;
                _customerRepository.Update(customer);
                _customerRepository.Save();
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, new { erroe = e.Message });
            }
        }
        [HttpPost]
        [Route("{id}/course")]
        public IActionResult PurchaseCourse(long id, [FromQuery]long courseId)
        {
            try
            {
                var course = _courseRrepository.GetById(courseId);
                if (course == null)
                    return BadRequest($"The course number: {courseId} is not valid!");
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                    return BadRequest($"The customer number: {id} is not valid!");
                if (customer.PuchasedCourses.Any(c => c.CourseId == course.Id && (c.ExpirationDate.Value == null || c.ExpirationDate.Value >= DateTime.UtcNow)))
                    return BadRequest($"Course {course.Name} is already registered!");
                _customerService.PuchaseCourse(customer, course);
                _customerRepository.Save();
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("{id}/promotion")]
        public IActionResult PromoteCustomer(long id)
        {
            try
            {
                Customer customer = _customerRepository.GetById(id);
                if (customer == null)
                    return BadRequest($"Customer with id {id} is not valid");
                if (customer.CustomerStatus == CustomerStatus.Advanced && (customer.StatusExpirationDate == null || customer.StatusExpirationDate.Value < DateTime.UtcNow))
                    return BadRequest("This customer is in Advanced status");
                var result = _customerService.PromoteCustomer(customer);
                if (!result)
                    return BadRequest("It's impossible to promote selected customer!");
                _customerRepository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
