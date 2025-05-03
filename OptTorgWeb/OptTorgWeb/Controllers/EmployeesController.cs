using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private string _ContactsViewForm = "TCEmployees";
        private string _AdressViewForm = "TAEmployees";
        private string _CreateForm = "CEmployees";
        private string _EditForm = "EEmployees";

        //Read
        [HttpGet]
        public IActionResult TAEmployees()
        {
            return View(_AdressViewForm, Employees.GetAllEmployees());
        }

        public IActionResult TCEmployees()
        {
            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCEmployees()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CEmployees(Employees p)
        {
            Employees.CreatrEmployees(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;
            //TODO:
            //Возврат к форме из которой пришел

            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }

        [HttpPost]
        public IActionResult CFewEmployees(Employees p)
        {
            Employees.CreatrEmployees(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }

        //Edit
        [HttpPost]
        public IActionResult OpenEEmployees(int id)
        {
            return View(_EditForm, Employees.GetEmployeesById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesEmployees(Employees p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;
            p.Active = true;

            Employees.UpdateEmployees(p);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }

        ////Delete
        [HttpPost]

        public IActionResult DEmployees(int id)
        {
            Employees.DeleteEmployees(id);
            //TODO:
            //Возврат к форме из которой пришел
            return View(_ContactsViewForm, Employees.GetAllEmployees());
        }
        //ToDo
        public IActionResult DCascadeEmployees(int id)
        {
            return View("TMeasureUnits", Employees.GetAllEmployees());
        }
    }
}
