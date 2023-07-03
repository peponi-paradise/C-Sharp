
## Introduction

<br>

- `bool` (논리형) 키워드는 참 (`true`), 거짓 (`false`) 을 나타낸다. .NET 형식은 `System.Boolean`이다.
- 주로 논리 연산 및 플래그 용도로 사용된다. 기본값은 `false`이다.
- 다음은 `bool`이 사용될 수 있는 식 또는 문의 종류이다.
    - [비교](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/comparison-operators) 또는 [상등](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/equality-operators)
    - [삼항 연산자 ?:](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/conditional-operator)
    - [if](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-if-statement)
    - [do](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements#the-do-statement)
    - [while](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement)
    - [for](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements#the-for-statement)

<br>

## bool 초기화

<br>

- 리터럴 `true` 또는 `false`를 이용하여 초기화한다.
    ```cs
    bool boolean = true;
    bool boolean2 = false;
    ```

<br>

## Tri state boolean

<br>

- 값이 세 개인 논리형이 필요한 경우 (지뢰찾기 게임 같은 경우), `bool?` ([Nullable bool](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/boolean-logical-operators#nullable-boolean-logical-operators)) 형식을 사용할 수있다.
- `bool?` 형식에는 `true`, `false`, `null`을 할당할 수 있다.
- 대체제로, 적당하게 정의된 [열거형 형식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/enum)을 이용할 수도 있다.

<br>

## bool 값 변환

<br>

- `bool` 값으로 변환하기 위해 많은 방법을 사용할 수 있다. 아래는 대표적인 예시들이다.
    <table>
    <tr>
    <th>Method</th><th>Example</th><th>비고</th>
    </tr>

    <tr>
    <td>bool.Parse(string)</td>
    <td>

    ```cs
    bool X = bool.Parse("TrUe");
    ```

    </td>
    <td><code>string</code> 값은 대소문자를 가리지 않는다</td>
    </tr>

    <tr>
    <td>bool.TryParse(string, out bool)</td>
    <td>

    ```cs
    bool isSuccess = bool.TryParse("TRUe",out bool result);
    ```

    </td>
    <td><code>string</code> 값은 대소문자를 가리지 않는다<br><code>isSuccess</code>가 <code>true</code>이면 <code>result</code> 값이 유효하다</td>
    </tr>

    <tr>
    <td>Convert.ToBoolean(string)</td>
    <td>

    ```cs
    bool X = Convert.ToBoolean("truE");
    ```

    </td>
    <td><code>string</code> 값은 대소문자를 가리지 않는다</td>
    </tr>

    <tr>
    <td>Convert.ToBoolean(숫자 형식)</td>
    <td>

    ```cs
    bool X = Convert.ToBoolean(-4.156);     // true
    bool X = Convert.ToBoolean(123);        // true

    bool X = Convert.ToBoolean(0);          // false
    ```

    </td>
    <td><code>숫자 형식</code> 값을 넣을 때, <code>0</code>이 아니면 참이다</td>
    </tr>
    </table>

<br>

- 아래는 `bool` 값을 문자열, 정수 형식으로 변환하는 예시다.
    ```cs
    bool X = true;
    string convertString = X.ToString();    // True
    int convertInt = Convert.ToInt32(X);    // 1

    X = false;
    string convertString = X.ToString();    // False
    int convertInt = Convert.ToInt32(X);    // 0
    ```

<br>

## 참조 자료

<br>

- [bool(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/bool)