using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class ProductSortController : Controller
    {
        private string _ViewForm = "TProductSort";
        private string _CreateForm = "CProductSort";
        private string _EditForm = "EProductSort";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TProductSort()
        {
            ViewBag.Role = CurrentUser.role;

            return View(_ViewForm, ProductSort.GetAllProductSort());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCProductSort()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CProductSort(ProductSort p)
        {
            ProductSort.CreatrProductSort(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, ProductSort.GetAllProductSort());
        }

        [HttpPost]
        public IActionResult CFewProductSort(ProductSort p)
        {
            ProductSort.CreatrProductSort(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEProductSort(int id)
        {
            return View(_EditForm, ProductSort.GetProductSortById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesProductSort(ProductSort p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            ProductSort.UpdateProductSort(p);

            return View(_ViewForm, ProductSort.GetAllProductSort());
        }

        ////Delete
        [HttpPost]
        public IActionResult DProductSort(int id)
        {
            ProductSort.DeleteProductSort(id);
            return View(_ViewForm, ProductSort.GetAllProductSort());
        }

        public IActionResult DCascade(int id)
        {
            ProductSort.DCascade(id);
            return View("TMeasureUnits", ProductSort.GetAllProductSort());
        }
    }
}
