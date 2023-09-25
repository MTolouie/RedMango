using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RedMango_API.Models;
using RedMango_API.Utilities;
using RedMango_Business.Repository.IRepository;
using RedMango_Models.DTOs;
using System.Net;

namespace RedMango_API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemController(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems()
    {
        try
        {
            var menuItems = await _menuItemRepository.GetAllMenuItems();

            var statusCode = HttpStatusCode.OK;
            if (menuItems is null)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            var response = ApiResponseConfiguration.ConfigureResponse(true, statusCode, null, menuItems);

            return Ok(response);
        }
        catch (Exception e)
        {
            var response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.InternalServerError, e.Message, null);
            return Ok(response);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMenuItem(int id)
    {

        try
        {
            var menuItem = await _menuItemRepository.GetMenuItem(id);

            var statusCode = HttpStatusCode.OK;
            if (menuItem is null)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            var response = ApiResponseConfiguration.ConfigureResponse(true, statusCode, null, menuItem);
            return Ok(response);

        }
        catch (Exception e)
        {

            var response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.InternalServerError, e.Message, null);
            return Ok(response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDTO createMenuItemDTO)
    {

        try
        {
            if (ModelState.IsValid)
            {
                if (createMenuItemDTO.Image is null && createMenuItemDTO.Image.Length == 0)
                {
                    return BadRequest();
                }

                var result = await _menuItemRepository.CreateMenuItem(createMenuItemDTO);
                var response = ApiResponseConfiguration.ConfigureResponse(true, HttpStatusCode.Created, null, null);
                if (result is true)
                {
                    return Ok(response);
                }

                response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadRequest, "something went wrong", null);
                return Ok(response);

            }
            else
            {
                var response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadRequest, "Form Was Not Valid", null);
                return Ok(response);
            }
        }
        catch (Exception e)
        {
            var response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.InternalServerError, e.Message, null);
            return Ok(response);
        }

    }
}
