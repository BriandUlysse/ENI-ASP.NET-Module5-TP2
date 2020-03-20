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
            initListVm(pizzaVm);
            return View(pizzaVm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM pizzaVm)
        {
            try
            {
                if (ModelState.IsValid && validatePizza(ModelState, pizzaVm))
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
                initListVm(pizzaVm);
                return View(pizzaVm);
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

            vm.Pizza = pizzas.FirstOrDefault(p => p.Id == id);

            initListVm(vm);

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
                if (ModelState.IsValid && validatePizza(ModelState, pizzaVm))
                {
                    Pizza pizzaDb = pizzas.FirstOrDefault(p => p.Id == pizzaVm.Pizza.Id);

                    pizzaDb.Nom = pizzaVm.Pizza.Nom;
                    pizzaDb.Pate = Pizza.PatesDisponibles.FirstOrDefault(x => x.Id == pizzaVm.IdPate);
                    pizzaDb.Ingredients = Pizza.IngredientsDisponibles.Where(x => pizzaVm.IdsIngredients.Contains(x.Id)).ToList();

                    return RedirectToAction("Index");
                }
                initListVm(pizzaVm);
                return View(pizzaVm);
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

        private bool validatePizza(ModelStateDictionary modelState, PizzaVM pizzaVm)
        {
            //On teste le nombre d'ingrédients :
            int nbIngredient = pizzaVm.IdsIngredients.Count();
            if (nbIngredient <= 2 || nbIngredient > 5)
            {
                ModelState.AddModelError("", "Il doit y avoir entre 2 et 5 ingrédients");
                return false;
            }

            //On teste que le nom est unique
            IEnumerable<Pizza> otherPizza = pizzas.Where(p => p.Id != pizzaVm.Pizza.Id);
            if (otherPizza.Select(p => p.Nom).Contains(pizzaVm.Pizza.Nom))
            {
                ModelState.AddModelError("", "Le nom de la pizza est déjà pris");
                return false;
            }

            // On vérifie que la liste d'ingrédient n'est pas identique à une autre
            List<int> listIdPizzaVM = pizzaVm.IdsIngredients;
            foreach (Pizza pizza in pizzas)
            {
                if (pizza.Id != pizzaVm.Pizza.Id)
                {
                    IEnumerable<int> idsPizzaToTest = pizza.Ingredients.Select(p => p.Id);

                    if (listIdPizzaVM.Count() == idsPizzaToTest.Count() && idsPizzaToTest.Intersect(listIdPizzaVM).Count()== idsPizzaToTest.Count())
                    {
                        ModelState.AddModelError("", "Une autre pizza possède déjà ses ingrédients");
                        return false;
                    }
                }

            }

            return true;
        }

        private void initListVm(PizzaVM pizzaVM)
        {

            pizzaVM.AllPate = Pizza.PatesDisponibles.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            pizzaVM.AllIngredient = Pizza.IngredientsDisponibles.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

        }
    }
}
