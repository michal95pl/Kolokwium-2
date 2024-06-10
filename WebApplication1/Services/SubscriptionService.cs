using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ISubscriptionService
{
    public Task<bool> SubscriptionExists(int idSubscription);
    public Task<bool> SubscriptionIsActive(int idSubscription);
    public Task<bool> SubscriptionPaymentAmountCorrect(int idSubscription, double price, int discount);
    public Task<int> GetDiscount(int idSubscription);
    public Task<bool> DiscountExists(int idSubscription);
}

public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _context;
    
    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SubscriptionExists(int idSubscription)
    {
        var data = await _context.Subscriptions.CountAsync(s => s.IdSubscription == idSubscription);

        return data > 0;
    }

    public async Task<bool> SubscriptionIsActive(int idSubscription)
    {
        var data = await _context.Subscriptions.Where(s => s.IdSubscription == idSubscription).FirstAsync();

        return data.EndTime < DateOnly.FromDateTime(DateTime.Now);
    }

    public async Task<bool> SubscriptionPaymentAmountCorrect(int idSubscription, double price, int discount)
    {
        var data = await _context.Subscriptions.Where(s => s.IdSubscription == idSubscription).FirstAsync();

        return data.Price == (price - discount);
    }

    public async Task<int> GetDiscount(int idSubscription)
    {
        if (!await DiscountExists(idSubscription))
            return 0;
            
        var data = await _context.Subscriptions.Where(s => s.IdSubscription == idSubscription)
            .Join(
                _context.Discounts,
                s => s.IdSubscription,
                d => d.IdSubscription,
                (s, d) => d.Value
            ).FirstAsync();

        return data;
    }

    public async Task<bool> DiscountExists(int idSubscription)
    {
        var data = await _context.Discounts.CountAsync(d => d.IdSubscription == idSubscription);
        
        return data > 0;
    }

    public async Task<bool> IsSubscriptionPaid(int idSubscription)
    {
        var data = _context.Subscriptions.Where(s => s.IdSubscription == idSubscription)
            .Join(
                _context.Payments,
                s => s.IdSubscription,
                p => p.IdSubscription,
                (s, p) => new
                {
                    Paymentdate = p.Date,
                    EndTime = s.EndTime
                }
            );

        return false;
    }
}