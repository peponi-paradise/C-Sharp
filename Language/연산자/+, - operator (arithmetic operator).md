## Introduction

<br>

- 산술 연산자 중 `+`, `-` 연산자는 [정수](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D), [부동 소수점](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type) 형식을 지원한다.
    - 정수 형식은 `int`, `uint`, `long`, `ulong` 형식으로 정의된다. (`++`, `--` 제외)
        - 피연산자가 다른 정수 형식인 (`int`보다 작은 데이터형) 경우 연산 결과는 `int` 형식으로 변환된다.
    - 피연산자가 정수 또는 부동 소수점 형식인 경우 값을 포함할 수 있는 가장 가까운 형식으로 변환된다.
- `++`, `--` 연산자는 모든 숫자 형식과 `char` 형식에 사용 가능하다.
- 복합 할당 식의 결과 형식은 왼쪽 피연산자의 형식이다.

<br>

## 이진 연산

<br>

- `+` 연산자는 피연산자의 합계를 계산한다.
- `-` 연산자는 왼쪽 피연산자에서 오른쪽 피연산자를 뺀다.

```cs
Console.WriteLine(1 + 2);
Console.WriteLine(3 - 2);

/* output:
3
1
*/
```

- 피연산자가 다음 형식인 경우 연산 결과는 `int` 형식으로 변환된다.
    - `sbyte`, `byte`, `short`, `ushort`

```cs
byte A = 1;
short B = 2;
var C = A + B;

Console.WriteLine(C);
Console.WriteLine(C.GetType());

/* output:
3
System.Int32
*/
```

- 정수 또는 부동 소수점만의 연산 결과 형식은 피연산자 중 더 큰 형식이다.
    - 피연산자의 형식이 같은 경우에는 같은 형식으로 지정된다.

```cs
int A = 1;
long B = 2;
var C = A + B;

Console.WriteLine(C);
Console.WriteLine(C.GetType());

float D = 2;
double E = 5;
var F = D + E;

Console.WriteLine(F);
Console.WriteLine(F.GetType());

/* output:
3
System.Int64
7
System.Double
*/
```

- 정수와 부동 소수점 사이의 연산 결과 형식은 부동 소수점 형식이다.

```cs
long A = 3;
float B = 0;

var C = A - B;
Console.WriteLine(C.GetType());

/* output:
System.Single
*/
```

<br>

## 단항 연산

<br>

- `+` 연산자는 피연산자의 값을 반환한다.
- `-` 연산자는 부호가 반전된 피연산자의 값을 반환한다.
    - 피연산자가 `uint` 형식인 경우 연산 결과는 `long` 형식으로 변환된다.
- 피연산자가 다음 형식인 경우 연산 결과는 `int` 형식으로 변환된다.
    - `sbyte`, `byte`, `short`, `ushort`

```cs
Console.WriteLine(-(-10));

ushort A = 1;
var B = +A;
uint C = 2;
var D = -C;

Console.WriteLine(B);
Console.WriteLine(B.GetType());
Console.WriteLine(D);
Console.WriteLine(D.GetType());

/* output:
10
1
System.Int32
-2
System.Int64
*/
```

<br>

### 증감 연산자

<br>

- `++` 연산자는 피연산자를 1씩 증가시킨다. 변수, 프로퍼티 및 인덱서에 적용 가능하다.
- 피연산자를 기준으로 `++` 연산자의 위치에 따라 값의 변경 시점이 달라진다.

```cs
// 전위 증가 연산자

int A = 1;

Console.WriteLine(A);
Console.WriteLine(++A);
Console.WriteLine(A);

// 후위 증가 연산자

A = 1;

Console.WriteLine(A);
Console.WriteLine(A++);
Console.WriteLine(A);

/* output:
1
2
2
1
1
2
*/
```

- `--` 연산자는 피연산자를 1씩 감소시킨다. 변수, 프로퍼티 및 인덱서에 적용 가능하다.
- 피연산자를 기준으로 `--` 연산자의 위치에 따라 값의 변경 시점이 달라진다.

```cs
// 전위 감소 연산자

int A = 2;

Console.WriteLine(A);
Console.WriteLine(--A);
Console.WriteLine(A);

// 후위 감소 연산자

A = 2;

Console.WriteLine(A);
Console.WriteLine(A--);
Console.WriteLine(A);

/* output:
2
1
1
2
2
1
*/
```

<br>

## 할당 연산자

<br>

- 할당 연산자 `+=` 또는 `-=`는 연산과 동시에 할당이 가능해지는 복합 할당 식을 가능하게 한다.
- 다음 두 식은 동일한 연산을 수행한다.
    (byte 연산 시 `int`로 변환되어 `X = X + Y;`는 실제로는 수행할 수 없는 코드이다.)

```cs
byte X = 255;
byte Y = 1;

X = X + Y;
X += Y;
```

- 연산은 동일하지만 캐스팅이 다르게 되는데, 위의 예제를 조금 더 자세히 풀어보면 아래와 같다.

```cs
byte X = 255;
byte Y = 1;

X = (int)X + (int)Y;
X = (byte)((int)X + (int)Y);
```

- 이런 캐스팅의 특성으로 인해 의도치 않은 계산의 결과가 일어날 수 있다.

```cs
byte X = 255;
byte Y = 1;

var Z = X + Y;

Console.WriteLine(Z);
Console.WriteLine(Z.GetType());

X += Y;

Console.WriteLine(X);
Console.WriteLine(X.GetType());

/* output:
256
System.Int32
0
System.Byte
*/
```

- 새로운 값 `Z`는 `int` 형식이므로 256을 출력한다.
- 반면, `byte` 형식의 최대값은 255 (FF) 이기 때문에 `X`의 값은 0이 된다.
- 코드 양을 줄이기 위해 축약형 표현을 사용하는 경우가 많은데, 연산을 하는 경우에는 주의가 필요하다.

<br>

## 참조 자료

<br>

- [산술 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/arithmetic-operators)
- [C# - Language - 정수 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D)