using Common.EF;
using Common.Entities;
using Common.Interfaces;
using Common.Repositories;

namespace TestWebApi.Services;
public class TestUnitOfWork : BaseUnitOfWork, ITestUnitOfWork
{
    private readonly IGroupRepository _groupRepository;
    private readonly ICustomerRepository _customerRepository;
    // private readonly IFactoryToCustomerRepository _factoryToCustomerRepository;
    public TestUnitOfWork(TestContext context) : base(context)
    {
        _groupRepository = new GroupRepository(context);
        _customerRepository = new CustomerRepository(context);
    }

    public async Task AddCustomerAndConnectToGroupAsync(Customer customer)
    {
        await _customerRepository.AddSingleAsync(customer);
        await CompleteAsync();
    }

    public async Task<IEnumerable<Group>> GetGroupsAndTheirCustomers()
    {
        return await _groupRepository.GetAllWithCustomers();
    }
}