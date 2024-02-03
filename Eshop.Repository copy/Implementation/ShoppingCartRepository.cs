using Eshop.Domain;
using Eshop.Domain.DomainModels;
using Eshop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Repository.Implementation
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ShoppingCart> entities;
        string errorMessage = string.Empty;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ShoppingCart>();
        }

        public void create(ShoppingCart shoppingCart)
        {
            entities.Add(shoppingCart);
            context.SaveChanges();
        }
    }
}
