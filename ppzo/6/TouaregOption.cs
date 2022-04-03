namespace Tea
{
    // Option making and evaluating the special Touareg tea
    class TouaregOption : IOption
    {
        private string pathInput = "touareg-input.txt";
        private string pathOutput = "result-5.txt"; 
        // Ingredient with its subtrate
        private List<Tuple<string, string>> ingredients = new List<Tuple<string, string>> { new Tuple<string, string>("Mięta", "woda"), new Tuple<string, string>("Gunpowder Zielony", "Mięta")};



        private struct TeaRecipe
        {
            public TeaRecipe(string name, string substrate, uint temp, uint time)
            {
                Name = name;
                Substrate = substrate;
                Temp = temp;
                Time = time;
            }
            public string Name;
            public string Substrate;
            public uint Temp;
            public uint Time;
        }

        private List<TeaRecipe> recipes = new List<TeaRecipe>();


        // Loads tea recipes from input file
        private void LoadRecipes()
        {
            // Check if the input file exists, return otherwise
            if (!File.Exists(pathInput))
            {
                Console.Write("Touareg file missing!");
                return;
            }
            // Read the list with a stream reader
            using (StreamReader sr = File.OpenText(pathInput))
            {
                string? s;
                while ((s = sr.ReadLine()) != null)
                {
                    // If the line is not empty and doesn't begin with #, parse and add to the list
                    if (s != "" && s[0] != '#')
                    {
                        string[] recipe = s.Split(", ");
                        recipes.Add(new TeaRecipe(recipe[0], recipe[1], uint.Parse(recipe[2]), uint.Parse(recipe[3])));
                    }
                }
            }
        }


        // Calculate and return the quality of finished product. If any of the recipes is not ideal, the product is not ideal.
        public void SaveOutput(List<Tea> teas)
        {
            using(StreamWriter sw = File.CreateText(pathOutput))
            {
                // Name of the last ingredient used
                string lastIngredient = "woda";
                foreach(TeaRecipe r in recipes)
                {
                    // Check if the ingredient exists and belongs to the tea
                    if(!teas.Exists(t => t.Name == r.Name) || !ingredients.Exists(n => n.Item1 == r.Name))
                    {
                        sw.WriteLine("Invalid ingredient {0}" , r.Name);
                        return;
                    }
                    // Check if the last ingredient is correct
                    if(ingredients.Find(n => n.Item1 == r.Name).Item2 != lastIngredient)
                    {
                        sw.WriteLine("Invalid {0} substrate", r.Name);
                        return;
                    }
                    Tea tea = teas.Find(t => t.Name == r.Name);
                    // Check if the recipe is ideal
                    if((r.Temp > tea.Temp * 1.1f || r.Time > tea.Time * 1.1f * 60) || (r.Temp < tea.Temp * 0.9f || r.Time < tea.Time * 0.9f * 60))
                    {
                        sw.WriteLine("Touareg ruined by {0}", r.Name);
                        return;
                    }
                    // Set the last ingredient to the current one
                    lastIngredient = r.Name;
                }
                // If nothing fails, return success
                sw.WriteLine("Touareg brewed successfully!");
            }
        }


        // Execute override
        public uint Execute(List<Tea> teas)
        {
            Console.WriteLine("Making a Touareg tea...");
            LoadRecipes();
            SaveOutput(teas);
            return 7;
        }
    }
}