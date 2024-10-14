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

        public IActionResult Details(int id)
        {
            //var category = _categoryService.Query().FirstOrDefault(c => c.Record.Id == id);
            var category = _categoryService.Query().SingleOrDefault(c => c.Record.Id == id);
            if (category is null) // ==: same as "is"
                return NotFound();
            return View(category);
        }
    }
}
