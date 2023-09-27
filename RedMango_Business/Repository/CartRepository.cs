
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedMango_Business.Repository.IRepository;
using RedMango_DataLayer.Context;
using RedMango_DataLayer.Models;

namespace RedMango_Business.Repository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CartRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> AddToCart(string userId, int menuItemId, int quantity)
    {
        try
        {
            var cart = await _db.Cart
                .Where(c => c.UserId == userId && c.IsFinally == false)
                .SingleOrDefaultAsync();

            var menuItem = await _db.MenuItems.FindAsync(menuItemId);

            if (menuItem is null)
                return false;

            if (cart is null)
            {
                cart = new()
                {
                    CreateDate = DateTime.Now,
                    IsFinally = false,
                    CartSum = menuItem.Price * quantity,
                    UserId = userId
                };

                await _db.Cart.AddAsync(cart);
                await _db.SaveChangesAsync();

                CartDetail detials = new()
                {
                    MenuItemId = menuItemId,
                    CartId = cart.CartId,
                    Price = menuItem.Price,
                    Quantity = quantity
                };

                await _db.CartDetails.AddAsync(detials);
                await _db.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await _db.CartDetails.Where(c => c.CartId == cart.CartId && c.MenuItemId == menuItemId).SingleOrDefaultAsync();

                if (cartDetail is null)
                {
                    CartDetail detials = new()
                    {
                        MenuItemId = menuItemId,
                        CartId = cart.CartId,
                        Quantity = quantity,
                        Price = menuItem.Price,
                    };

                    await _db.CartDetails.AddAsync(detials);
                    await _db.SaveChangesAsync();

                    await UpdateCartPrice(cart.CartId);
                }
                else
                {
                    cartDetail.Quantity += quantity;
                    _db.CartDetails.Update(cartDetail);
                    await _db.SaveChangesAsync();

                    await UpdateCartPrice(cart.CartId);
                }
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }


    }
    private async Task UpdateCartPrice(int CartId)
    {
        var cart = await _db.Cart.FindAsync(CartId);
        cart.CartSum = _db.CartDetails.Where(d => d.CartId == CartId).Sum(s => s.Price * s.Quantity);
        _db.Cart.Update(cart);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> DeleteFromCart(int cartId, int CartDetailId)
    {
        try
        {
            var cartDetial = await _db.CartDetails.FindAsync(CartDetailId);

            if (cartDetial is null)
                return false;

            _db.CartDetails.Remove(cartDetial);
            await _db.SaveChangesAsync();

            var cartDetails = await _db.CartDetails.Where(d => d.CartId == cartId).ToListAsync();

            if (cartDetails.Count == 0)
            {
                var cart = await _db.Cart.FindAsync(cartId);
                if (cart is not null)
                {
                    _db.Cart.Remove(cart);
                    await _db.SaveChangesAsync();
                }

            }
            else
            {
                await UpdateCartPrice(cartId);
            }
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
