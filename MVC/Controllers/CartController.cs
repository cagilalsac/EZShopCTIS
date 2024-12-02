using BLL.Controllers.Bases;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public class CartController : MvcController
    {
        const string SESSIONKEY = "cart";

        private readonly HttpServiceBase _httpService;

        public CartController(HttpServiceBase httpService)
        {
            _httpService = httpService;
        }

        public IActionResult Get()
        {
            var cart = _httpService.GetSession<List<CartItemModel>>(SESSIONKEY);
            return View("List", cart);
        }
    }
}
