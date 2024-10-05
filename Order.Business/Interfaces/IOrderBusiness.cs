using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Business.Interfaces
{
    public interface IOrderBusiness
    {
        Task<IEnumerable<Common.Entities.Order>> GetOrders();

        Task<Common.Entities.Order?> GetOrdersById(Guid id);

        Task UpdateOrder(Guid id, Common.Entities.Order order);

        Task<Common.Entities.Order?> CreateOrder(Common.Entities.Order order);

        Task<bool> DeleteOrder(Guid id);
    }
}
