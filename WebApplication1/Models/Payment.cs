namespace WebApplication1.Models;

public class Payment
{
    public int IdPayment { get; set; }
    public DateOnly Date { get; set; }
    public int IdClient { get; set; }
    public int IdSubscription { get; set; }
    
    public virtual Client Client { get; set; }
    public virtual Subscription Subscription { get; set; }
}