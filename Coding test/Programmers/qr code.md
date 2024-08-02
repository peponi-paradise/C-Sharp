## 1. 출처

<br>

[코딩테스트 연습 - qr code](https://school.programmers.co.kr/learn/courses/30/lessons/181903)

<br>

## 2. 문제 설명

<br>

두 정수 `q`, `r`과 문자열 `code`가 주어질 때, `code`의 각 인덱스를 `q`로 나누었을 때 나머지가 `r`인 위치의 문자를 앞에서부터 순서대로 이어 붙인 문자열을 return 하는 solution 함수를 작성해 주세요.

<br>

## 3. 제한사항

<br>

- 0 ≤ `r` < `q` ≤ 20
- `r` < `code`의 길이 ≤ 1,000
- `code`는 영소문자로만 이루어져 있습니다.

<br>

## 4. 풀이 전략

<br>
 
`Linq`의 [Where](https://learn.microsoft.com/ko-kr/dotnet/api/system.linq.enumerable.where?view=net-8.0)를 이용해 쉽게 연산을 수행할 수 있다.

<br>

## 5. Code

<br>

```cs
using System;
using System.Linq;

public class Solution
{
    public string solution(int q, int r, string code)
    {
        return new string(code.Where((ch, idx) => idx % q == r).ToArray());
    }
}
```

<br>

## 6. 참조 자료

<br>

- [Enumerable.Where 메서드](https://learn.microsoft.com/ko-kr/dotnet/api/system.linq.enumerable.where?view=net-8.0)