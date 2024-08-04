## Introduction

<br>

- `params` 키워드는 배열 형태의 인수를 지정한다. 여기서 배열은 1차원 배열이어야 한다.
- `params` 키워드 설정 시, 다른 매개 변수는 허용되지 않는다.

<br>

## Example

<br>

```cs
public static void ParamsInt(params int[] args)
{
    Console.WriteLine(String.Join(", ", args));
}

public static void ParamsString(params string[] args)
{
    Console.WriteLine(String.Join(", ", args));
}

public static void ParamsObject(params object[] args)
{
    Console.WriteLine(String.Join(", ", args));
}

private static void Main()
{
    ParamsInt(1, 2, 3);

    int[] intArgs = { 1, 2, 3 };
    ParamsInt(intArgs);

    ParamsString("Hello", "World!");

    string[] stringArgs = { "Hello", "World!" };
    ParamsString(stringArgs);

    // Object type's behavior depends on the type of array
    ParamsObject(intArgs);
    ParamsObject(stringArgs);
}

/* output:
1, 2, 3
1, 2, 3
Hello, World!
Hello, World!
System.Int32[]
Hello, World!
*/
```

<br>

## 참조 자료

<br>

- [params(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/params)