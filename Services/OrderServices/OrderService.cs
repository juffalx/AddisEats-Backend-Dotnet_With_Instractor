using AddisEats.Api.DTOs;
using AddisEats.Api.Models;
using AddisEats.Api.Common;
using AddisEats.Api.Service.RestaurantServices;
using AddisEats.Api.Services.MenuItemServices;

namespace AddisEats.Api.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IRestaurantServices _restaurantService;
    private readonly ILogger<OrderService> _logger;
    private readonly IMenuItemService _menuItemService;

    public OrderService(IMenuItemService menuItemServices, IRestaurantServices restaurantServices, ILogger<OrderService> logger)
    {
        _restaurantService = restaurantServices;
        _logger = logger;
        _menuItemService = menuItemServices;
    }
    private static readonly List<Order> _order = new List<Order>();

    private static int _nextId = 1;
    public Order GetById(int id)
    {
        var result = _order.FirstOrDefault(o => o.Id == id);
        return result;
    }



    public Result<Order> CreateOrder(CreatOrderDTO createDTO)
    {

        _logger.LogInformation(
        "Creating order for restaurant {RestaurantId} with menu item {MenuItemId} and customer {CustomerName}",
        createDTO.RestaurantId,
        createDTO.MenuItemId,
        createDTO.CustomerName
        );

        var restaurant = _restaurantService.GetById(createDTO.RestaurantId);
        if (restaurant is null)
        {
            return Result<Order>.Fail("Restaurant_Not_Found", " The selected restaurant Does not Exist ", 404);
        }
        if (!restaurant.IsActive)
        {
            return Result<Order>.Fail("Restaurant_Not_Active", "The Selected restaurant is not active", 409);
        }
        var o = new Order
        {
            Id = _nextId++,
            RestaurantId = createDTO.RestaurantId,
            MenuItemId = createDTO.MenuItemId,
            CustomerName = createDTO.CustomerName,
            DeliveryAddress = createDTO.DeliveryAddress,
            CustomerPhone = createDTO.CustomerPhone,
            Quantity = createDTO.Quantity,
            TotalAmount = (_menuItemService.GetById(createDTO.MenuItemId)?.Price ?? 0m)
                  * createDTO.Quantity
        };
        _logger.LogInformation(
            "Order Placed for restaurant {RestaurantId} in {DeliveryAddress}",
            createDTO.RestaurantId,
            createDTO.DeliveryAddress
            );
        _order.Add(o);
        return Result<Order>.Ok(o);


    }
}