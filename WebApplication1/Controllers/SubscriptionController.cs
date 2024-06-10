using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/subscription")]
public class SubscriptionController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(IClientService clientService, ISubscriptionService subscriptionService)
    {
        _clientService = clientService;
        _subscriptionService = subscriptionService;
    }

    [HttpPut]
    public async Task<IActionResult> AddSubscriptionPaymentInf(int idClient, int idSubscription, double payment)
    {
        if (!await _clientService.ClientExists(idClient))
            return NotFound("Client not found");

        if (!await _subscriptionService.SubscriptionExists(idSubscription))
            return NotFound("Subscription not exists");

        if (!await _subscriptionService.SubscriptionIsActive(idSubscription))
            return Conflict("Subscription is not active");

        var discount = await _subscriptionService.GetDiscount(idSubscription);

        if (!await _subscriptionService.SubscriptionPaymentAmountCorrect(idSubscription, payment, discount))
            return Conflict("Payment is not correct");
            
        return NoContent();
    }
    
}