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

        // TODO: Get UserId from Claims
        private int GetUserId() =>

        private List<CartItemModel> GetSession(int userId)
        {
            var cart = _httpService.GetSession<List<CartItemModel>>(SESSIONKEY);
            return cart.Where(c => c.UserId == userId).ToList();
        }

        public IActionResult Get()
        {
            var cart = GetSession(GetUserId());
            return View("List", cart);
        }
    }
}
