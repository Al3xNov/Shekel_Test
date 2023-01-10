using Common.Entities;

namespace Common.Interfaces;
public interface IGroupRepository : IBaseRepository<Group>
{
    Task<IEnumerable<Group>> GetAllWithCustomers();
}