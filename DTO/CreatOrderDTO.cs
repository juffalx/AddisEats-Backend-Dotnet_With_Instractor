using System.ComponentModel.DataAnnotations;
using AddisEats.Api.Validations;
namespace AddisEats.Api.DTOs;

public record CreatOrderDTO(
    [Range(1, int.MaxValue, ErrorMessage = "Restaurant Id Must Be Possitive.")]
    int Id,
    int RestaurantId ,
    int MenuItemId,
    [Required(ErrorMessage = "Customer Name is Required")]
    string CustomerName,
    [StringLength(60, MinimumLength = 1, ErrorMessage = "Deliver Address is Required")]
    string DeliveryAddress,
    [Range(1, 50, ErrorMessage = "Customer Phone Should 1 - 50")]
    [EthiopianMobile]
    string CustomerPhone,
    [ETBAmount]
    int Quantity,
    decimal TotalAmount
);