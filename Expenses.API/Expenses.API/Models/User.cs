using Expenses.API.Models.Base;

namespace Expenses.API.Models
{
    public class User:BaseEntity
    {

        public required string Email { get; set; }
        public required string Password { get; set; }

        public List<Transaction>? Transactions { get; set; }

    }
}
