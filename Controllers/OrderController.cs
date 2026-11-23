using AddisEats.Api.Models;
using AddisEats.Api.DTOs;
using AddisEats.Api.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]

public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = _orderService.GetById(id);

        if(order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]

    public ActionResult<Order> CreateOrder([FromBody] CreatOrderDTO createDTO)
    {
        var result = _orderService.CreateOrder(createDTO);

        if (!result.IsSuccess)
        {
            return Problem(
                title: result.Error?.Message,
                statusCode:result.Error?.Status,
                detail:result.Error?.Code
            );
        }

        var order = result.Value;

        return CreatedAtAction(
            nameof(GetById), 
            new {id = order.Id }, 
            order);
    }


    
}