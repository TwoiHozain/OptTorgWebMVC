using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class SalesController : Controller
    {
        private string _ViewForm = "TSales";
        private string _EditForm = "ESales";

        private string _CreateFormPickCustomer = "TPickCustomers";
        private string _CreateFormPickProduct = "TPickProducts";
        private string _CreateFormPickSalesData = "TPickSalesData";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TSales()
        {
            ViewBag.Role = CurrentUser.role;

            if (CurrentUser.isCustomer)
            {
                return View(_ViewForm, Sales.GetAllCustomerSales(CurrentUser.customerId));
            }

            return View(_ViewForm, Sales.GetAllSales());
        }

        //Create
        [HttpGet]
        public IActionResult OpenPickCustomer()
        {
            if (CurrentUser.isCustomer) 
            {
                Sales b = new Sales();
                b.CustomerId = CurrentUser.customerId;
                return OpenPickProduct(b);
            }
            return View(_CreateFormPickCustomer);
        }

        [HttpPost]
        public IActionResult OpenPickProduct(Sales b)
        {
            return View(_CreateFormPickProduct, b);
        }

        [HttpPost]
        public IActionResult OpenPickCount(Sales b)
        {
            b = Sales.GetNavData(b);
            ViewData["ProdCount"] = Products.GetProductBalance(b.ProductId);

            return View(_CreateFormPickSalesData, b);
        }

        [HttpPost]
        public IActionResult CreateSales(Sales b)
        {
            if(b.Count > Products.GetProductBalance(b.ProductId))
            {
                b.Count = 0;
                ViewBag.Message = $"Недостаточно товара на складе";
                ViewBag.Type = 1;

                return OpenPickCount(b);
            }
            
            Sales.CreatrSales(b);
            return View(_ViewForm, Sales.GetAllSales());
        }

        //Edit
        [HttpPost]
        public IActionResult OpenESales(int id)
        {
            return View(_EditForm, Sales.GetSalesById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesSales(Sales p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;
            p.Active = true;

            Sales.UpdateSales(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, Sales.GetAllSales());
        }

        ////Delete
        [HttpPost]

        public IActionResult DSales(int id)
        {
            Sales.DeleteSales(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, Sales.GetAllSales());
        }

        public IActionResult DCascade(int id)
        {
            Sales.DCascade(id);
            return View("TMeasureUnits", Sales.GetAllSales());
        }
    }
}
