using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedMango_Models.DTOs;
using Stripe.Checkout;

namespace RedMango_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StripeController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public StripeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Create(/*[FromBody]*/StripePaymentDTO payment)
    {
        try
        {
            var domain = _configuration.GetValue<string>("RedMango_Client_Url");

            var options = new SessionCreateOptions()
            {
                PaymentMethodTypes = new List<string>()
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>()
                {
                    new SessionLineItemOptions()
                    {

                       PriceData = new SessionLineItemPriceDataOptions()
                       {
                           UnitAmount = payment.Amount,
                           Currency = "usd",
                             ProductData = new SessionLineItemPriceDataProductDataOptions()
                                {
                                   Name = payment.ProductName
                                }
                       },

                     Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = domain + "/Stripe/successPayment?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = domain + payment.ReturnUrl
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return Ok(new SuccessPaymentDTO()
            {
                Data = session.Id,
            });
        }
        catch (Exception e)
        {
            return BadRequest(new StripeErrorDTO()
            {
                ErrorMessage = e.Message,
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
    }
}
