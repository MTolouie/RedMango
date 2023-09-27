
using RedMango_DataLayer.Models;

namespace RedMango_Business.Repository.IRepository;

public interface ICartRepository
{
    public Task<bool> AddToCart(string userId, int menuItemId, int quantity);
    public Task<bool> DeleteFromCart(int cartId, int CartDetailId);
    public Task<Cart> GetUserCart(string userId);
}
