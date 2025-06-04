namespace WebApplication2.Model;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    public List<PurchaseHistory> Purchases { get; set; } = new();
}