using Eshop.Domain;
using Eshop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Repository.Interface
{
    public interface IShoppingCartRepository
    {
        public void create(ShoppingCart shoppingCart);

    }
}
