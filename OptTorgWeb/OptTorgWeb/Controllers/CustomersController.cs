using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class CustomersController : Controller
    {
        private string _ContactsViewForm = "TCCustomers";
        private string _AdressViewForm = "TACustomers";
        private string _CreateForm = "CCustomers";
        private string _EditForm = "ECustomers";

        //Read
        [HttpGet]
        public IActionResult TACustomers()
        {
            return View(_AdressViewForm, Customers.GetAllCustomers());
        }

        public IActionResult TCCustomers()
        {
            return View(_ContactsViewForm, Customers.GetAllCustomers());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCCustomers()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CCustomers(Customers p)
        {
            Customers.CreatrCustomers(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;
            //TODO:
            //Возврат к форме из которой пришел

            return View(_ContactsViewForm, Customers.GetAllCustomers());
        }

        [HttpPost]
        public IActionResult CFewCustomers(Customers p)
        {
            Customers.CreatrCustomers(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenECustomers(int id)
        {
            return View(_EditForm, Customers.GetCustomersById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesCustomers(Customers p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;
            p.Active = true;

            Customers.UpdateCustomers(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Customers.GetAllCustomers());
        }

        ////Delete
        [HttpPost]

        public IActionResult DCustomers(int id)
        {
            Customers.DeleteCustomers(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Customers.GetAllCustomers());
        }
        //ToDo
        public IActionResult DCascadeCustomers(int id)
        {
            return View("TMeasureUnits", Customers.GetAllCustomers());
        }
    }
}
