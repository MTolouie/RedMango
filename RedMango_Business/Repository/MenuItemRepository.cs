using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedMango_Business.Repository.IRepository;
using RedMango_DataLayer.Context;
using RedMango_DataLayer.Models;
using RedMango_Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

    public async Task<bool> CreateMenuItem(MenuItemDTO menuItemDTO)
    {
        try
        {
            var menuItem = _mapper.Map<MenuItemDTO, MenuItem>(menuItemDTO);

            _db.MenuItems.Add(menuItem);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> DeleteMenuItemById(int id)
    {
        try
        {
            var menuItem = await _db.MenuItems.FindAsync(id);
            if (menuItem is null)
                return false;

            _db.MenuItems.Remove(menuItem);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
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

    public async Task<bool> UpdateMenuItem(int id, MenuItemDTO menuItemDTO)
    {
        try
        {
            if(menuItemDTO.Id != id)
            {
                return false;
            }

            var menuItem = await _db.MenuItems.FindAsync(id);
            var convertedMenuItem = _mapper.Map<MenuItemDTO, MenuItem>(menuItemDTO, menuItem);
            var UpdatedBook = _db.MenuItems.Update(convertedMenuItem);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


}
