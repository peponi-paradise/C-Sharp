## 1. 출처

<br>

[코딩테스트 연습 - A로 B 만들기](https://school.programmers.co.kr/learn/courses/30/lessons/120886)

<br>

## 2. 문제 설명

<br>

문자열 `before`와 `after`가 매개변수로 주어질 때, `before`의 순서를 바꾸어 `after`를 만들 수 있으면 1을, 만들 수 없으면 0을 return 하도록 solution 함수를 완성해보세요.

<br>

## 3. 제한사항

<br>

- 0 < `before`의 길이 == `after`의 길이 < 1,000
- `before`와 `after`는 모두 소문자로 이루어져 있습니다.

<br>

## 4. 풀이 전략

<br>
 
1. `before`, `after`에 포함되어 있는 각 글자와 수를 각각의 `Dictionary`에 등록한다.
2. 각 `Dictionary`를 비교하여 같은지에 따라 결과를 출력한다.

<br>

## 5. Code

<br>

```cs
using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public int solution(string before, string after)
    {
        Dictionary<char, int> beforeData = new Dictionary<char, int>();
        Dictionary<char, int> afterData = new Dictionary<char, int>();

        for (int i = 0; i < before.Length; i++)
        {
            if (!beforeData.ContainsKey(before[i]))
            {
                beforeData.Add(before[i], 1);
            }
            else
            {
                beforeData[before[i]]++;
            }

            if (!afterData.ContainsKey(after[i]))
            {
                afterData.Add(after[i], 1);
            }
            else
            {
                afterData[after[i]]++;
            }
        }

        if (beforeData.Count == afterData.Count && beforeData.Values.Sum() == afterData.Values.Sum())
        {
            bool allSame = true;

            try
            {
                foreach (var item in beforeData)
                {
                    if (item.Value != afterData[item.Key])
                    {
                        allSame = false;
                        break;
                    }
                }
            }
            catch
            {
                return 0;
            }

            return allSame ? 1 : 0;
        }

        return 0;
    }
}
```