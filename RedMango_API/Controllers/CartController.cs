using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedMango_API.Utilities;
using RedMango_Business.Repository.IRepository;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using RedMango_DataLayer.Models;

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
    public async Task<IActionResult> AddToCart(int menuItemId, string userId, int quantity)
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

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserCart(string userId)
    {
        var cart = await _cartRepository.GetUserCart(userId);


        var response = ApiResponseConfiguration.ConfigureResponse(true, HttpStatusCode.OK, null, cart);


        if (cart is not null)
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
