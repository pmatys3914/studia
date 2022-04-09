namespace Coffee
{
    public class Machine
    {
        // Struct holding all ingredient data
        public struct MachineState
        {
            public MachineState(uint water = 2000, uint milk = 500, uint beans = 250, uint cups = 40)
            {
                this.water = water;
                this.milk = milk;
                this.beans = beans;
                this.cups = cups;
                this.coins = 0;
            }
            public uint water;
            public uint milk;
            public uint beans;
            public uint cups;
            public uint coins;
        }
        private MachineState state = new MachineState();

        // Current option
        private IOption? option = null;

        // Constructor
        public Machine()
        {
            Loop();
        }

        // Main loop
        private void Loop()
        {
            while (true)
            {
                Console.WriteLine(
                  @"Coffee Machine
                  
                    [1] Buy
                    [2] Fill
                    [3] Stock
                    [4] Cash Out
                    [0] Quit"
                );
                int action = Int32.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        option = new OptionBuy();
                        option.Execute(state);
                        break;
                    case 2:
                        //option = new OptionFill();
                        option.Execute(state);
                        break;
                    case 3:
                        //option = new OptionStock();
                        option.Execute(state);
                        break;
                    case 4:
                        //option = new OptionCashOut();
                        option.Execute(state);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Unknown action");
                        break;
                }
            }
        }
    }
}