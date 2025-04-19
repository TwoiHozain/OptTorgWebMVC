using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class PackagingController : Controller
    {
        private string _ViewForm = "TPackaging";
        private string _CreateForm = "CPackaging";
        private string _EditForm = "EPackaging";

        //Read
        [HttpGet]
        public IActionResult TPackaging()
        {
            return View(_ViewForm, Packaging.GetAllPackaging());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCPackaging()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CPackaging(Packaging p)
        {
            Packaging.CreatrPackaging(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, Packaging.GetAllPackaging());
        }

        [HttpPost]
        public IActionResult CFewPackaging(Packaging p)
        {
            Packaging.CreatrPackaging(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEPackaging(int id)
        {
            return View(_EditForm, Packaging.GetPackagingById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesPackaging(Packaging p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Packaging.UpdatePackaging(p);

            return View(_ViewForm, Packaging.GetAllPackaging());
        }

        ////Delete
        [HttpPost]
        public IActionResult DPackaging(int id)
        {
            Packaging.DeletePackaging(id);
            return View(_ViewForm, Packaging.GetAllPackaging());
        }
        //ToDo
        public IActionResult DCascadePackaging(int id)
        {
            return View("TMeasureUnits", Packaging.GetAllPackaging());
        }
    }
}
