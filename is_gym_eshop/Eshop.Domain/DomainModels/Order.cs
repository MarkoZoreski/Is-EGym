using Eshop.Domain.Identity;
using Eshop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Eshop.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public EshopUser User { get; set; }

        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; } = new Collection<ProductInOrder>();
    }
}
