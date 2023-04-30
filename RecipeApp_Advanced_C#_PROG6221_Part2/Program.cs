// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook
{
    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
        }

        public int GetTotalCalories()
        {
            return Ingredients.Sum(i => i.Calories);
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        public Ingredient(string name, int calories, string foodGroup)
        {
            Name = name;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }

    class Program
    {
        static List<Recipe> recipeBook = new List<Recipe>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("WHAT WOULD YOU LIKE TO DO ?");
                Console.WriteLine("1. Add a recipe");
                Console.WriteLine("2. View recipe list");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        ViewRecipeList();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddRecipe()
        {
            Console.WriteLine("Enter recipe name:");
            string name = Console.ReadLine();

            Recipe recipe = new Recipe(name);

            while (true)
            {
                Console.WriteLine("Enter ingredient name (or 'DONE' to finish adding ingredients):");
                string ingredientName = Console.ReadLine();

                if (ingredientName == "DONE")
                {
                    break;
                }

                Console.WriteLine("Enter number of calories:");
                int calories = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter food group:");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient(ingredientName, calories, foodGroup);
                recipe.Ingredients.Add(ingredient);
            }

            recipeBook.Add(recipe);

            Console.WriteLine("Recipe added!");
        }

        static void ViewRecipeList()
        {
            if (recipeBook.Count == 0)
            {
                Console.WriteLine("No recipes found.");
                Console.WriteLine("****************************************************************************");
                return;
            }

            Console.WriteLine("Recipe list:");
            recipeBook.OrderBy(r => r.Name).ToList().ForEach(r => Console.WriteLine(r.Name));

            Console.WriteLine("Enter recipe name to view details:");
            string name = Console.ReadLine();

            Recipe recipe = recipeBook.FirstOrDefault(r => r.Name == name);

            if (recipe == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            Console.WriteLine($"Ingredients for {recipe.Name}:");
            recipe.Ingredients.ForEach(i => Console.WriteLine($"{i.Name} ({i.FoodGroup}): {i.Calories} calories"));

            int totalCalories = recipe.GetTotalCalories();
            Console.WriteLine($"Total calories: {totalCalories}");

            if (totalCalories > 300)
            {
                Console.WriteLine("Warning: this recipe has over 300 calories!");
            }
        }
    }
}
