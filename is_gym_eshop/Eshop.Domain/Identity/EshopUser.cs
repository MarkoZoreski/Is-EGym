using Eshop.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Domain.Identity
{
    public class EshopUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public virtual ShoppingCart UserCart { get; set; }
    }
}
