using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using System.Text;

// 값 형식 예제

// 할당

int a = 1;
int b = a;
a = 2;

Console.WriteLine($"a = {a}, b = {b}"); // a = 2, b = 1

// 메서드 인수 전달

ChangeInt(b);

void ChangeInt(int val)
{
    val = 500;
    Console.WriteLine($"val = {val}");
}

Console.WriteLine($"b = {b}");

// 메서드 리턴

Console.WriteLine($"a = {ChangeAndReturnInt(a)}");

Console.WriteLine($"a = {a}");

int ChangeAndReturnInt(int val)
{
    val = 100;
    return val;
}

// 정수 100을 초기화

int binaryInt = 0b_0110_0100;
int binaryInt2 = 0b01100100;

int decimalInt = 100_00;

int hexInt = 0x64;

var int1 = 9_999;    // int
var int2 = 2_999_999_999;      // uint
var int3 = 9_999_999_999;      // long
var int4 = 9_999_999_999_999_999_999;       // ulong

short short1 = 30000;
//short short2 = 99999;   // CS0031: '99999' 상수 값을 'short`(으)로 변환할 수 없습니다.

var byte1 = (byte)15;
var long1 = (uint)15;

var floatValue = 0.1F;

var doubleValue = 0.1;
var doubleValue2 = 0.1D;

var decimalValue = 0.1M;

double castingDouble = 1.6;
int castingInt = (int)castingDouble;    // castingInt = 1

double castingDouble2 = 1.5;
int castingInt2 = (int)Math.Round(castingDouble2);    // castingInt2 = 2

Console.WriteLine(castingInt);
Console.WriteLine(castingInt2);

double X = 1.0;
decimal Y = 1M;

double Z = X + (double)Y;
decimal W = (decimal)X + Y;

Console.WriteLine(Z);
Console.WriteLine(W);

//const int X = 500;
//float Y = X;
//byte Z = X;     // CS0031: '500' 상수 값을 'byte`(으)로 변환할 수 없습니다.

bool XX = false;
string convertString = XX.ToString();
int convertInt = Convert.ToInt32(XX);

char AA = 'A';
char BB = '\uAC01';
BB = (char)(BB - (char)2);

BB = (char)(AA + BB);

double XXXX = BB;

char C = '\uAC00';       // 80

int AC = C;
double BC = C;
short SC = (short)C;

test BBBA = default;
int testEnum = 0;
test parsed = (test)Enum.Parse(typeof(test), testEnum.ToString());
Console.WriteLine(BBBA.ToString());

int sadasd = 10;
StatusCode code = (StatusCode)13;  // 13
code = Enum.Parse<StatusCode>("Warning");      // 키가 정의되어 있지 않은 경우 예외가 발생한다. 여기서는 방법만 소개한다.
bool isSuccess = Enum.TryParse<StatusCode>("Warning", out StatusCode code2);
bool isDefined = Enum.IsDefined(typeof(StatusCode), "Hello");
Console.WriteLine($"{code}, {(int)code}");
code = StatusCode.Run;
code = StatusCode.Error;

bool boolean = Convert.ToBoolean("TrUe");
var bt = Convert.ToBoolean(-4.156);

testStruct st = new testStruct(10, "ads");
st.a = 1;
Console.WriteLine($"{st.a}, {st.b}");

//var coordinate = new CartesianCoordinate();
//Console.WriteLine($"{coordinate.X}, {coordinate.Y}");
//var coordinate1 = coordinate with { X = 5 };
//Console.WriteLine($"{coordinate1.X}, {coordinate1.Y}");
//var coordinate2 = default(CartesianCoordinate);
//Console.WriteLine($"{coordinate2.X}, {coordinate2.Y}");

int? A = 1;
int B = (int)A;

A = null;

object AAA = 1;
object BBB = "A";
object CCC = new MyClass(10);
Console.WriteLine(CCC.Equals(new MyClass(10)));   // True
Console.WriteLine(CCC.Equals(10));   // False

object DDD = CCC;
Console.WriteLine(Object.ReferenceEquals(CCC, DDD));

// String literal

string string1 = "Sample string";
string string2 = @"Sample Path : C:\Temp\SampleText.txt";
string string3 = "Sample Path : C:\\Temp\\SampleText.txt";

// From char

char[] chars = { 'A', 'B', 'C' };
string string4 = new string(chars);

// Repeated string

string string5 = new string('A', 5);

// From byte

byte[] bytes = { 0x41, 0x42, 0x43 };    // { A, B, C }
string string6 = Encoding.Default.GetString(bytes);

// Raw string literal

string string7 = """This is "Raw string literal".""";
string jsonString = """
    {
        "SampleValue": 1
    }
    """;
Console.WriteLine(string7);
Console.WriteLine(jsonString);

string stringCheck1 = null;
string stringCheck2 = string.Empty;
string stringCheck3 = " ";
string stringCheck4 = "ABC";

// 1. string.IsNullOrEmpty(string)

Console.WriteLine(string.IsNullOrEmpty(stringCheck1));          // True
Console.WriteLine(string.IsNullOrEmpty(stringCheck2));          // True
Console.WriteLine(string.IsNullOrEmpty(stringCheck3));          // False
Console.WriteLine(string.IsNullOrEmpty(stringCheck4));          // False

// 2. string.IsNullOrWhiteSpace(string)

Console.WriteLine(string.IsNullOrWhiteSpace(stringCheck1));     // True
Console.WriteLine(string.IsNullOrWhiteSpace(stringCheck2));     // True
Console.WriteLine(string.IsNullOrWhiteSpace(stringCheck3));     // True
Console.WriteLine(string.IsNullOrWhiteSpace(stringCheck4));     // False

MyClass classTest = new MyClass(10);
var AAAA = classTest;
var BBBB = classTest;

Console.WriteLine($"{AAAA.X}, {BBBB.X}");       // 10, 10

classTest.X = 20;

Console.WriteLine($"{AAAA.X}, {BBBB.X}");       // 20, 20

//// Without dynamic

//object providerObj = container.GetDataProvider();
//Type hostType = providerObj.GetType();
//object dataObj = hostType.InvokeMember(
//    "GetData",
//    BindingFlags.InvokeMethod,
//    null,
//    providerObj,
//    null
//);
//int data = Convert.ToInt32(dataObj);

//// With dynamic

//dynamic host = container.GetDataProvider();
//int data = host.GetData();

CartesianCoordinate coordinate = new(1, 2);

// record 출력

Console.WriteLine(coordinate.X);    // 1
Console.WriteLine(coordinate.Y);    // 2
Console.WriteLine(coordinate);      // CartesianCoordinate { X = 1, Y = 2 }

var (X11, Y11) = coordinate;

Console.WriteLine($"{X11},{Y11}");

//CartesianCoordinate coordinate1 = coordinate with { X = 3 };
//Console.WriteLine(coordinate1);

//coordinate1 = coordinate with { };

//Console.WriteLine(coordinate == coordinate1);   // True
//Console.WriteLine(object.ReferenceEquals(coordinate, coordinate1));     // False

var phoneNumbers = new string[2];
Person person1 = new("Nancy", "Davolio", phoneNumbers);
Person person2 = new("Nancy", "Davolio", phoneNumbers);
Console.WriteLine(person1 == person2); // output: True

Console.WriteLine(person1.PhoneNumbers[0]);
Console.WriteLine(person2.PhoneNumbers[0]);
person1.PhoneNumbers[0] = "555-1234";
Console.WriteLine(person1 == person2); // output: True
Console.WriteLine(person1.PhoneNumbers[0]);
Console.WriteLine(person2.PhoneNumbers[0]);

Console.WriteLine(ReferenceEquals(person1, person2)); // output: False

CargoList cargo = new("My Container", new List<string>() { "Stone", "Fish" });

Console.WriteLine(cargo);               // CargoList { ContainerName = My Container, Items = System.Collections.Generic.List`1[System.String] }
Console.WriteLine(cargo.Items[0]);     // Stone

cargo.Items[0] = "TV";

Console.WriteLine(cargo.Items[0]);      // TV

public record Person(string FirstName, string LastName, string[] PhoneNumbers);

public record CargoList(string ContainerName, List<string> Items);

public class CartesianCoordinate
{
    public double X { get; init; }
    public double Y { get; init; }

    public CartesianCoordinate(double x, double y)
    {
        X = x;
        Y = y;
    }

    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(nameof(CartesianCoordinate));
        stringBuilder.Append(" { ");

        if (PrintMembers(stringBuilder))
        {
            stringBuilder.Append(" ");
        }

        stringBuilder.Append("}");

        return stringBuilder.ToString();
    }

    protected virtual bool PrintMembers(StringBuilder stringBuilder)
    {
        stringBuilder.Append($"X = {X}, ");
        stringBuilder.Append($"Y = {Y}");
        return true;
    }
}

internal class MyClass
{
    public int X { get; set; }

    public MyClass(int x) => X = x;

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        return obj is MyClass && X == ((MyClass)obj).X;
    }

    public override string ToString() => X.ToString();

    public override int GetHashCode() => X.GetHashCode();
}

internal enum test
{
    a = 1,
    b = 2,
    c = 3,
}

[Flags]
internal enum StatusCode
{
    None = 0,       // 0
    Idle = 0b1,         // 1
    Run = 2,        // 2
    Warning = 4,   // 4
    Error = 0b_1000        // 8
}

internal struct testStruct
{
    public int a;
    public readonly string b;

    public testStruct(int a, string b)
    {
        this.a = a; this.b = b;
    }
}

//public record struct CartesianCoordinate
//{
//    public float X { get; init; }
//    public float Y { get; init; }

//    public CartesianCoordinate(float X, float Y)
//    {
//        this.X = X;
//        this.Y = Y;
//    }
//}