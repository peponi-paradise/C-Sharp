namespace LogBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Log.LogStart();     // Log thread start. Create log path & check old logs.

            Log.Log.WriteLog(Log.Log.LogType.Application, "Log message");       // Write log
        }
    }
}