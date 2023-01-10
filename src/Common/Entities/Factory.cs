using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities;
public class Factory
{
    public int factoryCode { get; set; }
    [MaxLength(50)]
    public string? factoryName { get; set; }
    public int groupCode { get; set; }

    public Factory(int factoryCode, string factoryName, int groupCode)
    {
        this.factoryCode = factoryCode;
        this.factoryName = factoryName;
        this.groupCode = groupCode;
    }

    // Reference
    public Group Group { get; set; }
    public List<FactoryToCustomer> FactoriesToCustomer { get; } = new();
}