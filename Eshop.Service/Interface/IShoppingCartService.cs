using Eshop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromSoppingCart(string userId, Guid productId);
        String order(string userId);
    }
}
