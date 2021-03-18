using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadzenTest.Data;

namespace RadzenTest
{
    public partial class ExportSqlExpressController : ExportController
    {
        private readonly SqlExpressContext context;

        public ExportSqlExpressController(SqlExpressContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/SqlExpress/orders/csv")]
        [HttpGet("/export/SqlExpress/orders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Orders, Request.Query), fileName);
        }

        [HttpGet("/export/SqlExpress/orders/excel")]
        [HttpGet("/export/SqlExpress/orders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Orders, Request.Query), fileName);
        }
        [HttpGet("/export/SqlExpress/orderdetails/csv")]
        [HttpGet("/export/SqlExpress/orderdetails/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrderDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.OrderDetails, Request.Query), fileName);
        }

        [HttpGet("/export/SqlExpress/orderdetails/excel")]
        [HttpGet("/export/SqlExpress/orderdetails/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrderDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.OrderDetails, Request.Query), fileName);
        }
        [HttpGet("/export/SqlExpress/products/csv")]
        [HttpGet("/export/SqlExpress/products/csv(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Products, Request.Query), fileName);
        }

        [HttpGet("/export/SqlExpress/products/excel")]
        [HttpGet("/export/SqlExpress/products/excel(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Products, Request.Query), fileName);
        }
    }
}
