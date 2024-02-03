using Eshop.Domain;
using Eshop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);
    }
}
