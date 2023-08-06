## null

<br>

- `null` 키워드는 참조 형식의 기본값이다.
- `null reference`를 나타내는 키워드로, 어느 객체도 참조하지 않고 있음을 나타낸다.
- 값 형식은 [null 허용 값 형식](https://peponi-paradise.tistory.com/entry/C-Language-Null-%ED%97%88%EC%9A%A9-%EA%B0%92-%ED%98%95%EC%8B%9D-Nullable-value-type)을 제외하고 `null`을 가질 수 없다.

<br>

### Example

<br>

```cs
string a = null;
Console.WriteLine(a);

a = "A";
Console.WriteLine(a);

// int b = null;   // CS0037

int? b = null;
Console.WriteLine(b);

b = 10;
Console.WriteLine(b);

/* output:

A

10
*/
```

<br>

## default

<br>

- `default` 키워드는 