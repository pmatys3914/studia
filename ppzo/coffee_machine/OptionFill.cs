namespace Coffee
{
    public class OptionFill : IOption
    {
        public void Execute(ref Machine.MachineState state)
        {
            while(true)
            {
            Console.WriteLine(
                  @"Pick ingredient to refill:
                
                     [1] Water
                     [2] Milk
                     [3] Beans
                     [4] Cups
                     [0] Back"
                );
                int action = Int32.Parse(Console.ReadLine());
                if (action == 0)
                {
                    break;
                }
                if (action < 0 || action > 4)
                {
                    Console.WriteLine("Invalid option");
                    continue;
                }
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Amount to add [ml]:");
                        state.water += UInt32.Parse(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine("Amount to add [ml]:");
                        state.milk += UInt32.Parse(Console.ReadLine());
                        break;
                    case 3:
                        Console.WriteLine("Amount to add [g]:");
                        state.beans += UInt32.Parse(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Amount to add [cups]:");
                        state.cups += UInt32.Parse(Console.ReadLine());
                        break;
                }
            }
        }
    }
}