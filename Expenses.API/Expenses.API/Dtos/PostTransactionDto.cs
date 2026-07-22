using System.ComponentModel.DataAnnotations;

namespace Expenses.API.Dtos
{
    public class PostTransactionDto
    {
        [Required(ErrorMessage = "Type is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Type must be between 1 and 50 characters")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Category must be between 1 and 50 characters")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime CreatedAt { get; set; }
    }
}