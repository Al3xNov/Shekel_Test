using System.ComponentModel.DataAnnotations;

namespace Common.Entities;
public class Group
{
    public int groupCode { get; set; }
    [MaxLength(150)]
    public string? groupName { get; set; }

    public Group() { }
    public Group(int groupCode, string groupName)
    {
        this.groupCode = groupCode;
        this.groupName = groupName;
    }
    public Group(int groupCode, string groupName, List<Customer> customerList)
    {
        this.groupCode = groupCode;
        this.groupName = groupName;
        Customers = customerList;
    }
    // Reference
    public List<Factory> Factories { get; } = new();
    public List<FactoryToCustomer> FactoriesToCustomer { get; } = new();
    public List<Customer> Customers { get; set; } = new();
}