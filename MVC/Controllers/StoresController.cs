#nullable disable
using BLL.Controllers.Bases;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Generated from Custom Template.

namespace MVC.Controllers
{
    //[Authorize(Roles = "Admin,User")]
    [Authorize]
    public class StoresController : MvcController
    {
        // Service injections:
        private readonly IStoreService _storeService;
        //private readonly ICityService _cityService;
        //private readonly ICountryService _countryService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public StoresController(
			IStoreService storeService
            //, ICityService cityService
            //, ICountryService countryService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _storeService = storeService;
            //_cityService = cityService;
            //_countryService = countryService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Stores
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _storeService.Query().ToList();
            return View(list);
        }

        // GET: Stores/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _storeService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            //ViewData["CityId"] = new SelectList(_cityService.Query().ToList(), "Record.Id", "Name");
            //ViewData["CountryId"] = new SelectList(_countryService.Query().ToList(), "Record.Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _storeService.Create(store.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = store.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(store);
        }

        // GET: Stores/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _storeService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Stores/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StoreModel store)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _storeService.Update(store.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = store.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(store);
        }

        // GET: Stores/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _storeService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Stores/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _storeService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
