using System.ComponentModel.DataAnnotations;

namespace Common.Entities;
public class FactoryToCustomer
{
    public int factoryCode { get; set; }
    [MaxLength(9)]
    public string customerId { get; set; }
    public int groupCode { get; set; }

    public FactoryToCustomer() { }
    public FactoryToCustomer(int factoryCode, string customerId, int groupCode)
    {
        this.factoryCode = factoryCode;
        this.customerId = customerId;
        this.groupCode = groupCode;
    }

    // Reference
    public Factory Factory { get; set; }
    public Customer Customer { get; set; }
    public Group Group { get; set; }
}