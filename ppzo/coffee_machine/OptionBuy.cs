namespace Coffee
{
    public class OptionBuy : IOption
    {
        private struct Recipe {
            public Recipe(string name, uint water, uint milk, uint beans, uint price)
            {
                this.name = name;
                this.water = water;
                this.milk = milk;
                this.beans = beans;
                this.price = price;
            }
            public string name;
            public uint water;
            public uint milk;
            public uint beans;
            public uint price;
        }

        private Recipe[] recipes = {
            new Recipe("Latte", 350, 75, 20, 7),
            new Recipe("Cappuccino", 200, 100, 12, 6),
            new Recipe("Espresso", 250, 0, 16, 4)
        };

        public void Execute(ref Machine.MachineState state)
        {
            Console.WriteLine("Pick a coffee:\n");
            int i = 1;
            foreach (Recipe r in recipes)
            {
                Console.WriteLine($"[{i++}] {r.name} - {r.price} coins");
            }
            Console.WriteLine("[0] Back");
            while(true)
            {
                int action = Int32.Parse(Console.ReadLine());
                if (action == 0)
                {
                    break;
                }
                if (action < 0 || action > recipes.Length)
                {
                    Console.WriteLine("Invalid option");
                    continue;
                }
                Recipe r = recipes[action - 1];
                if (state.water < r.water || state.milk < r.milk || state.beans < r.beans)
                {
                    Console.WriteLine("Not enough ingredients");
                    continue;
                }
                state.water -= r.water;
                state.milk -= r.milk;
                state.beans -= r.beans;
                state.cups--;
                state.coins += r.price;
                Console.WriteLine($"You have bought {r.name}");
                break;
            }
        }
    }
}