using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;
using Common.EF;
using Common.Entities;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Repositories;
public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public GroupRepository(TestContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Group>> GetAllWithCustomers()
    {
        IQueryable<Group> query = _dbSet;
        // var result = _dbSet.Include(x => x.FactoriesToCustomer.SelectMany(x => x.Customer.name)).ToListAsync();
        var result = _dbSet.Select(x => new Group
        {
            groupCode = x.groupCode,
            groupName = x.groupName,
            Customers = x.FactoriesToCustomer.ConvertAll(c => new Customer { name = c.Customer.name, customerId = c.Customer.customerId })
        }).ToListAsync();
        return await result;
    }
    // var result = _dbSet
    //     .Include(x => x.Group).ThenInclude(x => x.groupName)
    //     .Include(x => x.Customer).ThenInclude(x => x.name)
    //     .GroupBy(x => x.groupCode).ToListAsync();
}