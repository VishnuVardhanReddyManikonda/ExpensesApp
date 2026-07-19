using Expenses.API.Models.Base;

namespace Expenses.API.Models
{
    public class Transaction:BaseEntity
    {

        

        public String? Type { get; set; }

        public double Amount { get; set; }

        public string? Category { get; set; }


    }
}
