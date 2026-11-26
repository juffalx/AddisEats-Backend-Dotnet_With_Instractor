using AddisEats.Api.Models;
using AddisEats.Api.Services.MenuItemServices;
using Microsoft.AspNetCore.Mvc;

namespace AddisEats.Api.Controllers;

[ApiController]  
[Route("api/v1/[Controller]")]

public class MenuItemController : ControllerBase{
    private readonly IMenuItemService _menuItemService;
    // variable 

    public MenuItemController(IMenuItemService menuItemSerice)
    {
        _menuItemService = menuItemSerice;
    }
[HttpGet]
public IActionResult GetAll([FromQuery] string? category, [FromQuery] int? upPrice)
    {
        var result = _menuItemService.GetAll(category, upPrice);
        return Ok(result);
    }

[HttpGet("{id:int:min(1)}")]
public IActionResult GetById(int id)
    {
        var result = _menuItemService.GetById(id);
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}



// builder.Services.AddScoped<//   IMenuInterfaceService, MenuItemService >();

// IMenuInterfaceService request from Controller create and give back the asociate object 
// new MenuItemController(new MenuService());

// menuItemSerice = 