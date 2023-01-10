using System;
using System.ComponentModel.DataAnnotations;
namespace Common.Entities;
public class Customer
{
    [MaxLength(9)]
    public string customerId { get; set; }
    [MaxLength(50)]
    public string? name { get; set; }
    [MaxLength(150)]
    public string? address { get; set; }
    [MaxLength(50)]
    public string? phone { get; set; }

    public Customer() { }
    public Customer(string customerId, string name)
    {
        this.customerId = customerId;
        this.name = name;
    }
    public Customer(string customerId, string name, string address, string phone) : this(customerId, name)
    {
        this.address = address;
        this.phone = phone;
    }
    public Customer(string customerId, string name, string address, string phone, int factoryCode, int groupCode) : this(customerId, name, address, phone)
    {
        FactoriesToCustomer.Add(new FactoryToCustomer(factoryCode, customerId, groupCode));
    }
    // Reference
    public List<FactoryToCustomer> FactoriesToCustomer { get; } = new();
}