<h1 id="title">Integration - Midpoint Rule</h1>

<h2 id="intro">Introduction</h2>

1. 적분이란 정의된 함수의 그래프와 그 구간으로 둘러싸인 도형의 넓이를 구하는 것이다[footnote][위키피디아](https://ko.wikipedia.org/wiki/%EC%A0%81%EB%B6%84)[/footnote]<br>
    ![적분이미지](Integration.svg)<br>
    ![적분이미지2](Integration%20-%20TrapezoidalRule1.png)<br><br>
2. 수치 적분이란 연속된 데이터 포인트를 적당한 하나의 함수`function`를 포함한 급수합으로 근사하여 적분하는 것을 말한다.<br>
    ![적분이미지3](Integration%20-%20MidpointRule.png)<br>
3. 위 이미지와 같이, 연속된 데이터 `(X1, X2, X3...), (Y1, Y2, Y3...)`를 적당한 하나의 함수로 근사하고, 각 직사각형을 중점에 오게 하여 적분하는 것을 `Midpoint Rule`이라고 한다.
4. 자세한 구현은 아래 코드를 참조

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
        /// 중점공식은 바 그래프 형태에서 가장 정확성 높음
        /// float으로도 대부분의 정밀도 요구 만족
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <returns>Calculated value</returns>
        public static float MidpointRule(List<float> Xs, List<float> Ys)
        {
            float result = 0;

            for (int i = 0; i < Ys.Count; i++) Ys[i] = Ys[i] > 0 ? Ys[i] : 0;   // 음수 0으로 처리

            //  끝점 제외한 나머지 계산
            for (int i = 0; i < Xs.Count - 1; i++)
            {
                float YMidpoint = Ys[i] < Ys[i + 1] ? Ys[i] + (Math.Abs(Ys[i + 1] - Ys[i])) / 2 : Ys[i + 1] + (Math.Abs(Ys[i + 1] - Ys[i])) / 2;
                result += YMidpoint * (float)Math.Abs(Xs[i + 1] - Xs[i]);
            }
            return result;
        }
    }
}
```