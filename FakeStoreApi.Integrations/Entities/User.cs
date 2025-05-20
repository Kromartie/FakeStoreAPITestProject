namespace FakeStoreApi.Integrations.Entities;

// Represents a user in the system
public class User
{
    // User ID
    public int Id { get; set; }

    // User email
    public string Email { get; set; }

    // User username
    public string Username { get; set; }

    // User password
    public string Password { get; set; }

    // User name details
    public Name Name { get; set; }

    // User phone number
    public string Phone { get; set; }
}

// Represents a Name in the system
public class Name
{
    // User first name
    public string FirstName { get; set; }

    // User last name
    public string LastName { get; set; }
}
