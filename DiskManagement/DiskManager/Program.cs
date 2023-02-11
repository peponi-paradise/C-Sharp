namespace DiskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DiskManager.StartManager(20);   // Start disk manager
            DiskManager.StopManager();   // Stop disk manager
        }
    }
}