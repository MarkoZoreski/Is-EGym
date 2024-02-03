using Eshop.Domain.DomainModels;
using Eshop.Domain.DTO;
using Eshop.Domain.Relations;
using Eshop.Repository.Interface;
using Eshop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eshop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IShoppingCartRepository shoppingCart;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<EmailMessage> mailRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
            _mailRepository = mailRepository;
        }


        public bool deleteProductFromSoppingCart(string userId, Guid productId)
        {
            if(!string.IsNullOrEmpty(userId) && productId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.ProductId.Equals(productId)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser.UserCart;

                if(userCart == null)
                {
                    userCart = new ShoppingCart();
                    userCart.CartOwner = loggedInUser;
                    _shoppingCartRepository.Insert(userCart);
                }
                var allProducts = userCart.ProductInShoppingCarts.ToList();

                var allProductPrices = allProducts.Select(z => new
                {
                    ProductPrice = z.CurrnetProduct.ProductPrice,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allProductPrices)
                {
                    totalPrice += item.Quantity * item.ProductPrice;
                }

                var reuslt = new ShoppingCartDto
                {
                    Products = allProducts,
                    TotalPrice = totalPrice
                };

                return reuslt;
            }
            return new ShoppingCartDto();
        }

        public String order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;

                EmailMessage mail = new EmailMessage();
                //mail.MailTo = loggedInUser.Email;
                //mail.Subject = "Sucessfuly created order!";
                //mail.Status = false;


                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userCart.ProductInShoppingCarts.Select(z => new ProductInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = z.CurrnetProduct.Id,
                    Product = z.CurrnetProduct,
                    OrderId = order.Id,
                    Order = order, 
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Product.ProductPrice;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Product.ProductName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Product.ProductPrice);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                //mail.Content = sb.ToString();


                productInOrders.AddRange(result);

                foreach (var item in productInOrders)
                {
                    this._productInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.ProductInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                //this._mailRepository.Insert(mail);

                return sb.ToString();
            }

            return "Unsuccesfull order";
        }
    }
}
