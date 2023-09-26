
using RedMango_DataLayer.Models;

namespace RedMango_Business.Repository.IRepository;

public interface IUserRepository
{
    public Task<ApplicationUser> GetUserById(string id);
    public Task<ApplicationUser> GetUserByEmail(string email);
    public Task<ApplicationUser> GetUserByUsername(string username);
}
