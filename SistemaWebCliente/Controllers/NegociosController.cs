using Microsoft.AspNetCore.Mvc;

namespace SistemaWebCliente.Controllers
{
    public class NegociosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
