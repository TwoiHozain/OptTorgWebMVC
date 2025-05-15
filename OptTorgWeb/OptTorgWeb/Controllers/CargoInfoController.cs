using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class CargoInfoController : Controller
    {
        private string _ViewForm = "TCargoInfo";

        private string _FormPickSending = "TPickSending";
        private string _FormPickSales = "TPickSales";
        private string _FormPickCargoInfoData = "PickCargoInfoData";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        
        //Read
        [HttpGet]
        public IActionResult TCargoInfo()
        {
            ViewBag.Role = CurrentUser.role;

            if (CurrentUser.isCustomer)
            {
                return View(_ViewForm, CargoInfo.GetAllCustomerCargoInfo(CurrentUser.customerId));
            }

            return View(_ViewForm, CargoInfo.GetAllCargoInfo());
        }

        //Create
        [HttpGet]
        public IActionResult OpenPickSending()
        {
            return View(_FormPickSending);
        }

        [HttpPost]
        public IActionResult OpenPickSales(CargoInfo p)
        {
            return View(_FormPickSales, p);
        }

        [HttpPost]
        public IActionResult OpenPickCargoInfoData(CargoInfo p)
        {
            ViewData["InSendingCount"] = Sales.GetSalesProductCount(p.SaleId);

            return View(_FormPickCargoInfoData, p);
        }

        [HttpPost]
        public IActionResult CreateCargoInfo(CargoInfo p)
        {
            double ciMass = p.Count * Sales.GetSalesNav(p.SaleId).Product.MassPrutto;
            double deliveryMass = Sending.GetSendingWeight(p.SendingId);
            double tonnage = Sending.GetSendingTransport(p.SendingId).Tonnage;

            if (deliveryMass + ciMass > tonnage)
            {
                p.Count = 0;
                ViewBag.Message = $"Масса груза слишком велика. Доступный тоннаж: {tonnage}; масса груза {ciMass}; загрузка авто {deliveryMass}";
                ViewBag.Type = 1;
                return OpenPickCargoInfoData(p);
            }

            CargoInfo.CreatrCargoInfo(p);
            return View(_ViewForm, CargoInfo.GetAllCargoInfo());
        }

        ////Delete
        [HttpPost]
        public IActionResult DCargoInfo(long id)
        {
            CargoInfo.DeleteCargoInfo(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, CargoInfo.GetAllCargoInfo());
        }

        public IActionResult DCascade(int id)
        {
            CargoInfo.DCascade(id);
            return View("TMeasureUnits", CargoInfo.GetAllCargoInfo());
        }
    }
}
