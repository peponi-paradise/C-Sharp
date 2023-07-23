namespace ReadWriteRegistry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Write registry
            Registry.Registry.AppendRegistry("KeyNameToCreate", "ValueToWrite");

            // Read registry
            bool isSuccess = Registry.Registry.GetRegistry("KeyNameToCreate", out string readValue);
        }
    }
}
