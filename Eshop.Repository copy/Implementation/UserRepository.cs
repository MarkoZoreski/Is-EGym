using Eshop.Domain.Identity;
using Eshop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eshop.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<EshopUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<EshopUser>();
        }
        public IEnumerable<EshopUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public EshopUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.ProductInShoppingCarts")
               .Include("UserCart.ProductInShoppingCarts.CurrnetProduct")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(EshopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(EshopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(EshopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
