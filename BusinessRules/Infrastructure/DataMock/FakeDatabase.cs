namespace BusinessRules.Infrastructure.DataMock;

using BusinessRules.Domain.Models;

public static class FakeDatabase
{
    public static List<PackingSlip> PackingSlips = new List<PackingSlip>();
    public static List<Customer> Customers = new List<Customer>();

    public static void Clear()
    {
        PackingSlips.Clear();
        Customers.Clear();
    }
}
