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
    val=500;
    Console.WriteLine($"val = {val}");
}

Console.WriteLine($"b = {b}");

// 메서드 리턴

Console.WriteLine($"a = {ChangeAndReturnInt(a)}");

Console.WriteLine($"a = {a}");

int ChangeAndReturnInt(int val) 
{
    val=100;
    return val;
}