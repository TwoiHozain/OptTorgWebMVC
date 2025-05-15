using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class ProductPartController : Controller
    {
        private string _ViewForm = "TProductPart";

        private string _FormPickDelivery = "TPickDelivery";
        private string _FormPickBuy = "TPickBuys";
        private string _FormPickProductPartData = "PickProductPartData";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }

        //Read
        [HttpGet]
        public IActionResult TProductPart()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ViewForm, ProductPart.GetAllProductPart());
        }

        //Create
        [HttpGet]
        public IActionResult OpenPickDelivery()
        {
            return View(_FormPickDelivery);
        }

        [HttpPost]
        public IActionResult OpenPickBuy(ProductPart p)
        {
            return View(_FormPickBuy, p);
        }

        [HttpPost]
        public IActionResult OpenPickProductPartData(ProductPart p)
        {
            p.Buy = Buys.GetBuysByIdNavigation(p.BuyId);
            p.Delivery = Delivery.GetDeliveryById(p.DeliveryId);
            
            ViewData["InDeliveryCount"] = Buys.GetBuysProductCount(p.BuyId);

            return View(_FormPickProductPartData, p);
        }

        [HttpPost]
        public IActionResult CreateProductPart(ProductPart p)
        {
            double ppMass = p.Count * Buys.GetBuysByIdNavigation(p.BuyId).Product.MassPrutto;
            double deliveryMass = Delivery.GetDeliveryWeight(p.DeliveryId);
            double tonnage = Delivery.GetDeliveryById(p.DeliveryId).Transport.Tonnage;

            if (deliveryMass + ppMass > tonnage)
            {
                p.Count = 0;
                ViewBag.Message = $"Масса груза слишком велика. Доступный тоннаж: {tonnage}; масса груза {ppMass}; загрузка авто {deliveryMass}";
                ViewBag.Type = 1;
                return OpenPickProductPartData(p);
            }

            ProductPart.CreatrProductPart(p);
            return View(_ViewForm, ProductPart.GetAllProductPart());
        }

        ////Delete
        [HttpPost]
        public IActionResult DProductPart(long id)
        {
            ProductPart.DeleteProductPart(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ViewForm, ProductPart.GetAllProductPart());
        }

        public IActionResult DCascade(int id)
        {
            ProductPart.DCascade(id);
            return View("TMeasureUnits", ProductPart.GetAllProductPart());
        }
    }
}
