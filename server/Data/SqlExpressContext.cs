using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using RadzenTest.Models.SqlExpress;

namespace RadzenTest.Data
{
  public partial class SqlExpressContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public SqlExpressContext(DbContextOptions<SqlExpressContext> options):base(options)
    {
    }

    public SqlExpressContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RadzenTest.Models.SqlExpress.OrderDetail>()
              .HasOne(i => i.Order)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.OrderId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<RadzenTest.Models.SqlExpress.OrderDetail>()
              .HasOne(i => i.Product)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.ProductId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<RadzenTest.Models.SqlExpress.Order>()
              .Property(p => p.OrderDate)
              .HasColumnType("date");
        this.OnModelBuilding(builder);
    }


    public DbSet<RadzenTest.Models.SqlExpress.Order> Orders
    {
      get;
      set;
    }

    public DbSet<RadzenTest.Models.SqlExpress.OrderDetail> OrderDetails
    {
      get;
      set;
    }

    public DbSet<RadzenTest.Models.SqlExpress.Product> Products
    {
      get;
      set;
    }
  }
}
