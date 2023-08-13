## Introduction

<br>

- `in` 키워드는 읽기 전용 참조 매개 변수를 의미한다.
    - 메서드에 전달되기 전 초기화가 필요하다.
    - 읽기 전용 매개 변수가 되기 때문에 호출된 메서드 내부에서는 쓰기가 불가능하다.
    - 참조 매개 변수로 전달되므로, 큰 크기의 [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D) 데이터를 넘기는 데 활용할 수 있다.
- 메서드 선언 시 `in` 키워드를 지정하지만, 호출 사이트에서는 일부러 키워드를 넣을 필요는 없다.
    - `in` 키워드가 없는 메서드가 있는 경우에는 키워드를 넣어주어야 한다.
- 다음 요소가 포함된 메서드에는 `in`, `ref`, `out` 키워드 사용이 불가능하다.
    - [async](https://peponi-paradise.tistory.com/entry/C-Language-Async-Await)
    - [yield return](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/yield), `yield break`
- 확장 메서드에는 다음과 같은 제약이 있다.
    - 첫 번째 인수가 구조체일 때만 사용 가능
    - 첫 번째 인수가 제네릭인 경우 사용 불가

<br>

## Example

<br>

```cs
public static void Foo(int value)
{
    Console.WriteLine($"Foo(int) : {value}");
}

public static void Foo(in int value)
{
    Console.WriteLine($"Foo(in int) : {value}");
}

private static void Main()
{
    int input = 2;

    Foo(input);
    Foo(in input);
}

/* output:
Foo(int) : 2
Foo(in int) : 2
*/
```

<br>

## 참조 자료

<br>

- [in 매개 변수 한정자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/in-parameter-modifier)