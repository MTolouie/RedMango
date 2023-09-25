using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedMango_Business.Repository.IRepository;
using RedMango_DataLayer.Context;
using RedMango_DataLayer.Models;
using RedMango_Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMango_Business.Repository;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public MenuItemRepository(ApplicationDbContext db,IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> CreateMenuItem(MenuItemDTO MenuItemDTO)
    {
        try
        {
            var menuItem = _mapper.Map<MenuItemDTO, MenuItem>(MenuItemDTO);

            _db.MenuItems.Add(menuItem);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<List<MenuItemDTO>> GetAllMenuItems()
    {
        try
        {
            var menuItems = await _db.MenuItems.ToListAsync();

            var menuItemsDTO = _mapper.Map<List<MenuItem>, List<MenuItemDTO>>(menuItems);

            return menuItemsDTO;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<MenuItemDTO> GetMenuItem(int id)
    {
        var menuItem = await _db.MenuItems.FindAsync(id);

        if (menuItem == null)
            return null;

        var menuItemDTO = _mapper.Map<MenuItem, MenuItemDTO>(menuItem);

        return menuItemDTO;
    }
}
