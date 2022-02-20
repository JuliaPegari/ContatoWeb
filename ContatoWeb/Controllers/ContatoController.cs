using ContatoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContatoWeb.Controllers
{
    public class ContatoController : Controller
    {
        private ContatoRepository repository = new ContatoRepository();

        // GET: Contato
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contato contato)
        {
            repository.Save(contato);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var contato = repository.GetById(id);

            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        [HttpPost]
        public ActionResult Edit(Contato contato)
        {
            repository.Update(contato);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var contato = repository.GetById(id);

            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        [HttpPost]
        public ActionResult Delete(Contato contato)
        {
            repository.Delete(contato);
            return RedirectToAction("Index");
        }
    }
}