namespace Coffee
{
    class OptionCashOut : IOption
    {
        public void Execute(ref Machine.MachineState state)
        {
            Console.WriteLine("Cashed out {0} coins", state.coins);
            state.coins = 0;
        }
    }
}