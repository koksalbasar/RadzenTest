using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using RadzenTest.Data;

namespace RadzenTest
{
    public partial class SqlExpressService
    {
        SqlExpressContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly SqlExpressContext context;
        private readonly NavigationManager navigationManager;

        public SqlExpressService(SqlExpressContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportOrdersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sqlexpress/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sqlexpress/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrdersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sqlexpress/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sqlexpress/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrdersRead(ref IQueryable<Models.SqlExpress.Order> items);

        public async Task<IQueryable<Models.SqlExpress.Order>> GetOrders(Query query = null)
        {
            var items = Context.Orders.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOrdersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderCreated(Models.SqlExpress.Order item);
        partial void OnAfterOrderCreated(Models.SqlExpress.Order item);

        public async Task<Models.SqlExpress.Order> CreateOrder(Models.SqlExpress.Order order)
        {
            OnOrderCreated(order);

            Context.Orders.Add(order);
            Context.SaveChanges();

            OnAfterOrderCreated(order);

            return order;
        }
        public async Task ExportOrderDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sqlexpress/orderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sqlexpress/orderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrderDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sqlexpress/orderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sqlexpress/orderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrderDetailsRead(ref IQueryable<Models.SqlExpress.OrderDetail> items);

        public async Task<IQueryable<Models.SqlExpress.OrderDetail>> GetOrderDetails(Query query = null)
        {
            var items = Context.OrderDetails.AsQueryable();

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOrderDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderDetailCreated(Models.SqlExpress.OrderDetail item);
        partial void OnAfterOrderDetailCreated(Models.SqlExpress.OrderDetail item);

        public async Task<Models.SqlExpress.OrderDetail> CreateOrderDetail(Models.SqlExpress.OrderDetail orderDetail)
        {
            OnOrderDetailCreated(orderDetail);

            Context.OrderDetails.Add(orderDetail);
            Context.SaveChanges();

            OnAfterOrderDetailCreated(orderDetail);

            return orderDetail;
        }
        public async Task ExportProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sqlexpress/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sqlexpress/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sqlexpress/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sqlexpress/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductsRead(ref IQueryable<Models.SqlExpress.Product> items);

        public async Task<IQueryable<Models.SqlExpress.Product>> GetProducts(Query query = null)
        {
            var items = Context.Products.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductCreated(Models.SqlExpress.Product item);
        partial void OnAfterProductCreated(Models.SqlExpress.Product item);

        public async Task<Models.SqlExpress.Product> CreateProduct(Models.SqlExpress.Product product)
        {
            OnProductCreated(product);

            Context.Products.Add(product);
            Context.SaveChanges();

            OnAfterProductCreated(product);

            return product;
        }

        partial void OnOrderDeleted(Models.SqlExpress.Order item);
        partial void OnAfterOrderDeleted(Models.SqlExpress.Order item);

        public async Task<Models.SqlExpress.Order> DeleteOrder(int? id)
        {
            var itemToDelete = Context.Orders
                              .Where(i => i.Id == id)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDeleted(itemToDelete);

            Context.Orders.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOrderDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOrderGet(Models.SqlExpress.Order item);

        public async Task<Models.SqlExpress.Order> GetOrderById(int? id)
        {
            var items = Context.Orders
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnOrderGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.SqlExpress.Order> CancelOrderChanges(Models.SqlExpress.Order item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderUpdated(Models.SqlExpress.Order item);
        partial void OnAfterOrderUpdated(Models.SqlExpress.Order item);

        public async Task<Models.SqlExpress.Order> UpdateOrder(int? id, Models.SqlExpress.Order order)
        {
            OnOrderUpdated(order);

            var itemToUpdate = Context.Orders
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(order);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();

            OnAfterOrderUpdated(order);

            return order;
        }

        partial void OnOrderDetailDeleted(Models.SqlExpress.OrderDetail item);
        partial void OnAfterOrderDetailDeleted(Models.SqlExpress.OrderDetail item);

        public async Task<Models.SqlExpress.OrderDetail> DeleteOrderDetail(int? id)
        {
            var itemToDelete = Context.OrderDetails
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDetailDeleted(itemToDelete);

            Context.OrderDetails.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOrderDetailDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOrderDetailGet(Models.SqlExpress.OrderDetail item);

        public async Task<Models.SqlExpress.OrderDetail> GetOrderDetailById(int? id)
        {
            var items = Context.OrderDetails
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

            var itemToReturn = items.FirstOrDefault();

            OnOrderDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.SqlExpress.OrderDetail> CancelOrderDetailChanges(Models.SqlExpress.OrderDetail item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderDetailUpdated(Models.SqlExpress.OrderDetail item);
        partial void OnAfterOrderDetailUpdated(Models.SqlExpress.OrderDetail item);

        public async Task<Models.SqlExpress.OrderDetail> UpdateOrderDetail(int? id, Models.SqlExpress.OrderDetail orderDetail)
        {
            OnOrderDetailUpdated(orderDetail);

            var itemToUpdate = Context.OrderDetails
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(orderDetail);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();

            OnAfterOrderDetailUpdated(orderDetail);

            return orderDetail;
        }

        partial void OnProductDeleted(Models.SqlExpress.Product item);
        partial void OnAfterProductDeleted(Models.SqlExpress.Product item);

        public async Task<Models.SqlExpress.Product> DeleteProduct(int? id)
        {
            var itemToDelete = Context.Products
                              .Where(i => i.Id == id)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductDeleted(itemToDelete);

            Context.Products.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductGet(Models.SqlExpress.Product item);

        public async Task<Models.SqlExpress.Product> GetProductById(int? id)
        {
            var items = Context.Products
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnProductGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.SqlExpress.Product> CancelProductChanges(Models.SqlExpress.Product item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnProductUpdated(Models.SqlExpress.Product item);
        partial void OnAfterProductUpdated(Models.SqlExpress.Product item);

        public async Task<Models.SqlExpress.Product> UpdateProduct(int? id, Models.SqlExpress.Product product)
        {
            OnProductUpdated(product);

            var itemToUpdate = Context.Products
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(product);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();

            OnAfterProductUpdated(product);

            return product;
        }
    }
}
