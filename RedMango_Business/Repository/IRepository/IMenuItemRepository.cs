using RedMango_DataLayer.Models;
using RedMango_Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMango_Business.Repository.IRepository;

public interface IMenuItemRepository
{
    public Task<List<MenuItemDTO>> GetAllMenuItems();
    public Task<MenuItemDTO> GetMenuItem(int id);
    public Task<bool> CreateMenuItem(MenuItemDTO MenuItemDTO);

}
