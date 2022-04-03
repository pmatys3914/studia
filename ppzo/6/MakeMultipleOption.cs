namespace Tea
{
    class MakeMultipleOption : IOption
    {
        private string pathInput = "input-file.txt";
        private string pathOutput = "result-4.txt";
        private struct TeaRecipe
        {
            public TeaRecipe(string name, uint temp, uint time)
            {
                Name = name;
                Temp = temp;
                Time = time;
            }
            public string Name;
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
                Console.Write("Recipe file missing!");
                return;
            }
            // Read the list with a stream reader
            using (StreamReader sr = File.OpenText(pathInput))
            {
                string? s;
                while((s = sr.ReadLine()) != null)
                {
                    // If the line is not empty and doesn't begin with #, parse and add to the list
                    if(s != "" && s[0] != '#')
                    {
                        string[] recipe = s.Split(", ");
                        recipes.Add(new TeaRecipe(recipe[0], uint.Parse(recipe[1]), uint.Parse(recipe[2])));
                    }
                }
            }
        }

        
        // Calculates the quality of the teas based on the temperature and time and saves it to the output file
        private void SaveOutput(List<Tea> teas)
        {
            // Read the list with a stream reader
            using (StreamWriter sw = File.CreateText(pathOutput))
            {
                foreach(TeaRecipe recipe in recipes)
                {
                    // Check if the tea is in the database
                    if(!teas.Exists(t => recipe.Name == t.Name))
                    {
                        sw.WriteLine("{0}, Not in database!", recipe.Name);
                        continue;
                    }
                    Tea tea = teas.Find(t => recipe.Name == t.Name);
                    // Calculate and return the quality
                    if (recipe.Temp > tea.Temp * 1.1f || recipe.Time > tea.Time * 1.1f * 60)
                    {
                        sw.WriteLine(recipe.Name + ": yucky");
                    }
                    else if (recipe.Temp < tea.Temp * 0.9f || recipe.Time < tea.Time * 0.9f * 60)
                    {
                        sw.WriteLine(recipe.Name + ": weak");
                    }
                    else
                    {
                        sw.WriteLine(recipe.Name + ": perfect");
                    }
                }
            }
        }

        // Execute the code and save the teas to the output file
        public uint Execute(List<Tea> teas)
        {
            LoadRecipes();
            if(recipes.Count == 0)
            {
                Console.WriteLine("No recipes found!");
                return 6;
            }
            SaveOutput(teas);
            return 6;
        }
    }    
}