using Microsoft.AspNetCore.Mvc;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class PositionsController : Controller
    {
        private string _ViewForm = "TPositions";
        private string _CreateForm = "CPositions";
        private string _EditForm = "EPositions";

        //Read
        [HttpGet]
        public IActionResult TPositions()
        {
            return View(_ViewForm, Positions.GetAllPositions());
        }

        //Create
        [HttpGet]
        public IActionResult OpenCPositions()
        {
            return View(_CreateForm);
        }

        [HttpPost]
        public IActionResult CPositions(Positions p)
        {
            Positions.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            return View(_ViewForm, Positions.GetAllPositions());
        }

        [HttpPost]
        public IActionResult CFewPositions(Positions p)
        {
            Positions.CreatrPosition(p);

            ViewData["Message"] = "Запись успешно создана";
            ViewData["Type"] = 0;

            ModelState.Clear();

            return View(_CreateForm);
        }
        //Edit
        [HttpPost]
        public IActionResult OpenEPositions(int id)
        {
            return View(_EditForm, Positions.GetPositionById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesPositions(Positions p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Positions.UpdatePosition(p);

            return View(_ViewForm, Positions.GetAllPositions());
        }

        ////Delete
        [HttpPost]
        public IActionResult DPosition(int id)
        {
            Positions.DeletePosition(id);
            return View(_ViewForm, Positions.GetAllPositions());
        }
        //ToDo
        public IActionResult DCascadePosition(int id)
        {
            return View("TMeasureUnits", Positions.GetAllPositions());
        }
    }
}
