using AddisEats.Api.Models;

namespace AddisEats.Api.Services.MenuItemServices;

public interface IMenuItemService
{
    List<MenuItem> GetAll(string? category,  int? upPrice);

    MenuItem? GetById(int id);

}