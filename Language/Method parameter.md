## Introduction

<br>

- C#의 형식에는 [참조(class)](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D), [값(struct)](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D) 형식이 있고 이를 매개 변수로 전달할 수 있다.
    - 참조 형식은 변수에 대한 참조를 전달한다.
    - 값 형식은 변수의 복사본을 전달한다.

<br>

## Example

<br>

```cs
class Foo
{
    public int Integer;
}

struct Bar
{
    public int Integer;
}

static void ChangeValue(Foo foo) => foo.Integer = 10;
static void ChangeValue(Bar bar) => bar.Integer = 10;

static void Main()
{
    Foo foo = new Foo() { Integer = 0 };
    ChangeValue(foo);   // 참조 전달

    Bar bar = new Bar() { Integer = 1 };
    ChangeValue(bar);   // 복사본 전달

    Console.WriteLine(foo.Integer);
    Console.WriteLine(bar.Integer);
}

/* output:
10
0
*/
```

<br>

## 참조 형식 매개 변수

<br>

- 