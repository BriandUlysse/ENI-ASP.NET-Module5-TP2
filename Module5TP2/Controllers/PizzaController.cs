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
            PizzaVM pizzaVm = new PizzaVM();
            pizzaVm.AllIngredient = Pizza.IngredientsDisponibles.Select(p => new SelectListItem { 
                Text = p.Nom, Value = p.Id.ToString() }).ToList();
            pizzaVm.AllPate = Pizza.PatesDisponibles.Select(p => new SelectListItem
            {
                Text = p.Nom,
                Value = p.Id.ToString()
            }).ToList();
            return View(pizzaVm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM pizzaVm)
        {
            try
            {
                Pizza pizza = pizzaVm.Pizza;

                // Création des listes dans l'objet pizza résultat
                pizza.Pate = Pizza.PatesDisponibles.FirstOrDefault(x => x.Id == pizzaVm.IdPate);

                pizza.Ingredients = Pizza.IngredientsDisponibles.Where(
                    x => pizzaVm.IdsIngredients.Contains(x.Id))
                    .ToList();

                pizza.Id = pizzas.Count == 0 ? 1 : pizzas.Max(x => x.Id) + 1;

                pizzas.Add(pizza);

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
            PizzaVM vm = new PizzaVM();

            vm.AllPate = Pizza.PatesDisponibles.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.AllIngredient = Pizza.IngredientsDisponibles.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Pizza = pizzas.FirstOrDefault(p => p.Id == id);


            // Ajout pate/Ingrédients
            if (vm.Pizza.Pate != null)
            {
                vm.IdPate = vm.Pizza.Pate.Id;
            }

            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdsIngredients = vm.Pizza.Ingredients.Select(x => x.Id).ToList();
            }

            return View(vm);
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
                    pizzaDb.Pate = Pizza.PatesDisponibles.FirstOrDefault(x => x.Id == pizzaVm.IdPate);
                    pizzaDb.Ingredients = Pizza.IngredientsDisponibles.Where(x => pizzaVm.IdsIngredients.Contains(x.Id)).ToList();

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
