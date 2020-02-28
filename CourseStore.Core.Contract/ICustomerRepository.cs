using CourseStore.Core.Domain;

namespace CourseStore.Core.Contract
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}
