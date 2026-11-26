using System.ComponentModel.DataAnnotations;
using AddisEats.Api.Validations;
namespace AddisEats.Api.DTOs;

public record CreatOrderDTO(
    /// <summary>
    /// The Id of restaurant to order from
    /// </summary>
    /// <example>14</example>
    /// 
    [Range(1, int.MaxValue, ErrorMessage = "Restaurant Id Must Be Possitive.")]
    int Id,
    int RestaurantId,
    int MenuItemId,

    /// <summary>
    ///     The Name of Customer
    /// </summary>
    /// <example>Hanna</example>

    [Required(ErrorMessage = "Customer Name is Required")]
    string CustomerName,


    /// <summary>
    ///     The Delivery Area in Addis Ababa
    /// </summary>
    /// <example>Bole</example>

    [StringLength(60, MinimumLength = 1, ErrorMessage = "Deliver Address is Required")]
    string DeliveryAddress,


    /// <summary>
    ///     The Ethiopian customer phone number
    /// </summary>
    /// <example>+251 .. .. /09 11 .. ..</example>

    [Required]
    [StringLength(10, MinimumLength = 10)]
    [EthiopianMobile]
    string CustomerPhone,


    /// <summary>
    ///     Quantity of Orders
    /// </summary>
    /// <example>6</example>

    [Range(1, 100, ErrorMessage = "Quantity must be greater than zero.")]
    int Quantity
    // [ETBAmount]

    );