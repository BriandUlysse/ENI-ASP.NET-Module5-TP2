using Module5TP2BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2.Models
{
    public class PizzaVM
    {
        private Pizza pizza;
        private List<SelectListItem> allIngredient;
        private List<SelectListItem> allPate;

        public Pizza Pizza { get => pizza; set => pizza = value; }
        public List<SelectListItem> AllIngredient { get => allIngredient; set => allIngredient = value; }
        public List<SelectListItem> AllPate { get => allPate; set => allPate = value; }

        public List<int> IdsIngredients { get; set; } = new List<int>();

        public int IdPate { get; set; }
    }
}