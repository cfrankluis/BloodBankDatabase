using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;
using BloodBank.Models.BloodOrder;

namespace BloodBank.Service
{
    public class BloodOrderService
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();

        public bool CreateOrder(BloodOrderCreate model)
        {
            var entity = new BloodOrder
            {
                PatientID = model.PatientID,
                BloodType = model.BloodType,
                Amount = model.Amount,
                OrderDate = DateTime.Now
                
            };

            _ctx.Orders.Add(entity);
            return _ctx.SaveChanges() == 1;

        }

        public IEnumerable<BloodOrderListItem> GetAllOrders()
        {
            var query =
                _ctx
                .Orders
                .Select(
                    e => new BloodOrderListItem 
                    {
                        ID = e.ID,
                        BloodType = e.BloodType,
                        PatientName = e.Patient.FullName,
                        OrderDate = e.OrderDate
                    });

            return query.ToArray();
        }

        public IEnumerable<BloodOrderListItem> GetOrdersByPatient(int id)
        {
            var query =
                _ctx
                .Orders
                .Where(e => id == e.PatientID)
                .Select(
                    e => new BloodOrderListItem
                    {
                        ID = e.ID,
                        BloodType = e.BloodType,
                        PatientName = e.Patient.FullName,
                        OrderDate = e.OrderDate
                    });

            return query.ToArray();
        }

        public BloodOrderDetail GetOrderByID(int id)
        {
            var entity = _ctx.Orders.Find(id);

            if (entity is null)
                return null;

            return
                new BloodOrderDetail
                {
                    ID = entity.ID,
                    BloodType = entity.BloodType,
                    PatientID = entity.PatientID,
                    PatientName = entity.Patient.FullName,
                    Amount = entity.Amount,
                    OrderDate = entity.OrderDate
                };
        }

        public bool DeleteOrder(int id)
        {
            var entity = _ctx.Orders.Find(id);

            if (entity is null)
                return false;

            _ctx.Orders.Remove(entity);
            return _ctx.SaveChanges() == 1;
        }

        public bool EditOrder(BloodOrderEdit model)
        {
            var entity = _ctx.Orders.Find(model.ID);

            if (entity is null)
                return false;

            entity.PatientID = model.PatientID;
            entity.BloodType = model.BloodType;
            entity.Amount = model.Amount;

            _ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return _ctx.SaveChanges() == 1;
        }
    }
}
