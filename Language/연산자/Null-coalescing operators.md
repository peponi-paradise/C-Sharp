## Introduction

<br>

- Null 병합 연산자 (`??`), null 병합 대입 연산자 (`??=`)는 다음과 같은 특징을 가지고 있다.
    - 왼쪽 피연산자로 변수, 프로퍼티, 인덱서가 허용된다.
    - 왼쪽 피연산자의 형식은 `null`을 허용해야 한다.
    - 왼쪽 피연산자가 `null`일 때 오른쪽 피연산자를 확인한다.
- `??`, `??=` 연산자는 오른쪽에서부터 왼쪽으로 연산을 수행한다.
    ```cs
    A ?? B ?? C
    A ??= B ??= C

    // 상기 두 식은 아래의 연산을 수행한다.

    A ?? (B ?? C)
    A ??= (B ??= C)
    ```

<br>

## `??` 연산자

<br>

- Null 병합 연산자 (`??`) 는 왼쪽 피연산자의 값이 `null`이 아닐 경우 왼쪽 피연산자의 값을 반환한다.
    - `null`인 경우 오른쪽 피연산자의 값을 확인한다.

```cs
bool? X = null;
Console.WriteLine(X ?? true);

X = false;
Console.WriteLine(X ?? true);

X = null;
Console.WriteLine(X ?? throw new Exception("Null detected"));

/* output:
True
False
Unhandled exception. System.Exception: Null detected
*/
```

<br>

## `??=` 연산자

<br>

- Null 병합 대입 연산자 (`??=`) 는 왼쪽 피연산자의 값이 `null`이 아닐 경우 왼쪽 피연산자의 값을 반환한다.
    - `null`인 경우 오른쪽 피연산자의 값을 할당 후 반환한다.

```cs
bool? X = null;
Console.WriteLine(X ??= true);

X = true;
Console.WriteLine(X ??= false);

X = null;
Console.WriteLine(X.GetValueOrDefault());

/* output:
True
True
False
*/
```

- Nullable 형식의 값이 `null`일 때 반환할 값이 non-null 형식의 기본 값이어야 하는 경우 [Nullable\<T>.GetValueOrDefault 메서드](https://learn.microsoft.com/ko-kr/dotnet/api/system.nullable-1.getvalueordefault?view=net-8.0#system-nullable-1-getvalueordefault)를 활용할 수 있다.

<br>

## 참조 자료

<br>

- [?? 및 ??= 연산자 - null 병합 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/null-coalescing-operator)
- [Nullable\<T>.GetValueOrDefault 메서드](https://learn.microsoft.com/ko-kr/dotnet/api/system.nullable-1.getvalueordefault?view=net-8.0#system-nullable-1-getvalueordefault)