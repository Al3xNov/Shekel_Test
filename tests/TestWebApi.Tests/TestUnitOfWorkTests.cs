using Common.EF;
using Common.Entities;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestWebApi.Services;

namespace TestWebApi.Tests;

public class TestUnitOfWorkTests : IClassFixture<DatabaseFixture>
{
    private readonly string _connString;
    private readonly DbContextOptions<TestContext> _builderOptions;
    private TestContext NewTestContext => new(_builderOptions);
    private readonly TestUnitOfWork _testService;
    public TestUnitOfWorkTests(DatabaseFixture fixture)
    {
        _connString = "Encrypt=False;TrustServerCertificate=true;" + fixture.MsSqlContainer.ConnectionString;

        var serviceProvider = new ServiceCollection().AddEntityFrameworkSqlServer().BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<TestContext>();
        builder.UseSqlServer(_connString);
        builder.UseInternalServiceProvider(serviceProvider);
        _builderOptions = builder.Options;
        _testService = new TestUnitOfWork(NewTestContext);
        // NewTestContext.Database.EnsureDeleted();
        NewTestContext.Database.EnsureCreated();
    }
    [Fact]
    public async void Should_Add_And_Get_From_Db()
    {
        // arrange 
        var customer1 = new Customer() { name = "n1", customerId = "c1" };
        var customer2 = new Customer() { name = "n2", customerId = "c2" };
        var customer3 = new Customer() { name = "n3", customerId = "c3" };
        var group1 = new Group() { groupCode = 1, groupName = "g1" };
        var group2 = new Group() { groupCode = 2, groupName = "g2" };
        var factorytocustomer1 = new FactoryToCustomer() { groupCode = 1, factoryCode = 11, customerId = "c1" };
        var factorytocustomer2 = new FactoryToCustomer() { groupCode = 1, factoryCode = 11, customerId = "c2" };
        var factorytocustomer3 = new FactoryToCustomer() { groupCode = 2, factoryCode = 22, customerId = "c3" };

        var context = NewTestContext;
        await context.Customers.AddRangeAsync(customer1, customer2, customer3);
        await context.Groups.AddRangeAsync(group1, group2);
        await context.FactoriesToCustomer.AddRangeAsync(factorytocustomer1, factorytocustomer2, factorytocustomer3);
        await context.SaveChangesAsync();
        await context.DisposeAsync();

        var newCustomer = new Customer("c4", "n4", "a4", "p4", 22, 2);
        // act
        await _testService.AddCustomerAndConnectToGroupAsync(newCustomer);
        var groupsAndTheirCustomers = await _testService.GetGroupsAndTheirCustomers();

        // asssert
        Assert.Equal(2, groupsAndTheirCustomers.Count());
        Assert.Equal(2, groupsAndTheirCustomers.First().Customers.Count);
        Assert.Equal(2, groupsAndTheirCustomers.Last().Customers.Count);
    }
}