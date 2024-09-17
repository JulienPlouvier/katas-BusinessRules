namespace BusinessRules.Domain.Models;

public abstract record Product(bool IsPhysical);

public record PhysicalProduct(string Name) : Product(true);
public record Book(string Title) : PhysicalProduct("Book");

public record Membership(string Name, bool IsAnUpgrade = false) : Product(false);
public record Video(string Title) : Product(false);