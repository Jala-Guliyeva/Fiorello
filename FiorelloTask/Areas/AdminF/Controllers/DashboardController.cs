using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloTask.Areas.AdminF.Controllers
{
    public class DashboardController : Controller
    {
        [Area("AdminF")]
        [Authorize(Roles ="Admin")]  //qeydiyyatdi,qeydiyyatolmasa adminpanele gedmek olmasin
        public IActionResult Index()
        {
            return View();
        }
    }
}
