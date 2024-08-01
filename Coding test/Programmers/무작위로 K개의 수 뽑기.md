## 1. 출처

<br>

[코딩테스트 연습 - 무작위로 K개의 수 뽑기](https://school.programmers.co.kr/learn/courses/30/lessons/181858)

<br>

## 2. 문제 설명

<br>

랜덤으로 서로 다른 k개의 수를 저장한 배열을 만드려고 합니다. 적절한 방법이 떠오르지 않기 때문에 일정한 범위 내에서 무작위로 수를 뽑은 후, 지금까지 나온적이 없는 수이면 배열 맨 뒤에 추가하는 방식으로 만들기로 합니다.

이미 어떤 수가 무작위로 주어질지 알고 있다고 가정하고, 실제 만들어질 길이 `k`의 배열을 예상해봅시다.

정수 배열 `arr`가 주어집니다. 문제에서의 무작위의 수는 `arr`에 저장된 순서대로 주어질 예정이라고 했을 때, 완성될 배열을 return 하는 solution 함수를 완성해 주세요.

단, 완성될 배열의 길이가 `k`보다 작으면 나머지 값을 전부 -1로 채워서 return 합니다.

<br>

## 3. 제한사항

<br>

- 1 ≤ `arr`의 길이 ≤ 100,000
- 0 ≤ `arr`의 원소 ≤ 100,000
- 1 ≤ `k` ≤ 1,000

<br>

## 4. 풀이 전략

<br>

1. `List`를 이용해 `arr`에 없는 값을 추가한다.
2. `List`의 길이가 `k`가 되도록 `-1`을 채운다.

<br>

## 5. Code

<br>

```cs
using System;
using System.Collections.Generic;

public class Solution
{
    public int[] solution(int[] arr, int k)
    {
        List<int> answer = new List<int>();

        foreach (var value in arr)
        {
            if (!answer.Contains(value))
            {
                answer.Add(value);
            }
            if (answer.Count == k)
            {
                return answer.ToArray();
            }
        }

        for (int i = answer.Count; i < k; i++)
        {
            answer.Add(-1);
        }

        return answer.ToArray();
    }
}
```