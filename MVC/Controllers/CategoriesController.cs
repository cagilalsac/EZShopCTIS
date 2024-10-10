using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var list = _categoryService.Query().ToList();
            return View(list);
        }
    }
}
