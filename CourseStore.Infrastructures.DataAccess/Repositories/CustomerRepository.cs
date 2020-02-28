using CourseStore.Core.Contract;
using CourseStore.Core.Domain;
using CourseStore.Infrastructures.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseStore.Infrastructures.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepository<Customer, CourseStoreContext>, ICustomerRepository
    {
        public CustomerRepository(CourseStoreContext context) : base(context)
        {

        }
        public Customer GetByEmail(string email)
        {
            return _dbContext.Customers.Include(c => c.PuchasedCourses).
                ThenInclude(c => c.Course).FirstOrDefault(c => c.Email == email);
        }
        
        public override Customer GetById(long id)
        {
            return _dbContext.Customers.Include(c => c.PuchasedCourses).
              ThenInclude(c => c.Course).FirstOrDefault(c => c.Id == id);
        }
    }
}
