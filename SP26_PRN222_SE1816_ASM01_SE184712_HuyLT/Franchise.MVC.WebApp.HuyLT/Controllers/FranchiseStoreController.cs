using Microsoft.AspNetCore.Mvc;
using Franchise.Services.HuyLT;
using Franchise.Entities.HuyLT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Franchise.MVC.WebApp.HuyLT.Filters;

namespace Franchise.MVC.WebApp.HuyLT.Controllers
{
    public class FranchiseStoreController : Controller
    {
        private readonly IFranchiseStoreService _storeService;

        public FranchiseStoreController(IFranchiseStoreService storeService)
        {
            _storeService = storeService;
        }

        // Franchise Store Login Page
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            var store = await _storeService.GetStoreByLoginAsync(Username, Password);
            if (store != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, store.StoreName),
                    new Claim(ClaimTypes.Email, store.Email),
                    new Claim("StoreId", store.StoreId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Set Session for convenience
                HttpContext.Session.SetInt32("StoreId", store.StoreId);
                HttpContext.Session.SetString("StoreName", store.StoreName);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Invalid email or phone number.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // 1. Hiển thị danh sách cửa hàng
        [HttpGet]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public async Task<IActionResult> ViewInfo()
        {
            int? storeId = HttpContext.Session.GetInt32("StoreId");
            if (storeId.HasValue)
            {
                var store = await _storeService.GetStoreByIdAsync(storeId.Value);
                return View(store);
            }
            return RedirectToAction("Login");
        }

        // 2. Hiển thị chi tiết một cửa hàng
        [ServiceFilter(typeof(AuthenticationFilter))]
        public async Task<IActionResult> Details(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null) return NotFound();

            return View(store);
        }

        // 3. GET: Trang chỉnh sửa thông tin cửa hàng
        [HttpGet]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null) return NotFound();

            return View(store);
        }

        // 4. POST: Lưu thông tin chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public async Task<IActionResult> Edit(FranchiseStore store)
        {
            if (ModelState.IsValid)
            {
                await _storeService.UpdateStoreAsync(store);
                return RedirectToAction(nameof(ViewInfo));
            }
            return View(store);
        }
    }
}
