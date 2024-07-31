## 1. 출처

<br>

[코딩테스트 연습 - a와 b 출력하기](https://school.programmers.co.kr/learn/courses/30/lessons/181951)

<br>

## 2. 문제 설명

<br>

정수 `a`와 `b`가 주어집니다. 각 수를 입력받아 입출력 예와 같은 형식으로 출력하는 코드를 작성해 보세요.

<br>

## 3. 제한사항

<br>

- -100,000 ≤ `a`, `b` ≤ 100,000

<br>

## 4. 풀이 전략

<br>
 
띄어쓰기 (` `) 를 이용해 각 숫자를 분리하고 `int` 형식으로 변환한다. 

<br>

## 5. Code

<br>

```cs
using System;

public class Example
{
    public static void Main()
    {
        var s = Console.ReadLine().Split(' ');

        Console.WriteLine($"a = {int.Parse(s[0])}");
        Console.WriteLine($"b = {int.Parse(s[1])}");
    }
}
```