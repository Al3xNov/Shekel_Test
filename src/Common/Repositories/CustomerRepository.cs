using Common.EF;
using Common.Entities;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Repositories;
public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(TestContext context) : base(context)
    {
    }
}
