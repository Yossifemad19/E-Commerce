using E_Commerce.Core.Entities.OrdderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class OrderWithItemsSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsSpecification(string email):base(o=>o.UserEmail==email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o=>o.OrderItems);
            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderWithItemsSpecification(int id,string userEmail) : base(o=>o.Id==id&&o.UserEmail==userEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o=>o.OrderItems);
        }

    }
}
