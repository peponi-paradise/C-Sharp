## Introduction

<br>

- 비교 연산자 (`>`, `<`, `>=`, `<=`)는 관계형 연산자라고도 하며, 값의 크기를 비교한다.
- 비교 연산자를 지원하는 형식은 아래 항목을 참조한다.
    - 정수 형식
    - 부동 소수점 형식
    - char 형식 : 문자 코드 비교
    - enum 형식
    - 사용자 정의 형식

<br>

## 비교 연산자

<br>

- 다음 섹션에서는 `사용자 정의 형식`을 제외한 비교 연산자의 사용 방법을 알아본다.

<br>

### `>` 연산자

<br>

- `>` (보다 큼) 연산자는 왼쪽 피연산자가 오른쪽 피연산자보다 크면 `true` 작으면 `false`를 반환한다.
    ```cs
    public enum CompareTest
    {
        A = 0,
        B = 1
    }
    ```
    ```cs
    Console.WriteLine(5 > 3);
    Console.WriteLine(5.1 > 5.1);
    Console.WriteLine('a' > 'b');   // a = 97, b = 98
    Console.WriteLine(CompareTest.A > CompareTest.B);

    /* output:
    True
    False
    False
    False
    */
    ```

<br>

### `<` 연산자

<br>

- `<` (보다 작음) 연산자는 왼쪽 피연산자가 오른쪽 피연산자보다 작으면 `true`, 크면 `false`를 반환한다.
    ```cs
    Console.WriteLine(5 < 3);
    Console.WriteLine(5.1 < 5.1);
    Console.WriteLine('a' < 'b');
    Console.WriteLine(CompareTest.A < CompareTest.B);

    /* output:
    False
    False
    True
    True
    */
    ```

<br>

### `>=` 연산자

<br>

- `>=` (크거나 같음) 연산자는 왼쪽 피연산자가 오른쪽 피연산자보다 크거나 같은 경우 `true`, 작은 경우 `false`를 반환한다.
    ```cs
    Console.WriteLine(5 >= 3);
    Console.WriteLine(5.1 >= 5.1);
    Console.WriteLine('a' >= 'b');
    Console.WriteLine(CompareTest.A >= CompareTest.B);

    /* output:
    True
    True
    False
    False
    */
    ```

<br>

### `<=` 연산자

<br>

- `<=` (작거나 같음) 연산자는 왼쪽 피연산자가 오른쪽 피연산자보다 작거나 같은 경우 `true`, 큰 경우 `false`를 반환한다.
    ```cs
    Console.WriteLine(5 <= 3);
    Console.WriteLine(5.1 <= 5.1);
    Console.WriteLine('a' <= 'b');
    Console.WriteLine(CompareTest.A <= CompareTest.B);

    /* output:
    False
    True
    True
    True
    */
    ```

<br>

## 사용자 정의 형식

<br>

- 사용자 정의 형식은 비교 연산자를 오버로드 할 수 있으며, 다음 연산자는 반드시 같이 오버로드 해야 한다.
    - `>`, `<` 연산자
    - `>=`, `<=` 연산자
- 연산자를 오버로드 함에 따라, 결과를 자유롭게 정의할 수 있다.
- 다음은 연산 결과를 반대로 뒤집어 출력하는 예시다.
    ```cs
    public class Compare
    {
        public int A = 0;

        public Compare(int a) => A = a;

        public static bool operator >(Compare a, int b) => a.A < b;    
        public static bool operator <(Compare a, int b) => a.A > b;
        public static bool operator >=(Compare a, int b) => a.A <= b;
        public static bool operator <=(Compare a, int b) => a.A >= b;
    }
    ```
    ```cs
     var test = new Compare(1);

    Console.WriteLine(test > 2);
    Console.WriteLine(test < 2);
    Console.WriteLine(test >= 2);
    Console.WriteLine(test <= 2);

    /* output:
    True
    False
    True
    False
    */
    ```
- 위의 예시와 같이, 자유롭게 정의하여 사용할 수 있지만 일반적으로 생각하는 연산과 반대의 결과를 출력할 수 있으므로 주의가 필요하다.

<br>

## 참조 자료

<br>

- [비교 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/comparison-operators)
- [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)