using Eshop.Domain.Identity;
using Eshop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Eshop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string CartOwnerId { get; set; }
        public virtual EshopUser CartOwner { get; set; }

        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; } = new Collection<ProductInShoppingCart>();

    }
}
