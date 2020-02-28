using CourseStore.Core.Domain;
using System;
using System.Linq;

namespace CourseStore.Services.ApplicationServices
{
    public class CustomerService
    {
        private readonly CourseService _courseService;
        public CustomerService(CourseService courseService)
        {
            _courseService = courseService;
        }
        private decimal CalculatePrice(CustomerStatus status, DateTime? statusExpirationDate, LicensingModel licensingModel)
        {
            decimal price;
            switch (licensingModel)
            {
                case LicensingModel.TowDays:
                    price = 4;
                    break;
                case LicensingModel.LifeLong:
                    price = 8;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (status == CustomerStatus.Advanced && (statusExpirationDate == null || statusExpirationDate >= DateTime.UtcNow))
                price *= 0.75m;
            return price;

        }
        public void PuchaseCourse(Customer customer, Course course)
        {
            DateTime? expirationDate = _courseService.GetExpirationDate(course.LicensingModel);
            decimal price = CalculatePrice(customer.CustomerStatus, customer.StatusExpirationDate, course.LicensingModel);
            var purchased = new PurchasedCourse
            {
                CourseId = course.Id,
                CustomerId = customer.Id,
                ExpirationDate = expirationDate,
                Price = price
            };
            customer.PuchasedCourses.Add(purchased);
            customer.MoneySpent += price;
        }
        public bool PromoteCustomer(Customer customer)
        {
            if (customer.PuchasedCourses.Count(c => c.ExpirationDate == null || c.ExpirationDate.Value > DateTime.UtcNow.AddDays(-30)) < 2)
                return false;
            if (customer.PuchasedCourses.Where(c => c.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(c => c.Price) < 100m)
                return false;
            customer.CustomerStatus = CustomerStatus.Advanced;
            customer.StatusExpirationDate = DateTime.UtcNow.AddYears(1);
            return true; ;
        }
    }
}
