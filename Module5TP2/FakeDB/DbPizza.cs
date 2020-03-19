using Module5TP2BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module5TP2.FakeDB
{
    public static class DbPizza
    {
        public static List<Pizza> pizzas = new List<Pizza>
        {
            new Pizza{Id=1, Nom="Reine", Pate=Pizza.PatesDisponibles[0]
                ,Ingredients=new List<Ingredient>{
                    Pizza.IngredientsDisponibles[0],
                    Pizza.IngredientsDisponibles[1],
                    Pizza.IngredientsDisponibles[2],
                }
            },
            new Pizza{Id=2, Nom="Random", Pate=Pizza.PatesDisponibles[2]
                ,Ingredients=new List<Ingredient>{
                    Pizza.IngredientsDisponibles[3],
                    Pizza.IngredientsDisponibles[4],
                    Pizza.IngredientsDisponibles[5],
                }
            },
            new Pizza{Id=3, Nom="Fromageuse", Pate=Pizza.PatesDisponibles[3]
                ,Ingredients=new List<Ingredient>{
                    Pizza.IngredientsDisponibles[0],
                    Pizza.IngredientsDisponibles[4],
                    Pizza.IngredientsDisponibles[2],
                }
            },
            new Pizza{Id=4, Nom="Shnafou", Pate=Pizza.PatesDisponibles[1]
                ,Ingredients=new List<Ingredient>{
                    Pizza.IngredientsDisponibles[5],
                    Pizza.IngredientsDisponibles[7],
                    Pizza.IngredientsDisponibles[1],
                    Pizza.IngredientsDisponibles[0],
                }
            },
            new Pizza{Id=5, Nom="Avarié", Pate=Pizza.PatesDisponibles[2]
                ,Ingredients=new List<Ingredient>{
                    Pizza.IngredientsDisponibles[7],
                    Pizza.IngredientsDisponibles[5],
                    Pizza.IngredientsDisponibles[6],
                    Pizza.IngredientsDisponibles[3],
                }
            }
        };
    }
}