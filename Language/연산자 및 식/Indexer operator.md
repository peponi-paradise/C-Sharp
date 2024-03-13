## Introduction

<br>

- `[]`는 일반적으로 [배열](https://peponi-paradise.tistory.com/entry/C-Language-Array), [인덱서](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/indexers/), [포인터 요소 액세스](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/pointer-related-operators#pointer-element-access-operator-)에 사용한다.

<br>

## 배열 액세스

<br>

- `[]`를 이용하여 배열에 액세스 하는 경우는 다음과 같다.
    - 형식 선언
    - 인스턴스화
    - 요소 접근

```cs
int[] foo = [1, 2, 3];            

Console.WriteLine(foo[1]);  

/* output:
2
*/
```

- 만일 배열의 인덱스가 범위를 벗어난 경우 [IndexOutOfRangeException](https://learn.microsoft.com/ko-kr/dotnet/api/system.indexoutofrangeexception)이 발생한다.

```cs
int[] foo = [1, 2, 3];

try
{
    var value = foo[5];
}
catch (IndexOutOfRangeException e)
{
    Console.WriteLine(e.Message);
}

/* output:
Index was outside the bounds of the array.
*/
```

<br>

## 인덱서 액세스

<br>

- 인덱서를 사용하면 배열과 비슷한 방법으로 인덱싱을 수행할 수 있다.
- 배열과 달리 인덱서 매개 변수는 임의 형식으로 하는 것이 가능하다.
- 다음은 리스트 및 딕셔너리 형식의 액세스 방법을 보여준다.

```cs
List<int> foo = [1, 2, 3];      

Console.WriteLine(foo[1]);

Dictionary<string, int> bar = new() { { "Zero", 0 }, { "One", 1 }, { "Two", 2 } };

Console.WriteLine(bar["One"]);

/* output:
2
1
*/
```

- 만일 리스트 형식의 범위를 벗어난 경우 [ArgumentOutOfRangeException](https://learn.microsoft.com/ko-kr/dotnet/api/system.argumentoutofrangeexception?view=net-8.0)이 발생한다.

```cs
List<int> foo = [1, 2, 3];

try
{
    var value = foo[5];
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine(e.Message);
}

/* output
Index was out of range. Must be non-negative and less than the size of the collection. (Parameter 'index')
*/
```

- 만일 딕셔너리 형식의 키를 찾을 수 없는 경우 [KeyNotFoundException](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.keynotfoundexception?view=net-7.0)이 발생한다.

```cs
Dictionary<string, int> foo = new() { { "Zero", 0 }, { "One", 1 }, { "Two", 2 } };

try
{
    var value = foo["Five"];
}
catch (KeyNotFoundException e)
{
    Console.WriteLine(e.Message);
}

/* output:
The given key 'Five' was not present in the dictionary.
*/
```

<br>

## 포인터 요소 액세스

<br>

- `type* p`의 `p[n]` 액세스는 `*(p + n)`으로 계산된다.
- 다음은 포인터 요소 액세스 방법을 보여준다.

```cs
unsafe
{
    int* foo = stackalloc int[3];

    for (int i = 0; i < 3; i++)
    {
        foo[i] = i;
    }

    for (int i = 0; i < 3; i++)
    {
        Console.Write($"{foo[i]} ");
    }
}

/* output:
0 1 2
*/
```

<br>

## 참조 자료

<br>

- [멤버 액세스 및 null 조건부 연산자 및 식 - 인덱서 연산자 []](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/member-access-operators#indexer-operator-)
- [C# - Language - Array](https://peponi-paradise.tistory.com/entry/C-Language-Array)
- [인덱서(C# 프로그래밍 가이드)](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/indexers/)
- [포인터 요소 액세스 연산자 []](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/pointer-related-operators#pointer-element-access-operator-)