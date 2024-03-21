## Introduction

<br>

- 사용자 정의 형식은 C# 연산자를 오버로드 할 수 있다.
- `operator` 키워드와 함께 연산자를 정의할 수 있으며, 아래의 규칙을 지켜야한다.
    1. `public static`으로 선언한다.
    2. 매개 변수의 경우 다음 규칙을 따른다.
        - 단항 연산자 : 1개
        - 이항 연산자 : 2개
    3. 하나 이상의 매개 변수의 형식이 사용자 정의 형식 `T` 또는 `T?`이어야 한다.

<br>

## Example

<br>

- 다음은 연산자 오버로드의 간단한 예시를 보여준다.
    ```cs
    public struct Int
    {
        private readonly int _int;

        public Int(int value) => _int = value;

        public override string ToString() => _int.ToString();

        public static Int operator +(Int a) => a;    
        public static Int operator -(Int a) => new(-a._int);

        public static Int operator +(Int a, Int b) => new(a._int + b._int);
        public static Int operator -(Int a, Int b) => new(a._int - b._int);
        public static Int operator *(Int a, Int b) => new(a._int * b._int);
        public static Int operator /(Int a, Int b) => new(a._int / b._int);
    }
    ```
    ```cs
    var foo = new Int(1);
    var bar = new Int(2);

    Console.WriteLine(+foo);
    Console.WriteLine(-foo);

    Console.WriteLine(foo + bar);
    Console.WriteLine(foo - bar);
    Console.WriteLine(foo * bar);
    Console.WriteLine(foo / bar);

    /* output:
    1
    -1
    3
    -1
    2
    0
    */
    ```

<br>

## 오버로드 할 수 있는 연산자

<br>

|연산자|비고|
|-----|----|
|`+`, `-`, `*`, `/`, `%`, `++`, `--`||
|`true`, `false`|함께 정의되어야 함|
|`!`, `~`, `&`, `\|`, `^`||
|`==`, `!=`|함께 정의되어야 함|
|`<`, `>`|함께 정의되어야 함|
|`<=`, `>=`|함께 정의되어야 함|
|`<<`, `>>`, `>>>`||

<br>

## 오버로드 할 수 없는 연산자

<br>

|연산자|비고|
|-----|----|
|`&&`, `\|\|`|`true`, `false`, `&`, `\|` 정의에 따라 동작|
|`[]`, `?[]`|[인덱서](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/indexers/) 정의에 따라 동작|
|`(T)`|[사용자 정의 변환]() 정의에 따라 동작|
|`+=`, `-=`, `*=`, `/=`, `%=`, `&=`, `\|=`, `^=`, `<<=`, `>>=`, `>>>=`|해당 이진 연산자의 정의에 따라 동작|
|`=`||
|`^`, `..`||
|`.`, `.?`||
|`a ? b : c`, `??`, `??=`||
|`->`||
|`=>`||
|`f(x)`||
|`delegate`||
|`as`||
|`await`||
|`checked`, `unchecked`||
|`new`, `default`||
|`is`||
|`nameof`, `sizeof`, `typeof`||
|`switch`||
|`with`||
|`stackalloc`||

<br>

## 참조 자료

<br>

- [연산자 오버로딩 - 미리 정의된 단항, 산술, 항등 및 비교 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/operator-overloading)