using Eshop.Domain.Relations;
using System.Collections.Generic;

namespace Eshop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<ProductInShoppingCart> Products { get; set; }

        public double TotalPrice { get; set; }
    }
}
