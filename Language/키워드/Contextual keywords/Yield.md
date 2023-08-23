## Introduction

<br>

- `yield` 키워드는 [iterator](https://learn.microsoft.com/en-us/dotnet/csharp/iterators) ([for 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements#the-for-statement), [foreach 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement)) 의 다음 값을 리턴하거나 끝을 알리는 데 사용한다.
    - `yield return` : 값을 리턴한다.
    - `yield break` : 반복의 끝을 알린다.
- `yield` 키워드를 사용하려는 경우, 메서드는 다음 형식 중 하나를 리턴해야 한다.
    - [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-7.0)
    - [IEnumerable<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-7.0)
    - [IAsyncEnumerable<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1?view=net-7.0)
    - [IEnumerator](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerator?view=net-7.0)
    - [IEnumerator<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.ienumerator-1?view=net-7.0)
    - [IAsyncEnumerator<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.iasyncenumerator-1?view=net-7.0)
- `yield` 키워드는 다음 식 또는 메서드에서 사용이 불가능하다.
    - [in](https://peponi-paradise.tistory.com/entry/C-Language-In-keyword-Parameter-modifier), [ref](https://peponi-paradise.tistory.com/entry/C-Language-Ref-keyword-Parameter-modifier), [out](https://peponi-paradise.tistory.com/entry/C-Language-Out-keyword-Parameter-modifier) 매개 변수 한정자가 사용된 메서드
    - [람다 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/lambda-expressions), [익명 메서드](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/delegate-operator)
    - [unsafe](https://peponi-paradise.tistory.com/entry/C-Language-Unsafe) 한정자를 사용하는 메서드

<br>

## Example

<br>

```cs
public class CartesianCoordinate
{
    private List<ValueTuple<double, double>> coordinates = new List<(double, double)>();

    public void AddValue(double x, double y)
    {
        coordinates.Add(new ValueTuple<double, double>(x, y));
    }

    public IEnumerable<ValueTuple<double, double>> GetWholeValues()
    {
        foreach (var item in coordinates)
        {
            yield return item;
        }
    }

    public IEnumerable<ValueTuple<double, double>> GetSomeValues(int dataCount)
    {
        for (int i = 0; i < dataCount; i++)
        {
            if (i > coordinates.Count - 1) yield break;
            else yield return coordinates[i];
        }
    }
}

private static void Main()
{
    CartesianCoordinate cartesianCoordinate = new CartesianCoordinate();
    cartesianCoordinate.AddValue(0, 0);
    cartesianCoordinate.AddValue(5, 5);
    cartesianCoordinate.AddValue(10, 10);

    foreach (var item in cartesianCoordinate.GetWholeValues())
    {
        Console.WriteLine(item);
    }

    foreach (var item in cartesianCoordinate.GetSomeValues(2))
    {
        Console.WriteLine(item);
    }
}

/* output:
(0, 0)
(5, 5)
(10, 10)
(0, 0)
(5, 5)
*/
```

<br>

## Yield 동작 특성

<br>

```cs
public class CartesianCoordinate
{
    private List<ValueTuple<double, double>> coordinates = new List<(double, double)>();

    public void AddValue(double x, double y)
    {
        coordinates.Add(new ValueTuple<double, double>(x, y));
    }

    public IEnumerable<ValueTuple<double, double>> GetWholeValues()
    {
        Console.WriteLine("2. Iterator entry point");

        foreach (var item in coordinates)
        {
            Console.WriteLine($"3. Will return {item}");

            yield return item;

            Console.WriteLine($"5. End return {item}");
        }

        Console.WriteLine("6. Iterator exit point");
    }
}

private static void Main()
{
    CartesianCoordinate cartesianCoordinate = new CartesianCoordinate();
    cartesianCoordinate.AddValue(0, 0);
    cartesianCoordinate.AddValue(5, 5);

    var wholeValues = cartesianCoordinate.GetWholeValues();

    Console.WriteLine("1. Iterator is waiting");

    foreach (var item in wholeValues)
    {
        Console.WriteLine($"4. Main : {item}");
    }
}

/* output:
1. Iterator is waiting
2. Iterator entry point
3. Will return (0, 0)
4. Main : (0, 0)
5. End return (0, 0)
3. Will return (5, 5)
4. Main : (5, 5)
5. End return (5, 5)
6. Iterator exit point
*/
```

- 위의 예제를 통해 알 수 있는 `yield`의 동작 특성은 아래와 같다.
    1. Iterator를 반환 후 대기
    2. 호출자가 iterator를 반복하기 시작하면 메서드 내부 진입
    3. `yield return`을 만날 때까지 진행하여 값 리턴
    4. 호출자의 리턴값 처리
    5. 호출자가 다음 iterator를 호출하면 다음 `yield return` 를 만날 때까지 진행
    6. 3 ~ 5 과정을 반복하다 iterator의 끝 또는 `yield break` 를 만나는 경우 종료

<br>

## 참조 자료

<br>

- [yield 문 - 다음 요소 제공](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/yield)