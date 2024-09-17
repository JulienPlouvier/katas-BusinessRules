namespace BusinessRules.Domain.Models;

public record Customer(string Surname, string Name, string Mail, string Address, string? ActiveMembership = null)
{
    public string FullName() => $"{Surname} {Name}";
}
