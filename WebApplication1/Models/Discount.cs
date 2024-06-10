namespace WebApplication1.Models;

public class Discount
{
    public int IdDiscount { get; set; }
    public int Value { get; set; }
    public int IdSubscription { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    
    public virtual Subscription Subscription { get; set; }
}