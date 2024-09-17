namespace BusinessRules.Infrastructure;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure.DataMock;

public interface IPackingSlipGenerator
{
    public PackingSlip Generate(string address, params Product[] products);
    public PackingSlip Duplicate(PackingSlip? source);
}

public class PackingSlipGenerator : IPackingSlipGenerator
{
    public PackingSlip Generate(string address, params Product[] products)
    {
        var packingSlip = new PackingSlip(address, products);
        FakeDatabase.PackingSlips.Add(packingSlip);
        return packingSlip;
    }

    public PackingSlip Duplicate(PackingSlip? source)
    {
        if (source == null)
            throw new ArgumentNullException("source");

        var packingSlip = new PackingSlip(source.ToAddress, source.products);
        FakeDatabase.PackingSlips.Add(packingSlip);
        return packingSlip;
    }
}
