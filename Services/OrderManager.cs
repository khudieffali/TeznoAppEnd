using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderManager
    {
        private readonly ApplicationDbContext _context;

        public OrderManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Order orders)
        {
            _context.Orders.Add(orders);
            _context.SaveChanges();
        }
    }
}
