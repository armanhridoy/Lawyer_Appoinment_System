using Microsoft.AspNetCore.Mvc;

namespace LawyerAppointmentSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
