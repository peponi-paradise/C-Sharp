## Introduction

<br>

- 인덱스 (`^`), 범위 (`..`) 연산자는 `Count`, `Length`와 같은 `int getter` 프로퍼티를 가진 형식에 사용할 수 있다.
    - [컬렉션 식](https://peponi-paradise.tistory.com/entry/C-Language-Collection-expression) 또한 여기에 포함된다.
- `^` 연산자는 요소 위치를 거꾸로 표현한다.
    - `^n`으로 위치를 지정한다.
    - n의 자리에 시퀀스 길이를 넣는 경우 첫번째 요소, `^1`은 마지막 요소이다.
- `..` 연산자는 요소의 범위를 나타낸다.
    - 표현식은 `a..b`이며 `[a, b)`를 의미한다.
    - `^` 연산자와 같이 사용할 수 있다.
- 아래는 `^`, `..` 연산자의 사용법을 보여준다.

<br>

## `^` 연산자

<br>

```cs
string foo = "Hello, World!";

Console.WriteLine(foo[^2]);     // d

int[] bar = [1, 2, 3, 4, 5];

Console.WriteLine(bar[^2]);     // 4

List<int> baz = [1, 2, 3, 4];

Console.WriteLine(baz[^2]);     // 3
```

<br>

## `..` 연산자

<br>

```cs
List<int> foo = [1, 2, 3, 4, 5, 6, 7];

// a.. : a부터 끝까지
var segment = foo[2..];
Write(segment);

// ..b : 처음부터 b-1까지
segment = foo[..5];
Write(segment);

// a..b : a부터 b-1까지
segment = foo[2..5];
Write(segment);

void Write(List<int> segment)
{
    segment.ForEach(x => Console.Write($"{x} "));
    Console.WriteLine();
}

/* output:
3 4 5 6 7
1 2 3 4 5
3 4 5
*/
```

<br>

### `^` 연산자와 같이 사용

<br>

- `..` 연산자는 `^` 연산자와 함께 사용 가능하다.

```cs
List<int> foo = [1, 2, 3, 4, 5, 6, 7];

// ^n.. : 뒤에서 n부터
var segment = foo[^5..];
Write(segment);

// ..^n : 처음부터 뒤에서 n+1까지
segment = foo[..^2];
Write(segment);

// ^n..^n : 뒤에서 n부터 뒤에서 n+1까지
segment = foo[^5..^2];
Write(segment);

void Write(List<int> segment)
{
    segment.ForEach(x => Console.Write($"{x} "));
    Console.WriteLine();
}

/* output:
3 4 5 6 7
1 2 3 4 5
3 4 5
*/
```

<br>

## 컬렉션 범위 표현

<br>

- 요약하면, 컬렉션 범위를 표현하는 방법은 아래와 같다.

|식|범위|
|--|---|
|`..`|`[0, ^0)`|
|`..b`|`[0, b)`|
|`a..`|`[a, ^0)`|
|`a..b`|`[a, b)`|
|`^a..`|`[^a, ^0)`|
|`..^b`|`[0, ^b)`|
|`a..^b`|`[a, ^b)`|
|`^a..^b`|`[^a, ^b)`|

<br>

## 참조 자료

<br>

- [멤버 액세스 및 null 조건부 연산자 및 식 - 끝부터 인덱스 연산자 ^](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/member-access-operators#index-from-end-operator-)
- [멤버 액세스 및 null 조건부 연산자 및 식 - 범위 연산자 ..](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/member-access-operators#range-operator-)
- [컬렉션 식](https://peponi-paradise.tistory.com/entry/C-Language-Collection-expression)