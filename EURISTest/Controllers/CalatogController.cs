using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;

namespace EURISTest.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogManager catalogManager;

        /// <summary>
        /// Costructor
        /// </summary>
        public CatalogController()
        {
            catalogManager = new CatalogManager();
        }

        /// <summary>
        /// Catalog index page
        /// </summary>
        /// <returns>Catalog index page</returns>
        public ActionResult Index()
        {
            List<Catalog> catalogs = catalogManager.GetCatalogs();

            return View(catalogs);
        }

        /// <summary>
        /// Create catalog
        /// </summary>
        /// <returns>Create catalog view</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Catalog());
        }

        /// <summary>
        /// Create catalog form post
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns>View with error message if not succeded</returns>
        [HttpPost]
        public ActionResult Create(Catalog catalog)
        {
            if (!ModelState.IsValid) return View(catalog);

            var result = catalogManager.CreateCatalog(catalog);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(catalog);
            }
        }

        /// <summary>
        /// Displays catalog info
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Catalog info view</returns>
        [HttpGet]
        public ActionResult Info(int id)
        {
            var catalog = catalogManager.FindCatalog(id);

            if (catalog == null)
                return RedirectToAction("Index");

            return View(catalog);
        }

        /// <summary>
        /// Edit catalog
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Catalog edit view</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var catalog = catalogManager.FindCatalog(id);

            if (catalog == null)
                return RedirectToAction("Index");

            return View(catalog);
        }

        /// <summary>
        /// Updates catalog values
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns>Catalog edit view on error</returns>
        [HttpPost]
        public ActionResult Edit(Catalog catalog)
        {
            var result = catalogManager.UpdateCatalog(catalog);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(catalog);
            }
        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Catalog delete confirmation view</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var catalog = catalogManager.FindCatalog(id);

            if (catalog == null)
                return RedirectToAction("Index");

            return View(catalog);
        }

        /// <summary>
        /// Deletes catalog
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Catalog delete view on error</returns>
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var catalog = catalogManager.FindCatalog(id);

            if (catalog == null)
                return View();

            var result = catalogManager.DeleteCatalog(catalog);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(catalog);
            }
        }
    }
}
