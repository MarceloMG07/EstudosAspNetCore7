using DemoViews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoViews.Controllers
{
    public class FilmeController : Controller
    {
        // GET: FilmeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FilmeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FilmeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilmeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Filme filme)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
                //return RedirectToAction(nameof(Index));
                return View(filme);
            }
            catch
            {
                return View();
            }
        }

        // GET: FilmeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilmeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilmeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilmeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
