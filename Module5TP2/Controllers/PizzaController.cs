using Module5TP2.FakeDB;
using Module5TP2.Models;
using Module5TP2BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2.Controllers
{
    public class PizzaController : Controller
    {
        private static List<Pizza> pizzas = DbPizza.pizzas;
        // GET: Pizza
        public ActionResult Index()
        {
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizz = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizz != null)
            {
                return View(pizz);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            return View(new PizzaVM(new Pizza { Id = pizzas.Count}));
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM pizzaVm)
        {
            try
            {
                pizzas.Add(pizzaVm.Pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza pizz = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizz != null)
            {
                PizzaVM pizzaVM = new PizzaVM(pizz);
                return View(pizzaVM);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaVM pizzaVm)
        {
            try
            {
                Pizza pizzaDb = pizzas.FirstOrDefault(p => p.Id == pizzaVm.Pizza.Id);
                if (pizzaDb != null)
                {
                    pizzaDb.Nom = pizzaVm.Pizza.Nom;
                    pizzaDb.Ingredients = pizzaVm.Pizza.Ingredients;
                    pizzaDb.Pate = pizzaVm.Pizza.Pate;

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza pizz = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizz != null)
            {
                return View(pizz);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizz = pizzas.FirstOrDefault(p => p.Id == id);
                if (pizz != null)
                {
                    pizzas.Remove(pizz);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
