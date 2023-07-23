<h1 id="title">Integration - Simpson 1/3 rule</h1>

<h2 id="intro">Introduction</h2>

1. 적분이란 정의된 함수의 그래프와 그 구간으로 둘러싸인 도형의 넓이를 구하는 것이다[footnote][위키피디아](https://ko.wikipedia.org/wiki/%EC%A0%81%EB%B6%84)[/footnote]<br>
    ![적분이미지](Integration.svg)<br>
    ![적분이미지2](Integration%20-%20TrapezoidalRule1.png)<br><br>
2. 수치 적분이란 연속된 데이터 포인트를 적당한 하나의 함수`function`를 포함한 급수합으로 근사하여 적분하는 것을 말한다.<br>
    ![적분이미지3](Integration%20-%20SimpsonOneDivThree.png)<br>
3. 앞선 글과는 달리, 연속된 데이터 `(X1, X2, X3...), (Y1, Y2, Y3...)`를 곡선 함수로 근사하고 적분하는 것을 `Simpson rule`이라고 한다.
    - 1/3 rule은 이차함수로 근사한다.
    - 3/8 rule은 삼차함수로 근사한다.
4. 아래 코드는 1/3 rule에 대한 코드이다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericalIntegration
{
    public static class Integration
    {
        /// <summary>
        /// 심슨 1/3오더는 곡선구간이고 각 구간이 짧을 수록 정확, 반드시 구간의 수가 짝수여야 함
        /// float으로도 대부분의 정밀도 요구 만족
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <returns>Calculated value</returns>
        static float SimpsonOneDivThree(List<float> Xs, List<float> Ys)
        {
            if (IsEven(Xs.Count) || IsEven(Ys.Count)) return -999;      //포인트가 홀수여야 구간 수가 짝수가 나옴

            float result = 0;
            float sumOdd = 0;
            float sumEven = 0;
            float intervalDivThree = (float)(Math.Abs(Xs.Max() - Xs.Min()) / (Xs.Count - 1) / 3);

            for (int i = 0; i < Ys.Count; i++) Ys[i] = Ys[i] > 0 ? Ys[i] : 0;   // 음수 0으로 처리

            for (int i = 1; i < Ys.Count - 1; i++)
            {
                if (IsEven(i)) sumEven += Ys[i]; 
                else sumOdd += Ys[i];
            }

            sumOdd = 4 * sumOdd;
            sumEven = 2 * sumEven;

            float YTotal = Ys[0] + Ys[Ys.Count - 1] + sumOdd + sumEven;

            result = intervalDivThree * YTotal;
            return result;
        }

        // 짝수, 홀수 구분
        public static bool IsEven(int count)=> count % 2 == 0 ? true : false;
    }
}
```