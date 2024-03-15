using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Shared.Data;

namespace BlazorWebApp.Shared.DataAccess
{
    public class OrderDataAccessLayer
    {
        OrdersDetailsContext db = new OrdersDetailsContext();

        //To Get all Orders details   
        public DbSet<Order> GetAllOrders()
        {
            try
            {
                return db.Orders;
            }
            catch
            {
                throw;
            }
        }

       // To Add new Order record
        public void AddOrder(Order Order)
        {
            try
            {
                db.Orders.Add(Order);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar Order    
        public void UpdateOrder(Order Order)
        {
            try
            {
                db.Entry(Order).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular Order    
        public Order GetOrderData(int id)
        {
            try
            {
                Order Order = db.Orders.Find(id);
                return Order;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular Order    
        public void DeleteOrder(int id)
        {
            try
            {
                Order ord = db.Orders.Find(id);
                db.Orders.Remove(ord);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
