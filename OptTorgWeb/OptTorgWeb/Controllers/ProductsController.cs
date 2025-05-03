using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class ProductsController : Controller
    {
        private string _ViewFormMass = "TMassProducts";
        private string _ViewFormGeneral = "TGeneralProducts";
        private string _ViewFormCoast = "TCostProducts";

        private string _CreateForm = "CProducts";
        private string _EditForm = "EProducts";

        //Read
        [HttpGet]
        public IActionResult OpenTProductsMass()
        {
            return View(_ViewFormMass, Products.GetAllProducts());
        }

        [HttpGet]
        public IActionResult OpenTProductsGeneral()
        {
            return View(_ViewFormGeneral, Products.GetAllProducts());
        }

        [HttpGet]
        public IActionResult OpenTProductsCoast()
        {
            return View(_ViewFormCoast, Products.GetAllProducts());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCProducts()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CProducts(Products p)
        {
            Products.CreatrProducts(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewFormGeneral, Products.GetAllProducts());
        }

        [HttpPost]
        public IActionResult CFewProducts(Products p)
        {
            Products.CreatrProducts(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }

        //Edit
        [HttpPost]
        public IActionResult OpenEProducts(int id)
        {
            return View(_EditForm, Products.GetProductsById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesProducts(Products p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Products.UpdateProducts(p);

            return View(_ViewFormGeneral, Products.GetAllProducts());
        }

        ////Delete
        [HttpPost]
        public IActionResult DProducts(int id)
        {
            Products.DeleteProducts(id);
            return View(_ViewFormGeneral, Products.GetAllProducts());
        }

        //ToDo
        public IActionResult DCascadeProducts(int id)
        {
            return View("TMeasureUnits", Products.GetAllProducts());
        }
    }
}
