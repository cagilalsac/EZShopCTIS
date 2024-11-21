#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize]
    public class ProductsController : MvcController
    {
        // Service injections:
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        /* Can be uncommented and used for many to many relationships. Store may be replaced with the related entiy name in the controller and views. */
        private readonly IStoreService _StoreService;

        public ProductsController(
			IProductService productService
            , ICategoryService categoryService

            /* Can be uncommented and used for many to many relationships. Store may be replaced with the related entiy name in the controller and views. */
            , IStoreService StoreService
        )
        {
            _productService = productService;
            _categoryService = categoryService;

            /* Can be uncommented and used for many to many relationships. Store may be replaced with the related entiy name in the controller and views. */
            _StoreService = StoreService;
        }

        // GET: Products
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _productService.Query().ToList();
            return View(list);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _productService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["CategoryId"] = new SelectList(_categoryService.Query().ToList(), "Record.Id", "Name");

            /* Can be uncommented and used for many to many relationships. Store may be replaced with the related entiy name in the controller and views. */
            ViewBag.StoreIds = new MultiSelectList(_StoreService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _productService.Create(product.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = product.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _productService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Products/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _productService.Update(product.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = product.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(product);
        }

        // GET: Products/Delete/5
        // Way 3:
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Way 2:
            //var role = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);
            //if (role is null || role.Value != "Admin")
            //    return RedirectToAction("Login", "Users");
            // Way 1:
            //if (!User.IsInRole("Admin"))
            //    return RedirectToAction("Login", "Users");
            // Get item to delete service logic:
            var item = _productService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Products/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Way 3:
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _productService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
