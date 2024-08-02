## 1. 출처

<br>

[코딩테스트 연습 - k의 개수](https://school.programmers.co.kr/learn/courses/30/lessons/120887)

<br>

## 2. 문제 설명

<br>

1부터 13까지의 수에서, 1은 1, 10, 11, 12, 13 이렇게 총 6번 등장합니다. 정수 `i`, `j`, `k`가 매개변수로 주어질 때, `i`부터 `j`까지 `k`가 몇 번 등장하는지 return 하도록 solution 함수를 완성해주세요.

<br>

## 3. 제한사항

<br>

- 1 ≤ `i` < `j` ≤ 100,000
- 0 ≤ `k` ≤ 9

<br>

## 4. 풀이 전략

<br>
 
순회할 숫자를 string으로 변환한 후 각 자리에 `k`가 있는지 체크한다.

<br>

## 5. Code

<br>

```cs
using System;
using System.Linq;

public class Solution
{
    public int solution(int i, int j, int k)
    {
        int answer = 0;

        for (int index = i; index <= j; index++)
        {
            answer += index.ToString().Count(ch => ch == k.ToString()[0]);
        }

        return answer;
    }
}
```