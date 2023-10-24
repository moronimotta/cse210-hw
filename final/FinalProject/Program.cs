using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Please enter your age: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Please enter your height in inches: ");
        int heightInches = int.Parse(Console.ReadLine());
        Console.Write("Please enter your weight in pounds: ");
        int weightPounds = int.Parse(Console.ReadLine());
        UserProfile user = new UserProfile(name, age, heightInches, weightPounds);

        Console.WriteLine("Please choose your macro goals:");
        Console.WriteLine("1. Lose weight");
        Console.WriteLine("2. Maintain weight");
        Console.WriteLine("3. Gain weight");
        Console.Write("Please enter your choice: ");
        int goalChoice = int.Parse(Console.ReadLine());
        MacroGoals goals;
        switch (goalChoice)
        {
            case 1:
                goals = new MacroGoals(0.8f, 0.2f, 0.2f);
                break;
            case 2:
                goals = new MacroGoals(0.4f, 0.3f, 0.3f);
                break;
            case 3:
                goals = new MacroGoals(0.3f, 0.4f, 0.3f);
                break;
            default:
                Console.WriteLine("Invalid choice. Using default goals.");
                goals = new MacroGoals(0.4f, 0.3f, 0.3f);
                break;
        }
        user.SetMacroGoals(goals);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine($"Welcome, {user.Name}!");
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Add a meal");
            Console.WriteLine("2. View meal plan");
            Console.WriteLine("3. Save plan to file");
            Console.WriteLine("4. Edit a specific food for the meal");
            Console.WriteLine("5. Delete a specific meal");
            Console.WriteLine("6. Exit");
            Console.Write("Please enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Please enter the name of the meal: ");
                    string mealName = Console.ReadLine();
                    Meal meal = new Meal(mealName);
                    bool addFood = true;
                    while (addFood)
                    {
                        Console.Write("Please enter the name of the food: ");
                        string foodName = Console.ReadLine();
                        Console.Write("Please enter the quantity of the food: ");
                        int foodQuantity = int.Parse(Console.ReadLine());
                        Console.Write("Please enter the macros of the food (in the format 'Protein/Carbs/Fat'): ");
                        string foodMacros = Console.ReadLine();
                        Food food = new Food(foodName, foodQuantity, foodMacros);
                        meal.AddFood(food);
                        string[] macros = foodMacros.Split('/');
                        int proteinGrams = int.Parse(macros[0]) * foodQuantity;
                        int carbsGrams = int.Parse(macros[1]) * foodQuantity;
                        int fatGrams = int.Parse(macros[2]) * foodQuantity;
                        int totalProteinGrams = 0;
                        int totalCarbsGrams = 0;
                        int totalFatGrams = 0;
                        foreach (Meal m in user.Meals)
                        {
                            foreach (Food f in m.Foods)
                            {
                                string[] fMacros = f.Macros.Split('/');
                                totalProteinGrams += int.Parse(fMacros[0]) * f.Quantity;
                                totalCarbsGrams += int.Parse(fMacros[1]) * f.Quantity;
                                totalFatGrams += int.Parse(fMacros[2]) * f.Quantity;
                            }
                        }
                        totalProteinGrams += proteinGrams;
                        totalCarbsGrams += carbsGrams;
                        totalFatGrams += fatGrams;
                        int goalProteinGrams = (int)(user.Goals.ProteinRatio * user.Goals.TotalCalories / 4);
                        int goalCarbsGrams = (int)(user.Goals.CarbRatio * user.Goals.TotalCalories / 4);
                        int goalFatGrams = (int)(user.Goals.FatRatio * user.Goals.TotalCalories / 9);
                        if (totalProteinGrams > goalProteinGrams)
                        {
                            Console.WriteLine($"Warning: Your protein intake is higher than expected ({totalProteinGrams}g vs {goalProteinGrams}g).");
                        }
                        if (totalCarbsGrams > goalCarbsGrams)
                        {
                            Console.WriteLine($"Warning: Your carb intake is higher than expected ({totalCarbsGrams}g vs {goalCarbsGrams}g).");
                        }
                        if (totalFatGrams > goalFatGrams)
                        {
                            Console.WriteLine($"Warning: Your fat intake is higher than expected ({totalFatGrams}g vs {goalFatGrams}g).");
                        }
                        Console.Write("Add another food? (y/n): ");
                        string addAnother = Console.ReadLine();
                        addFood = addAnother.ToLower() == "y";
                    }
                    user.AddMeal(meal);
                    Console.WriteLine($"Meal {mealName} added.");
                    Console.WriteLine($"Current macros: {user.GetCurrentMacros()}");
                    Console.WriteLine($"Goal macros: {user.GetGoalMacros()}");
                    break;
                case "2":
                    Console.WriteLine("Meal Plan:");
                    foreach (Meal m in user.Meals)
                    {
                        Console.WriteLine(m.Name);
                        foreach (Food f in m.Foods)
                        {
                            Console.WriteLine($"- {f.Name}: {f.Quantity} {f.Macros}");
                        }
                    }
                    Console.WriteLine($"Total macros: {user.GetTotalMacros()}");
                    Console.WriteLine($"Goal macros: {user.GetGoalMacros()}");
                    break;
                case "3":
                    Console.Write("Please enter the filename to save the plan to: ");
                    string filename = Console.ReadLine();
                    using (StreamWriter writer = new StreamWriter(filename))
                    {
                        writer.WriteLine($"My goal: {user.GetGoalMacros()}");
                        foreach (Meal m in user.Meals)
                        {
                            writer.WriteLine(m.Name);
                            foreach (Food f in m.Foods)
                            {
                                writer.WriteLine($"- {f.Name}: {f.Quantity} {f.Macros}");
                            }
                        }
                    }
                    Console.WriteLine($"Plan saved to {filename}.");
                    break;
                case "4":
                    Console.Write("Please enter the name of the meal to edit: ");
                    string mealToEdit = Console.ReadLine();
                    Meal mealToEditObj = user.Meals.Find(m => m.Name == mealToEdit);
                    if (mealToEditObj == null)
                    {
                        Console.WriteLine($"Meal {mealToEdit} not found.");
                        break;
                    }
                    Console.Write("Please enter the name of the food to edit: ");
                    string foodToEdit = Console.ReadLine();
                    Food foodToEditObj = mealToEditObj.Foods.Find(f => f.Name == foodToEdit);
                    if (foodToEditObj == null)
                    {
                        Console.WriteLine($"Food {foodToEdit} not found in meal {mealToEdit}.");
                        break;
                    }
                    Console.Write("Please enter the new quantity of the food: ");
                    int newQuantity = int.Parse(Console.ReadLine());
                    foodToEditObj.Quantity = newQuantity;
                    Console.WriteLine($"Food {foodToEdit} in meal {mealToEdit} updated.");
                    Console.WriteLine($"Current macros: {user.GetCurrentMacros()}");
                    Console.WriteLine($"Goal macros: {user.GetGoalMacros()}");
                    break;
                case "5":
                    Console.Write("Please enter the name of the meal to delete: ");
                    string mealToDelete = Console.ReadLine();
                    Meal mealToDeleteObj = user.Meals.Find(m => m.Name == mealToDelete);
                    if (mealToDeleteObj == null)
                    {
                        Console.WriteLine($"Meal {mealToDelete} not found.");
                        break;
                    }
                    user.Meals.Remove(mealToDeleteObj);
                    Console.WriteLine($"Meal {mealToDelete} deleted.");
                    Console.WriteLine($"Current macros: {user.GetCurrentMacros()}");
                    Console.WriteLine($"Goal macros: {user.GetGoalMacros()}");
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}

abstract class MacroTracker
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int HeightInches { get; set; }
    public int WeightPounds { get; set; }
    public List<Meal> Meals { get; set; }
    public MacroGoals Goals { get; set; }

    public MacroTracker(string name, int age, int heightInches, int weightPounds)
    {
        Name = name;
        Age = age;
        HeightInches = heightInches;
        WeightPounds = weightPounds;
        Meals = new List<Meal>();
    }

    public void SetMacroGoals(MacroGoals goals)
    {
        Goals = goals;
    }

    public void AddMeal(Meal meal)
    {
        Meals.Add(meal);
    }

    public abstract string GetCurrentMacros();

    public abstract string GetGoalMacros();

    public abstract string GetTotalMacros();
}

class UserProfile : MacroTracker
{
    public UserProfile(string name, int age, int heightInches, int weightPounds) : base(name, age, heightInches, weightPounds)
    {
    }

    public override string GetCurrentMacros()
{
    float proteinGrams = 0f;
    float carbsGrams = 0f;
    float fatGrams = 0f;
    foreach (Meal m in Meals)
    {
        foreach (Food f in m.Foods)
        {
            string[] macros = f.Macros.Split('/');
            proteinGrams += float.Parse(macros[0]) * f.Quantity;
            carbsGrams += float.Parse(macros[1]) * f.Quantity;
            fatGrams += float.Parse(macros[2]) * f.Quantity;
        }
    }
    return $"Protein: {proteinGrams}g, Carbs: {carbsGrams}g, Fat: {fatGrams}g";
}

    public override string GetGoalMacros()
    {
        int totalCalories = Goals.TotalCalories;
        int proteinCalories = (int)(totalCalories * Goals.ProteinRatio);
        int carbsCalories = (int)(totalCalories * Goals.CarbRatio);
        int fatCalories = (int)(totalCalories * Goals.FatRatio);
        int proteinGrams = proteinCalories / 4;
        int carbsGrams = carbsCalories / 4;
        int fatGrams = fatCalories / 9;
        return $"Protein: {proteinGrams}g, Carbs: {carbsGrams}g, Fat: {fatGrams}g";
    }

    public override string GetTotalMacros()
    {
        float proteinGrams = 0;
        float carbsGrams = 0;
        float fatGrams = 0;
        foreach (Meal m in Meals)
        {
            foreach (Food f in m.Foods)
            {
                string[] macros = f.Macros.Split('/');
                proteinGrams += float.Parse(macros[0]) * f.Quantity;
                carbsGrams += float.Parse(macros[1]) * f.Quantity;
                fatGrams += float.Parse(macros[2]) * f.Quantity;
            }
        }
        return $"Protein: {proteinGrams}g, Carbs: {carbsGrams}g, Fat: {fatGrams}g";
    }
}

class Meal
{
    public string Name { get; set; }
    public List<Food> Foods { get; set; }

    public Meal(string name)
    {
        Name = name;
        Foods = new List<Food>();
    }

    public void AddFood(Food food)
    {
        Foods.Add(food);
    }
}

class Food
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Macros { get; set; }

    public Food(string name, int quantity, string macros)
    {
        Name = name;
        Quantity = quantity;
        Macros = macros;
    }
}

class MacroGoals
{
    public float ProteinRatio { get; set; }
    public float CarbRatio { get; set; }
    public float FatRatio { get; set; }
    public int TotalCalories { get; set; }

    public MacroGoals(float proteinRatio, float carbRatio, float fatRatio)
    {
        ProteinRatio = proteinRatio;
        CarbRatio = carbRatio;
        FatRatio = fatRatio;
        TotalCalories = 2000;
    }
}