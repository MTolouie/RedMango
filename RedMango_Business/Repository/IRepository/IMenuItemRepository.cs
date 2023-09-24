using RedMango_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMango_Business.Repository.IRepository;

public interface IMenuItemRepository
{
    public Task<List<MenuItem>> GetAllMenuItems();
    public Task<MenuItem> GetMenuItem(int id);

}
