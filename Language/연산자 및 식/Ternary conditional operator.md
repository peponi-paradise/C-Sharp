## 1. 삼항 연산자

<br>

- 삼항 연산자 (`?:`) 는 boolean 식을 계산 후 반환 값에 따라 결과를 반환한다.
- 삼항 연산자의 사용 방법은 아래와 같다.
    ```cs
    Condition ? True : False
    ```
    - Condition은 boolean 식이며, 계산 결과가 `true`일 때는 True 항이 반환되고 `false`일 때는 False 항이 반환된다.
- 삼항 연산자는 왼쪽에서부터 오른쪽으로 연산을 수행한다.
    ```cs
    static void Main(string[] args)
    {
        int A = 2;

        Console.WriteLine(IsZero(A) ? "0" : IsOne(A) ? "1" : "2");

        bool IsZero(int x)
        {
            Console.WriteLine(nameof(IsZero));
            return x == 0;
        }
        bool IsOne(int x)
        {
            Console.WriteLine(nameof(IsOne));
            return x == 1;
        }
    }

    /* output:
    IsZero
    IsOne
    2
    */
    ```
- 다음 예제에서와 같이, 삼항 연산자를 이용하여 간단한 [if 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-if-statement)을 대체하는 경우 코드가 간결해질 수 있다.
    ```cs
    static void Main(string[] args)
    {
        int A = 2;

        // If 문

        if (A == 0)
        {
            Console.WriteLine(0);
        }
        else if (A == 1)
        {
            Console.WriteLine(1);
        }
        else
        {
            Console.WriteLine(2);
        }

        // 삼항 연산자

        Console.WriteLine(A == 0 ? 0 : A == 1 ? 1 : 2);
    }
    ```

<br>

## 2. 조건부 참조 식

<br>

- `ref` 키워드를 이용하여 조건부 참조 식의 결과를 활용할 수 있다.
- 조건부 참조 식의 사용 방법은 아래와 같다.
    ```cs
    Condition ? ref True : ref False
    ```
    ```cs
    static void Main(string[] args)
    {
        int A = 10;
        int B = 20;
        int X = 15;

        // B의 참조를 Y에 할당
        ref int Y = ref (X < 10 ? ref A : ref B);

        Console.WriteLine(Y);

        Y = 0;

        Console.WriteLine(B);
    }

    /* output:
    20
    0
    */
    ```

<br>

## 3. 참조 자료

<br>

- [?: 연산자 - 3개로 구성된 조건 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/conditional-operator)