using Common.Entities;

namespace TestWebApi.Services;
public interface ITestUnitOfWork
{
    Task<IEnumerable<Group>> GetGroupsAndTheirCustomers();
    Task AddCustomerAndConnectToGroupAsync(Customer customer);
}