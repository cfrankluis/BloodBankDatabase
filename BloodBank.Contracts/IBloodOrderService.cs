using BloodBank.Models.BloodOrder;
using System.Collections.Generic;

namespace BloodBank.Contracts
{
    public interface IBloodOrderService
    {
        bool CreateOrder(BloodOrderCreate model);
        bool DeleteOrder(int id);
        bool EditOrder(BloodOrderEdit model);
        IEnumerable<BloodOrderListItem> GetAllOrders();
        BloodOrderDetail GetOrderByID(int id);
        IEnumerable<BloodOrderListItem> GetOrdersByPatient(int id);
    }
}