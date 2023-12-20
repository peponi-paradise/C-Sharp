## Introduction

<br>

- `==`, `!=` 연산자는 피연산자가 같은지 여부를 확인한다.
  - [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)은 값을 비교한다.
  - [참조 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D)은 참조 주소를 비교한다.
- `==` 연산자는 피연산자가 같으면 `true`, 다르면 `false`를 반환한다.
- `!=` 연산자는 `==` 연산자의 반대 결과를 반환한다.

<br>

## 값 형식

<br>

```cs
int X = 1;
int Y = 2;
int Z = 3;

Console.WriteLine(X == Y);
Console.WriteLine(X != Z);
Console.WriteLine(X + Y == Z);

/* output:
False
True
True
*/
```

- `struct` 형식의 경우 기본적으로 `==` 연산자를 지원하지 않는다.
  - 같음 연산자를 사용하려면 형식에 해당 연산자를 오버로드해야 한다.

