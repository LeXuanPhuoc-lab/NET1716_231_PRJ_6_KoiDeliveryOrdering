using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data.Dtos.Documents;
using KoiDeliveryOrdering.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new DocumentMutationDto();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(DocumentMutationDto dto)
        {
            if (ModelState.IsValid)
            {
                using var httpClient = new HttpClient();
                using var resp = await httpClient.PostAsJsonAsync(
                    Const.APIEndpoint + "documents", dto);
                if (resp.IsSuccessStatusCode)
                {
                    var context = await resp.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                    if (result != null && result.Status == Const.SUCCESS_INSERT_CODE)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(dto);
        }
    }
}
