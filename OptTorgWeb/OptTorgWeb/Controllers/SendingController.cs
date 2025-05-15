using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OptTorgWeb.Classes;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Controllers
{
    public class SendingController : Controller
    {
        private string _ViewForm = "TSending";
        private string _EditForm = "ESendingData";

        private string _TPickSendingForm = "TPickSending";

        private string _FormPickStorage = "TPickStorage";
        private string _FormPickPricing = "TPickPricing";

        private string _FormPickGlavBuh = "TPickGlavBuh";
        private string _FormPickSeOtpuskProizvel = "TPickSeOtpuskProizvel";
        private string _FormPickSeOtpuskRazreshil = "TPickSeOtpuskRazreshil";
        private string _FormPickDriver = "TPickDriver";

        private string _FormPickTransport = "TPickTransport";
        private string _FormPickCustomer = "TPickCustomers";
        private string _FormPickSendingData = "PickSendingData";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (Controller)context.Controller;
            controller.ViewData["Layout"] = CurrentUser.layout;
        }
        //Read
        [HttpGet]
        public IActionResult TSending()
        {
            ViewBag.Role = CurrentUser.role;

            if (CurrentUser.isCustomer)
            {
                return View(_ViewForm, Sending.GetAllCustomerSendings(CurrentUser.customerId));
            }

            return View(_ViewForm, Sending.GetAllSendings());
        }

        [HttpGet]
        public IActionResult OpenPickSending()
        {

            if (CurrentUser.isCustomer)
            {
                return View(_ViewForm, Sending.GetCustomerSendingsFor1T(CurrentUser.customerId));
            }

            return View(_TPickSendingForm, Sending.GetSendingsFor1T());
        }

        [HttpPost]
        public IActionResult CreateTovarnTransp1Ting(long id)
        {
            //string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            //string baseDir = Path.GetFullPath(Path.Combine(exeDir, "..", "..", "..", "..")); ;

            //String _sourcePath = Path.Combine(baseDir, "1Tblank.xlsx");
            //String _outputPath = Path.Combine(baseDir, "1Tblanktmp.xlsx");

            Sending s = Sending.GetSendingById(id);

            string mimeType = "application/vnd.ms-excel";
            
            string date = Sending.GetSendingById(id).DateArrivalLoading.Value.ToShortDateString() + "  " + DateTime.Now.ToLongDateString();
            date = date.Replace(".", " ");
            date = date.Replace(",", " ");

            string fileName = $"1T {date}.XLS";

            //byte[] fileBytes = System.IO.File.ReadAllBytes(_outputPath);

            //System.IO.File.Delete(_outputPath);

            return File(TovarnTransp1T.CreateExcell1T(s), mimeType, fileName);
        }
        
        //Create
        [HttpGet]
        public IActionResult OpenPickCustomer()
        {
            return View(_FormPickCustomer);
        }

        [HttpPost]
        public IActionResult OpenPickStorage(Sending d)
        {
            return View(_FormPickStorage);
        }

        [HttpPost]
        public IActionResult OpenPickPricing(Sending d)
        {
            return View(_FormPickPricing, d);
        }

        [HttpPost]
        public IActionResult OpenPickGlavBuh(Sending d)
        {
            return View(_FormPickGlavBuh, d);
        }

        [HttpPost]
        public IActionResult OpenPickOtpuskProizvel(Sending d)
        {
            return View(_FormPickSeOtpuskProizvel, d);
        }

        [HttpPost]
        public IActionResult OpenPickOtpuskRazreshil(Sending d)
        {
            return View(_FormPickSeOtpuskRazreshil, d);
        }

        [HttpPost]
        public IActionResult OpenPickDriver(Sending d)
        {
            return View(_FormPickDriver, d);
        }

        [HttpPost]
        public IActionResult OpenPickTransport(Sending d)
        {
            return View(_FormPickTransport, d);
        }

        [HttpPost]
        public IActionResult OpenPickSendingData(Sending d)
        {
            return View(_FormPickSendingData, d);
        }

        [HttpPost]
        public IActionResult CreateSending(Sending d)
        {
            if (!ModelState.IsValid)
            {
                return View(_FormPickSendingData, d);
            }

            Sending.CreateSending(d);
            return View(_ViewForm, Sending.GetAllSendings());
        }

        //Edit
        [HttpPost]
        public IActionResult OpenESending(long id)
        {
            return View(_EditForm, Sending.GetSendingById(id));
        }

        [HttpPost]
        public IActionResult SaveChangesSending(Sending p)
        {
            ViewData["Message"] = "Запись успешно изменена";
            ViewData["Type"] = 0;

            Sending.UpdateSending(p);

            return View(_ViewForm, Sending.GetAllSendings());
        }

        //Delete
        [HttpPost]

        public IActionResult DSending(int id)
        {
            Sending.DeleteSending(id);
            return View(_ViewForm, Sending.GetAllSendings());
        }

        public IActionResult DCascade(int id)
        {
            Sending.DCascade(id);
            return View("TMeasureUnits", Sending.GetAllSendings());
        }
    }
}
