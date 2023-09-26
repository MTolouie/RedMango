
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedMango_Business.Repository.IRepository;
using RedMango_DataLayer.Context;
using RedMango_DataLayer.Models;

namespace RedMango_Business.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<ApplicationUser> GetUserByEmail(string email)
    {
        try
        {
            var user = await _db.ApplicationUsers.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                return null;
            }

            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<ApplicationUser> GetUserById(string id)
    {
        try
        {
            var user = await _db.ApplicationUsers.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }

            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<ApplicationUser> GetUserByUsername(string username)
    {
        try
        {
            var user = await _db.ApplicationUsers.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
            if (user == null)
            {
                return null;
            }

            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
