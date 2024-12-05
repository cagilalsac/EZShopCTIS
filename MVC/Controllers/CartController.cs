using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services;
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
        private readonly IProductService _productService;

        public CartController(HttpServiceBase httpService, IProductService productService)
        {
            _httpService = httpService;
            _productService = productService;
        }

        private int GetUserId() => Convert.ToInt32(User.Claims?.Single(c => c.Type == "Id").Value);

        private List<CartItemModel> GetSession(int userId)
        {
            var cart = _httpService.GetSession<List<CartItemModel>>(SESSIONKEY);

            // Way 1:
            //if (cart is not null)
            //    cart = cart.Where(c => c.UserId == userId).ToList();
            //return cart;

            // Way 2:
            return cart?.Where(c => c.UserId == userId).ToList();
        }

        public IActionResult Get()
        {
            var cart = GetSession(GetUserId());
            return View("List", cart);
        }

        // GET: /Cart/Add?productId=13
        public IActionResult Add(int productId)
        {
            int userId = GetUserId();
            var cart = GetSession(userId);
            cart = cart ?? new List<CartItemModel>();
            var product = _productService.Query().SingleOrDefault(p => p.Record.Id == productId);

            // Way 1:
            //var cartItem = new CartItemModel();
            //cartItem.ProductName = product.Name;
            //cartItem.ProductUnitPrice = product.UnitPrice;
            //cartItem.ProductId = product.Record.Id;
            //cartItem.UserId = userId;
            // Way 2:
            var cartItem = new CartItemModel()
            {
                ProductName = product.Name,
                ProductUnitPrice = product.UnitPrice,
                ProductId = product.Record.Id,
                UserId = userId
            };
            cart.Add(cartItem);
            _httpService.SetSession(SESSIONKEY, cart);
            TempData["Message"] = $"\"{cartItem.ProductName}\" added to cart.";
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Remove(int productId)
        {
            var cart = GetSession(GetUserId());
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            cart.Remove(cartItem);
            _httpService.SetSession(SESSIONKEY, cart);
            return RedirectToAction(nameof(Get));
        }
    }
}
