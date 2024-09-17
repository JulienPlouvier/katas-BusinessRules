namespace BusinessRules.Domain.Models;

public record PackingSlip(string ToAddress, Product[] products);