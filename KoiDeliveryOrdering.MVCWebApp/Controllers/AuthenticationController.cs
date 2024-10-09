using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KoiDeliveryOrdering.MVCWebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        [BindProperty]
        public string ErrorMsg { get; set; } = string.Empty;

        public IActionResult Index()
        {
            // Remove session (if any)
            var username = HttpContext.Session.GetString("Username");
            if (!string.IsNullOrEmpty(username)) HttpContext.Session.Remove("Username");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel user)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "users/" + user.Username))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var context = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ServiceResult>(context.ToString());
                        if (result != null
                            && result.Data != null && result.Status == Const.SUCCESS_READ_CODE)
                        {
                            var userModel = JsonConvert.DeserializeObject<UserModel>(
                                result.Data.ToString());


                            if(userModel == null || userModel.Password != user.Password)
                            {
                                ModelState.AddModelError("ErrorMsg", "Incorrect username or password");
                                return RedirectToAction(nameof(Index));
                            }

                            // Add user to session
                            //HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                            HttpContext.Session.SetString("Username", userModel.Username);

                            return RedirectToAction("Index","Home");
                        }
                    }
                }
            }

            ModelState.AddModelError("ErrorMsg", "Something went wrong.");
            return RedirectToAction(nameof(Index));
        }
    }
}
