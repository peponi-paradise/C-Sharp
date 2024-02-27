using System.Drawing;
using System.Runtime.InteropServices;

namespace MemoryLayoutRule
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Test
    {
        [FieldOffset(10)]
        public byte A;

        [FieldOffset(8)]
        public short B;

        [FieldOffset(0)]
        public double C;
    }

    public struct Test2
    {
        public short A;
        public short B;
        public int C;
        public string D;
    }

    public class Test3
    {
        public bool A;
    }

    internal class Program
    {
        unsafe static void Main(string[] args)
        {
            Test t = new();
            var addr = (byte*)&t;

            Console.WriteLine(Marshal.SizeOf<Test>());

            Console.WriteLine($"Offset : {(byte*)&t.A - addr}");
            Console.WriteLine($"Offset : {(byte*)&t.B - addr}");
            Console.WriteLine($"Offset : {(byte*)&t.C - addr}");

            Console.WriteLine($"Size : {Marshal.SizeOf(t.A)}");
            Console.WriteLine($"Size : {Marshal.SizeOf(t.B)}");
            Console.WriteLine($"Size : {Marshal.SizeOf(t.C)}");

            Console.WriteLine(Marshal.SizeOf<Test2>());

            Test3 t3 = new Test3();
            Console.WriteLine(Marshal.SizeOf(t3.A));
        }
    }
}