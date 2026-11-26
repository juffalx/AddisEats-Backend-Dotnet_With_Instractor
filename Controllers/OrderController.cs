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

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    ///     Places a new order for delivery
    /// </summary>
    /// <param name="request">
    ///     Contains the restaurant delivery area and order item
    /// </param>
    /// 
    /// <response code="201">
    ///     The order Succesfully Placed.
    /// </response>
    /// 
    /// <response code = "400">
    ///     The submitted request contains invalid data.
    /// </response>
    /// 
    /// <response code="404">
    ///     The requested restaurant was not found.
    /// </response>
    /// 
    /// <response code="409">
    ///     The order conflicts with the current restaurant state.
    /// </response>
    [HttpPost]
    [ProducesResponseType(
        typeof(Order),
        StatusCodes.Status201Created
    )]

    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        StatusCodes.Status404NotFound
    )]
    [ProducesResponseType(
        typeof(ProblemDetails),
        StatusCodes.Status404NotFound
    )]

    [ProducesResponseType(
        typeof(ProblemDetails),
        StatusCodes.Status409Conflict
    )]


    public ActionResult<Order> CreateOrder([FromBody] CreatOrderDTO createDTO)
    {
        var result = _orderService.CreateOrder(createDTO);

        if (!result.IsSuccess)
        {
            return Problem(
                title: result.Error?.Message,
                statusCode: result.Error?.Status,
                detail: result.Error?.Code
            );
        }

        var order = result.Value;

        return CreatedAtAction(
            nameof(GetById),
            new { id = order.Id },
            order);
    }



}