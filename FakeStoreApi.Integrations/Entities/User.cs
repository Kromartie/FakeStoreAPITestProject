namespace FakeStoreApi.Integrations.Entities;
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // Only for login; not usually returned
    public Name Name { get; set; }
    public string Phone { get; set; }
}

public class Name
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
}
