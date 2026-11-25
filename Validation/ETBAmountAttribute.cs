using System.ComponentModel.DataAnnotations;

namespace AddisEats.Api.Validations;

public class ETBAmountAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value, 
        ValidationContext validationContext)
    {
        if(value is null)
        {
            return ValidationResult.Success;
        }

        if(value is not decimal amount || amount <= 0)
        {
            return new  ValidationResult(
                "Amoutnt must be greater thatn Zero.");
        }

        if(decimal.Round(amount, 2) != amount)
        {
            return new ValidationResult(
                "Amount cannot have more than two decimal places"
            );
        }
    return ValidationResult.Success;
    }
}