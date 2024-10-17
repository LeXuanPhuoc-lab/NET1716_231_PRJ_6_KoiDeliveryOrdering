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
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Text;
using System.Net.Http.Headers;
using KoiDeliveryOrdering.Data.Dtos;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "users"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null && result.Data != null)
                        {
                            var userModels
                                = JsonConvert.DeserializeObject<List<UserModel>>(
                                    result.Data.ToString());

                            return View(userModels);
                        }
                    }
                }
            }

            return View(new List<UserModel>());
        }


            public async Task<IActionResult> Delete(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                UserModel user = null;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"users/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<ServiceResult>(content);

                            if (result != null && result.Data != null)
                            {
                                user = JsonConvert.DeserializeObject<UserModel>(result.Data.ToString());
                            }
                        }
                    }
                }

                if (user == null)
                {
                    return NotFound();
                }

                return View(user); // Pass the user data to the view
            }   

        //// POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid UserId)
        {
            bool delelteStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.DeleteAsync(Const.APIEndpoint + "users/" + UserId + "/remove"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<ServiceResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_REMOVE_CODE)
                            {
                                delelteStatus = true;
                            }
                        }
                    }
                }
            }

            if (delelteStatus)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Delete));
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Create User";
            var userDto = new UserDto();  // Ensure the model is properly initialized
            return View(userDto);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            userModel.IsActive = true; // Set IsActive to true by default

            // Add the logic to set CreateDate
            userModel.CreateDate = DateTime.UtcNow;

            bool saveStatus = false;
            using (var httpClient = new HttpClient())
            {
                var apiUrl = Const.APIEndpoint + "/users/insert";
                var response = await httpClient.PostAsJsonAsync(apiUrl, userModel);

                if (response.IsSuccessStatusCode)
                {
                    var context = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ServiceResult>(context);

                    if (result != null && result.Status == Const.SUCCESS_INSERT_CODE)
                    {
                        saveStatus = true;
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(userModel);
        }




        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}