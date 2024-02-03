using Eshop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EshopUser> GetAll();
        EshopUser Get(string id);
        void Insert(EshopUser entity);
        void Update(EshopUser entity);
        void Delete(EshopUser entity);
    }
}
