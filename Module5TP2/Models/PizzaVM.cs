using Module5TP2BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module5TP2.Models
{
    public class PizzaVM
    {
        private Pizza pizza;
        private List<Ingredient> allIngredient;
        private List<Pate> allPate;

        public PizzaVM()
        {
            this.AllIngredient = Pizza.IngredientsDisponibles;
            this.AllPate = Pizza.PatesDisponibles;
        }

        public PizzaVM(Pizza pizza)
        {
            this.Pizza = pizza;
            this.AllIngredient = Pizza.IngredientsDisponibles;
            this.AllPate = Pizza.PatesDisponibles;
        }

        public Pizza Pizza { get => pizza; set => pizza = value; }
        public List<Ingredient> AllIngredient { get => allIngredient; set => allIngredient = value; }
        public List<Pate> AllPate { get => allPate; set => allPate = value; }
    }
}