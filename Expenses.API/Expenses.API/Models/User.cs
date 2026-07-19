using Expenses.API.Models.Base;

namespace Expenses.API.Models
{
    public class User:BaseEntity
    {

        public String? Email { get; set; }
        public String? Password { get; set; }

    }
}
