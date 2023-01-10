using Common.EF;
using Common.Entities;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Repositories;
public class FactoryToCustomerRepository : BaseRepository<FactoryToCustomer>, IFactoryToCustomerRepository
{
    public FactoryToCustomerRepository(TestContext context) : base(context)
    {
    }
}
