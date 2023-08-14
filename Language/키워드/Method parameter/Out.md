## Introduction

<br>

- `out` 키워드는 쓰기 전용 참조 매개 변수를 의미한다.
- 메서드에 전달되기 전 할당은 선택사항이며, 선언된 `out` 파라미터는 리턴 전 값을 할당해야 한다.
- [in](https://peponi-paradise.tistory.com/entry/C-Language-In-keyword-Parameter-modifier) 키워드와 달리, `out` 키워드는 호출 시 반드시 입력해야 한다.
- `out` 키워드를 사용하여 여러 값을 반환할 수 있다.
    - 대안으로 [튜플 형식 (System.ValueTuple)](https://peponi-paradise.tistory.com/entry/C-Language-%ED%8A%9C%ED%94%8C-%ED%98%95%EC%8B%9D-SystemValueTuple)을 사용할 수도 있다.
- 다음 요소가 포함된 메서드에는 `in`, `ref`, `out` 키워드 사용이 불가능하다.
    - [async](https://peponi-paradise.tistory.com/entry/C-Language-Async-Await)
    - [yield return](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/yield), `yield break`
- 확장 메서드에는 다음과 같은 제약이 있다.
    - 첫 번째 인수에는 `out` 키워드 사용 불가

<br>

## Example

<br>

```cs
public static void Foo(out int value)
{
    value = 10;
}

private static void Main()
{
    int input;

    // Foo(input);     // CS1620

    Foo(out input);

    Console.WriteLine(input);

    Foo(out var value);

    Console.WriteLine(value);
}

/* output:
10
10
*/
```

<br>

## 참조 자료

<br>

- [out 매개 변수 한정자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/out-parameter-modifier)