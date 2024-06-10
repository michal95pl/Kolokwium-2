namespace WebApplication1.DTO;

public class ClientSubscriptionDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public List<SubscriptionDto> Subscriptions { get; set; }
}