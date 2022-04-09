namespace Coffee
{
    public class OptionStock : IOption
    {
        public void Execute(ref Machine.MachineState state)
        {
            Console.WriteLine(
              @"Current stocks:
               
                 Water: {0} ml
                 Milk: {1} ml
                 Beans: {2} g
                 Cups: {3}
                 Coins: {4}",
                state.water, state.milk, state.beans, state.cups, state.coins
            );
        }
    }
}