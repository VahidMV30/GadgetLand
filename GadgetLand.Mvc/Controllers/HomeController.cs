using Microsoft.AspNetCore.Mvc;

namespace GadgetLand.Mvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
