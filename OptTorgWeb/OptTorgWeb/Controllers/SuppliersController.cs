using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class SuppliersController : Controller
    {
        private string _ContactsViewForm = "TCSuppliers";
        private string _AdressViewForm = "TASuppliers";
        private string _CreateForm = "CSuppliers";
        private string _EditForm = "ESuppliers";

        //Read
        [HttpGet]
        public IActionResult TASuppliers()
        {
            return View(_AdressViewForm, Suppliers.GetAllSuppliers());
        }

        public IActionResult TCSuppliers()
        {
            return View(_ContactsViewForm, Suppliers.GetAllSuppliers());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCSuppliers()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CSuppliers(Suppliers p)
        {
            Suppliers.CreatrSuppliers(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;
            //TODO:
            //Возврат к форме из которой пришел

            return View(_ContactsViewForm, Suppliers.GetAllSuppliers());
        }

        [HttpPost]
        public IActionResult CFewSuppliers(Suppliers p)
        {
            Suppliers.CreatrSuppliers(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenESuppliers(int id)
        {
            return View(_EditForm, Suppliers.GetSuppliersById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesSuppliers(Suppliers p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Suppliers.UpdateSuppliers(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Suppliers.GetAllSuppliers());
        }

        ////Delete
        [HttpPost]

        public IActionResult DSuppliers(int id)
        {
            Suppliers.DeleteSuppliers(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Suppliers.GetAllSuppliers());
        }
        //ToDo
        public IActionResult DCascadeSuppliers(int id)
        {
            return View("TMeasureUnits", Suppliers.GetAllSuppliers());
        }
    }
}
