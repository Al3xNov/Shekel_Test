using Microsoft.AspNetCore.Mvc;
using Common.Entities;
using TestWebApi.Services;

namespace TestWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly ITestUnitOfWork _service;

    public TestController(ILogger<TestController> logger, ITestUnitOfWork unitOfWork)
    {
        _logger = logger;
        _service = unitOfWork;
    }

    [HttpGet]
    [Route("group")]
    public async Task<IEnumerable<Group>> GetGroupsAndTheirCustomers()
    {
        return await _service.GetGroupsAndTheirCustomers();
    }

    [HttpPost]
    [Route("customer")]
    public async Task Post([FromBody] Customer customerWithGroupFactory)
    {
        await _service.AddCustomerAndConnectToGroupAsync(customerWithGroupFactory);
    }
}
