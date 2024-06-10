using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;

namespace WebApplication1.Services;

public interface IClientService
{
    public Task<ClientSubscriptionDto> GetClientSubscription(int idClient);
    public Task<bool> ClientExists(int idClient);
}

public class ClientService : IClientService
{
    private readonly AppDbContext _context;
    public ClientService(AppDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<ClientSubscriptionDto> GetClientSubscription(int idClient)
    {
        var subscriptionsPayments = await _context.Sales.Where(s => s.IdClient == idClient)
            .Join(
                _context.Subscriptions,
                s => s.IdSubscription,
                sb => sb.IdSubscription,
                (s, sb) =>
                    new
                    {
                        IdSubscription = sb.IdSubscription,
                        Name = sb.Name,
                        Price = sb.Price
                    }
            ).Join(
                _context.Payments,
                sp => sp.IdSubscription,
                p => p.IdSubscription,
                (sp, p) => new
                {
                    IdSubscription = sp.IdSubscription,
                    Name = sp.Name,
                    Price = sp.Price
                }
            ).GroupBy(spp => new { spp.IdSubscription, spp.Name })
            .Select(spp => new SubscriptionDto()
            {
                IdSubscription = spp.Key.IdSubscription,
                Name = spp.Key.Name,
                TotalPaidAmount = spp.Sum(spp => spp.Price)
            }).ToListAsync();

        var clientData = await _context.Clients.Where(c => c.IdClient == idClient)
            .Select(c => new ClientSubscriptionDto()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Subscriptions = subscriptionsPayments
            }).FirstAsync();


        return clientData;
    }

    public async Task<bool> ClientExists(int idClient)
    {
        var data = await _context.Clients.CountAsync(c => c.IdClient == idClient);

        return data > 0;
    }
}