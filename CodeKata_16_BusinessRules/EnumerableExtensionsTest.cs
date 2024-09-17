namespace BusinessRules_Tests;

using BusinessRules.Domain.Models;
using BusinessRules.Infrastructure.SyntaxicSugar;
using System.Collections.Generic;

public class EnumerableExtensionsTest
{
    [Fact]
    public void AddOrUpdateTest()
    {
        var list = new List<Customer>
        {
            new ("Franz", "Ferdinan", "ff@gmail.com", "3 rue des tulipes"),
            new ("Rocky", "Balboa", "ADRIENNE@gmail.com", "31 chemin des gants"),
            new ("Stephen", "Curry", "stephC@gmail.com", "13 Avenue des bagues"),
        };

        var updatedList = list.AddOrReplace(new("Franz", "Ferdinan", "ff@gmail.com", "3 rue Jean Jaures"), x => x.Surname == "Franz")
        .AddOrReplace(new("Stephen", "King", "bouh@gmail.com", "chambre 237"), x => x.Surname == "Stephen" && x.Name == "King")
        .AddOrReplace(new("Rocky", "Balboa", "rocky.balboa@yopmail.com", "31 chemin des gants"), x => x.Surname == "Rocky")
        .AddOrReplace(new("Rocky", "Balboa", "rocky.balboa@yopmail.com", "Boucherie du 15"), x => x.Surname == "Rocky");

        updatedList.Should().BeEquivalentTo(new List<Customer>
        {
            new("Franz", "Ferdinan", "ff@gmail.com", "3 rue Jean Jaures"),
            new("Rocky", "Balboa", "rocky.balboa@yopmail.com", "Boucherie du 15"),
            new("Stephen", "Curry", "stephC@gmail.com", "13 Avenue des bagues"),
            new("Stephen", "King", "bouh@gmail.com", "chambre 237")
        });
    }
}
