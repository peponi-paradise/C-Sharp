## Introduction

<br>

- Null 조건부 연산자 (`?.`, `?[]`) 는 [멤버 액세스 (.)](https://peponi-paradise.tistory.com/entry/C-Language-Member-access-expression), [인덱서 액세스 ([])](https://peponi-paradise.tistory.com/entry/C-Language-Indexer-operator) 시 피연산자가 `null`이 아닐 때만 연산을 수행하며 `null`인 경우에는 `null`을 반환한다.

<br>

## Example

<br>

- 피연산자가 `null`인 경우, `.` 및 `[]` 연산이 실행되지 않고 `null`을 반환한다.
    ```cs
    string? foo = null;

    if (foo?.Length is null) Console.WriteLine("Null-conditional member access returns null");
    if (foo?[1] is null) Console.WriteLine("Null-conditional indexer access returns null");

    /* output:
    Null-conditional member access returns null
    Null-conditional indexer access returns null
    */
    ```
- 피연산자가 `null`이 아닌 경우, `?.` 및 `?[]`는 각각 `.` 및 `[]` 연산의 결과를 반환한다.
    ```cs
    string? foo = "ABCDE";

    if (foo?.Length is int length)
    {
        Console.WriteLine(length);
    }
    else
    {
        Console.WriteLine("Null-conditional member access returns null");
    }

    if (foo?[1] is char value)
    {
        Console.WriteLine(value);
    }
    else
    {
        Console.WriteLine("Null-conditional indexer access returns null");
    }

    /* output:
    5
    B
    */
    ```

<br>

## 