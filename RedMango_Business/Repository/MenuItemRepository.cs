using Microsoft.EntityFrameworkCore;
using RedMango_Business.Repository.IRepository;
using RedMango_DataLayer.Context;
using RedMango_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMango_Business.Repository;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ApplicationDbContext _db;

    public MenuItemRepository(ApplicationDbContext db)
    {
        _db = db;
        
    }
    public async Task<List<MenuItem>> GetAllMenuItems()
    {
        var menuItems = await _db.MenuItems.ToListAsync();
        return menuItems;
    }

    public async Task<MenuItem> GetMenuItem(int id)
    {
        var menuItem = await _db.MenuItems.FindAsync(id);

        if (menuItem == null)
            return null;

        return menuItem;
    }
}
