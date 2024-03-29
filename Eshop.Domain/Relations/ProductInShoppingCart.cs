﻿using Eshop.Domain.DomainModels;
using System;

namespace Eshop.Domain.Relations
{
    public class ProductInShoppingCart : BaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product CurrnetProduct { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

        public int Quantity { get; set; }
    }
}
