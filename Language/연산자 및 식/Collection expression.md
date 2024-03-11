## Introduction

<br>

- C# 12부터, [컬렉션](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/collections) 및 [배열](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/arrays)은 컬렉션 식 (`[]`) 을 이용하여 초기화가 가능하다.
- 본문에서는 [List\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.list-1?view=net-8.0)를 활용하여 예시를 작성한다.
- 다음 예시와 같이, 컬렉션 식을 사용하여 코드를 간략하게 구성할 수 있다.
    ```cs
    // C# 11
    List<int> foo = new() { 1, 2, 3 };

    // C# 12
    List<int> bar = [1, 2, 3];
    ```

<br>

## Example

<br>

- 컬렉션 식은 다양한 형태로 사용 가능하다.

```cs
internal class Program
{
    // 필드
    private List<int> _field = [1, 2, 3];

    // 프로퍼티
    private List<int> _property => [1, 2, 3];

    static void Main(string[] args)
    {   
        // 변수
        List<int> variable = [1, 2, 3];

        int param1 = 1;
        int param2 = 2;
        int param3 = 3;
        List<int> parameters = [param1, param2, param3];

        // 컬렉션 값 인라인
        List<int> spread1 = [1, 2, 3];
        List<int> spread2 = [4, 5, 6];
        List<int> spreads = [.. spread1, .. spread2];

        // 매개변수
        Method([1, 2, 3]);
    }

    static int Method(IEnumerable<int> values) => values.Sum();
}
```

<br>

## 컬렉션 빌더

<br>

- 사용자 정의 형식은 [IEnumerable\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.ienumerable-1), [CollectionBuilderAttribute](https://learn.microsoft.com/ko-kr/dotnet/api/system.runtime.compilerservices.collectionbuilderattribute) 구현을 통해 컬렉션 식을 사용할 수 있다.
    ```cs
    [CollectionBuilder(typeof(MyCollection), nameof(Build))]
    public class MyCollection : IEnumerable<int>
    {
        private readonly List<int> ints = new();

        public MyCollection(ReadOnlySpan<int> initializer)
        {
            foreach (var item in initializer) ints.Add(item);
        }

        public IEnumerator<int> GetEnumerator() => ints.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ints.GetEnumerator();

        internal static MyCollection Build(ReadOnlySpan<int> initializer) => new MyCollection(initializer);
    }
    ```
    - `CollectionBuilder`의 생성자는 다음 매개변수를 가진다.
        - BuilderType : 컬렉션 빌더의 대상 형식
        - MethodName : 컬렉션을 생성하는데 사용할 메서드 이름
- 위와 같이 작성한 사용자 정의 형식은 다음 예시와 같이 컬렉션 식을 지원한다.
    ```cs
    MyCollection collection = [1, 2, 3, 4, 5];

    Console.WriteLine(string.Join(", ", collection));

    /* output:
    1, 2, 3, 4, 5
    */
    ```

<br>

## 참조 자료

<br>

- [컬렉션 식 - C# 언어 참조](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/collection-expressions)
- [컬렉션](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/collections)
- [배열](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/arrays)
- [IEnumerable\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.ienumerable-1)
- [CollectionBuilderAttribute](https://learn.microsoft.com/ko-kr/dotnet/api/system.runtime.compilerservices.collectionbuilderattribute)