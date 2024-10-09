using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
	public class DocumentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
