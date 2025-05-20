namespace FakeStoreApi.Integrations.Entities;

// Represents a shopping cart
public class Cart
{
    // Cart ID
    public int Id { get; set; }

    // User ID for the cart owner
    public int UserId { get; set; }

    // Date the cart was created or updated
    public DateTime Date { get; set; }

    // List of products in the cart
    public List<CartItem> Products { get; set; }
}