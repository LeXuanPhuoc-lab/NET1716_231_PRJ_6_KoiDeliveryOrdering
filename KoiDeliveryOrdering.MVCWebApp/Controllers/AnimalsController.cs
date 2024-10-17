using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Newtonsoft.Json;
using KoiDeliveryOrdering.API.Payloads.Requests;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class AnimalsController : Controller
    {

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "animals"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var animals = JsonConvert.DeserializeObject<List<AnimalModel>>(
                                result.Data.ToString());



                            return View(animals);
                        }
                    }
                }
            }
            return View(new List<AnimalModel>());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimalModel? animalModel = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "animals/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            animalModel = JsonConvert.DeserializeObject<AnimalModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return animalModel != null
                ? View(animalModel)
                : NotFound();
        }

        // GET: Animals/Create
        public async Task<IActionResult> Create()
        {
            List<SelectListItem>? animalTypeList = null;

            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(Const.APIEndpoint + "animals/types"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var content = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(content.ToString());
                        if (result != null && result.Data != null)
                        {
                            var animalTypes = JsonConvert.DeserializeObject<List<AnimalTypeModel>>(result.Data.ToString());
                            if (animalTypes != null)
                            {
                                animalTypeList = animalTypes.Select(a => new SelectListItem
                                {
                                    Value = a.AnimalTypeId.ToString(),
                                    Text = a.AnimalTypeDesc
                                }).ToList();
                            }
                        }
                    }
                }
            }

            if (animalTypeList != null)
            {
                ViewBag.AnimalTypeList = new SelectList(animalTypeList, "Value", "Text");
                return View();
            }
            else
            {
                return NotFound(); // Xử lý khi không lấy được danh sách loại động vật
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnimalRequest animalModel)
        {
            bool saveStatus = false;
            //Animal animal = new Animal()
            //{
            //    Age = animalModel.Age,
            //    IsAvailable = true,
            //    AnimalTypeId = animalModel.AnimalTypeId,
            //    Breed = animalModel.Breed,
            //    ColorPattern = animalModel.ColorPattern,
            //    Description = animalModel.Description,
            //    HealthStatus = animalModel.HealthStatus,
            //    ImageUrl = animalModel.ImageUrl,
            //    OriginCountry = animalModel.OriginCountry,
            //    Size = animalModel.Size,
            //    EstimatedPrice = animalModel.EstimatedPrice,
            //    AnimalId = Guid.NewGuid(),
            //};
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(Const.APIEndpoint + "animals", animalModel))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Status == Const.SUCCESS_INSERT_CODE)
                        {
                            saveStatus = true;
                        }
                        else
                        {
                            saveStatus = false;
                        }
                    }

                }
                if (saveStatus)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await SetDefaultViewDataAsync();

                    return View();
                }
            }
        }
        private async Task SetDefaultViewDataAsync()
        {
            List<SelectListItem>? animalTypeList = null;

            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(Const.APIEndpoint + "animals/types"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var content = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(content.ToString());
                        if (result != null && result.Data != null)
                        {
                            var animalTypes = JsonConvert.DeserializeObject<List<AnimalTypeModel>>(result.Data.ToString());
                            if (animalTypes != null)
                            {
                                animalTypeList = animalTypes.Select(a => new SelectListItem
                                {
                                    Value = a.AnimalTypeId.ToString(),
                                    Text = a.AnimalTypeDesc
                                }).ToList();
                            }
                        }
                    }
                }
            }

            ViewBag.AnimalTypeList = animalTypeList != null ?
                new SelectList(animalTypeList, "Value", "Text") : null;
        }




        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null
            if (id == null)
            {
                return NotFound();
            }

            // Get the animal types to populate the dropdown
            List<SelectListItem>? animalTypeList = await GetAnimalTypesAsync();
            if (animalTypeList == null)
            {
                return NotFound(); // Handle when unable to fetch animal types
            }

            // Fetch the animal details
            UpdateAnimalRequest? animalModel = await GetAnimalByIdAsync(id.Value);
            if (animalModel == null)
            {
                return NotFound(); // Handle when the animal is not found
            }

            // Set the ViewBag for animal types
            ViewBag.AnimalTypeList = new SelectList(animalTypeList, "Value", "Text");

            // Return the view with the animal model
            return View(animalModel);
        }

        // Method to fetch animal types
        private async Task<List<SelectListItem>?> GetAnimalTypesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(Const.APIEndpoint + "animals/types"))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var content = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(content);
                        if (result?.Data != null)
                        {
                            var animalTypes = JsonConvert.DeserializeObject<List<AnimalTypeModel>>(result.Data.ToString());
                            if (animalTypes != null)
                            {
                                return animalTypes.Select(a => new SelectListItem
                                {
                                    Value = a.AnimalTypeId.ToString(),
                                    Text = a.AnimalTypeDesc
                                }).ToList();
                            }
                        }
                    }
                }
            }
            return null; // Return null if fetching animal types fails
        }

        // Method to fetch an animal by ID
        private async Task<UpdateAnimalRequest?> GetAnimalByIdAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(Const.APIEndpoint + "animals/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);
                        if (result?.Data != null)
                        {
                            return JsonConvert.DeserializeObject<UpdateAnimalRequest>(result.Data.ToString());
                        }
                    }
                }
            }
            return null; // Return null if fetching the animal fails
        }


        //// POST: Animals/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateAnimalRequest animal)
        {
            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.PutAsJsonAsync(
                           Const.APIEndpoint + "animals", animal))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                        if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                        {
                            saveStatus = true;
                        }
                        else
                        {
                            saveStatus = false;
                        }
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await SetDefaultViewDataAsync();

                return await this.Edit(id);
            }
        }

        //// GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimalModel? animalModel = null!;
            using (var httpClient = new HttpClient())
            {
                using (var resp = await httpClient.GetAsync(
                           Const.APIEndpoint + "animals/" + id))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var context = await resp.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            animalModel = JsonConvert.DeserializeObject<AnimalModel>(
                                result.Data.ToString()!);
                        }
                    }
                }
            }

            return animalModel != null
                ? View(animalModel)
                : NotFound();
        }

        //// POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(
                    Const.APIEndpoint + "animals/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_REMOVE_CODE)
                        {
                            deleteStatus = true;
                        }
                    }
                }
            }


            return deleteStatus
                ? RedirectToAction(nameof(Index))
                : RedirectToAction(nameof(Delete));
        }

        //private bool AnimalExists(int id)
        //{
        //    return _context.Animals.Any(e => e.Id == id);
        //}
    }
}
