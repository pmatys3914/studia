namespace Coffee
{
    // Machine option interface
    public interface IOption
    {
        // Execute the option with the state of the machine
        public void Execute(Machine.MachineState state);
    }
}