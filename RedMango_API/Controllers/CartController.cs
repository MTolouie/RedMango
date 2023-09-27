using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedMango_API.Utilities;
using RedMango_Business.Repository.IRepository;
using System.Net;

namespace RedMango_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpPost("[action]/{menuItemId:int}/{userId}/{quantity:int}")]
    public async Task<IActionResult> AddToShoppingCart(int menuItemId, string userId, int quantity)
    {
        var IsAdded = await _cartRepository.AddToCart(userId, menuItemId, quantity);
        var response = ApiResponseConfiguration.ConfigureResponse(true, HttpStatusCode.Created, null, null);
        if (IsAdded)
        {
            return Ok(response);
        }
        else
        {
            response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadGateway, "Something Went Wrong", null);
            return BadRequest(response);
        }
    }
}
