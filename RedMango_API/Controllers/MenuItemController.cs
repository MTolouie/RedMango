using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedMango_API.Models;
using RedMango_Business.Repository.IRepository;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private ApiResponse _apiResponse;

        public MenuItemController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
            _apiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllMenuItems();

                _apiResponse.StatusCode = HttpStatusCode.OK;
                if (menuItems is null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                }
                _apiResponse.Results = menuItems;

            }
            catch (Exception e)
            {
                _apiResponse.ErrorMessages.Add(e.Message);
                _apiResponse.IsSuccessful = false;
                _apiResponse.StatusCode = HttpStatusCode.BadGateway;
            }
            return Ok(_apiResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMenuItem(int id)
        {

            try
            {
                var menuItem = await _menuItemRepository.GetMenuItem(id);

                _apiResponse.StatusCode = HttpStatusCode.OK;
                if (menuItem is null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                }
                _apiResponse.Results = menuItem;

            }
            catch (Exception e)
            {
                _apiResponse.ErrorMessages.Add(e.Message);
                _apiResponse.IsSuccessful = false;
                _apiResponse.StatusCode = HttpStatusCode.BadGateway;
            }
            return Ok(_apiResponse);
        }
    }
}
