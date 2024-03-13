## Introduction

<br>

- `.` 기호는 네임스페이스 및 형식 멤버에 액세스할 때 사용한다.

<br>

## Using 지시문

<br>

- [using 지시문](https://peponi-paradise.tistory.com/entry/C-Language-Using-directive)을 사용할 때 `.`을 사용하여 중첩된 네임스페이스 또는 형식에 접근할 수 있다.

```cs
using System.ComponentModel;
using static System.Console;
```

<br>

## 정규화된 형식

<br>

- `.`을 사용하여 `정규화된 이름`을 만들고 네임스페이스 내의 형식에 액세스할 수 있다.
- `using 지시문`을 사용하면 정규화를 하지 않아도 된다.

```cs
System.Collections.Generic.List<int> ints = [1, 2, 3, 4, 5];

System.Console.WriteLine(string.Join(", ", ints));

/* output:
1, 2, 3, 4, 5
*/
```

<br>

## 멤버 접근

<br>

- `.`을 사용하여 형식의 정적, 비정적 멤버에 접근할 수 있다.

```cs
string foo = "ABC";

Console.WriteLine(foo.Length);

/* output:
3
*/
```

<br>

## 확장 메서드

<br>

- `.`을 사용하여 [확장 메서드](https://peponi-paradise.tistory.com/entry/C-Language-This-keyword)에 접근할 수 있다.

```cs
public static class IntExtension
{
    public static int Add(this int A, int B) => A + B;
}
```
```cs
int foo = 1;
int bar = 2;

Console.WriteLine(foo.Add(bar));

/* output:
3
*/
```

<br>

## 참조 자료

<br>

- [멤버 액세스 및 null 조건부 연산자 및 식 - 멤버 액세스 식 .](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/member-access-operators#member-access-expression-)
- [C# - Language - Using 문 (Using statement)](https://peponi-paradise.tistory.com/entry/C-Language-Using-directive)
- [C# - Language - This keyword](https://peponi-paradise.tistory.com/entry/C-Language-This-keyword)