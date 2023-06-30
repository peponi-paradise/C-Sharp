using System.Numerics;

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