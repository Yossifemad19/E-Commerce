using E_Commerce.Core.Entities.OrdderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress,a=> {
                a.WithOwner();
                a.Property(a => a.State).IsRequired();
                a.Property(a=>a.StreetName).IsRequired();
                a.Property(a=>a.HouseNumber).IsRequired();
            });

            builder.Navigation(o => o.ShippingAddress).IsRequired();

            builder.Property(o => o.OrderStatus).HasConversion(os => os.ToString(), os => (OrderStatus)Enum.Parse(typeof(OrderStatus),os));

            builder.HasMany(o=>o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
