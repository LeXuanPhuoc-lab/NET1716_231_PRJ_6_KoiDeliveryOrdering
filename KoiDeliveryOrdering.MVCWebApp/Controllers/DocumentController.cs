using System.Runtime.InteropServices.JavaScript;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data.Dtos.Documents;
using KoiDeliveryOrdering.Service.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class DocumentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(Const.APIEndpoint + "documents");
            if (response.IsSuccessStatusCode)
            {
                var context = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResult>(context);
                if (result is { Data: not null })
                {
                    var documents =
                        JsonConvert.DeserializeObject<List<DocumentDto>>(result.Data.ToString() ?? "[]");

                    return View(documents);
                }
            }

            return View(new List<DocumentDto>());
        }

        public IActionResult Create()
        {
            var model = new DocumentMutationDto();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            ViewData["Id"] = id;

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(Const.APIEndpoint + "documents/" + id);
            if (response.IsSuccessStatusCode)
            {
                var context = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResult>(context);
                if (result is { Data: not null })
                {
                    var document =
                        JsonConvert.DeserializeObject<DocumentDto>(result.Data.ToString() ?? "");

                    if (document is not null)
                        return View(document.Adapt<DocumentMutationDto>());
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentMutationDto dto)
        {
            if (dto.IssueDate < DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("IssueDate", "Issue date must be today or after");
            }

            if (dto.IssueDate > dto.ExpirationDate)
            {
                ModelState.AddModelError("ExpirationDate", "Expiration date must be after Issue date");
            }

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

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, DocumentMutationDto dto)
        {
            if (id is null)
            {
                return NotFound();
            }

            ViewData["Id"] = id;

            if (ModelState.IsValid)
            {
                using var httpClient = new HttpClient();
                using var resp = await httpClient.PutAsJsonAsync(
                    Const.APIEndpoint + "documents/" + id, dto);
                if (resp.IsSuccessStatusCode)
                {
                    var context = await resp.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                    if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(dto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            using var httpClient = new HttpClient();
            using var resp = await httpClient.DeleteAsync(
                Const.APIEndpoint + "documents/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var context = await resp.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                if (result != null && result.Status == Const.SUCCESS_REMOVE_CODE)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}