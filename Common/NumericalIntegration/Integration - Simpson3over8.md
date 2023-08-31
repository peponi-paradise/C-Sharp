<h1 id="title">Integration - Simpson 3/8 rule</h1>

## Introduction

<br>

1. 적분이란 정의된 함수의 그래프와 그 구간으로 둘러싸인 도형의 넓이를 구하는 것이다[footnote][위키피디아](https://ko.wikipedia.org/wiki/%EC%A0%81%EB%B6%84)[/footnote]<br>
    ![적분이미지](Integration.svg)<br>
    ![적분이미지2](Integration%20-%20TrapezoidalRule1.png)<br><br>
2. 수치 적분이란 연속된 데이터 포인트를 적당한 하나의 함수`function`를 포함한 급수합으로 근사하여 적분하는 것을 말한다.<br>
    ![적분이미지3](Integration%20-%20SimpsonOneDivThree.png)<br>
3. 앞선 글과는 달리, 연속된 데이터 `(X1, X2, X3...), (Y1, Y2, Y3...)`를 곡선 함수로 근사하고 적분하는 것을 `Simpson rule`이라고 한다.
    - 1/3 rule은 이차함수로 근사한다.
    - 3/8 rule은 삼차함수로 근사한다.
4. 아래 코드는 3/8 rule에 대한 코드이다.

<br>

<h2 id="code">Code</h2>

<br>

```csharp
using Peponi.Math.Extensions;

namespace Peponi.Math.Integration;

public static class Simpson3over8
{
    /// <summary>
    /// 심슨 3/8은 잘 쓰이지 않음.
    /// </summary>
    public static double Integrate(List<double> xs, List<double> ys)
    {
        if (xs.Count != ys.Count) throw new ArgumentException($"Input value count mismatched. xs : {xs.Count}, ys : {ys.Count}");
        else if ((xs.Count - 1) % 3 != 0) throw new ArgumentException("\"Array length - 1\" must be multiple of 3");
        else if (!xs.IsIntervalUniform()) throw new ArgumentException("X axis array's interval should be uniform");

        double intervalH = (xs.Max() - xs.Min()) / (xs.Count - 1) * 3 / 8;

        double yTotal = 0;
        for (int i = 0; i < ys.Count - 2; i += 3)
        {
            yTotal += ys[i] + 3 * ys[i + 1] + 3 * ys[i + 2] + ys[i + 3];
        }

        double result = intervalH * yTotal;

        return result;
    }

    public static double Integrate(Func<double, double> fx, double lowLimit, double upperLimit, int intervalCount)
    {
        if (lowLimit > upperLimit) throw new ArgumentException($"Low limit ({lowLimit}) could not bigger than upper limit ({upperLimit})");
        else if (fx == null) throw new ArgumentNullException("fx is null");
        else if ((intervalCount) % 3 != 0) throw new ArgumentException("Interval count must be multiple of 3");

        double deltaX = (upperLimit - lowLimit) / intervalCount;
        double yTotal = 0;

        for (int i = 0; i < intervalCount - 2; i += 3)
        {
            yTotal += fx(lowLimit + deltaX * i) + 3 * fx(lowLimit + deltaX * (i + 1)) + 3 * fx(lowLimit + deltaX * (i + 2)) + fx(lowLimit + deltaX * (i + 3));
        }

        return yTotal * deltaX * 3 / 8;
    }
}
```

```cs
namespace Peponi.Math.Extensions;

public static class CollectionExtension
{
    public static bool IsIntervalUniform(this IEnumerable<double> values)
    {
        double interval = values.ElementAt(1) - values.ElementAt(0);

        for (int i = 0; i < values.Count() - 1; i++)
        {
            if (values.ElementAt(i + 1) - values.ElementAt(i) != interval) return false;
        }

        return true;
    }
}
```

<br>

## 참조 자료

<br>

- [Peponi Library - Simpson3over8](https://github.com/peponi-paradise/Peponi/blob/Development/Peponi/Peponi.Math/Integrations/Simpson3over8.cs)
- [Peponi Library - Collection extension](https://github.com/peponi-paradise/Peponi/blob/Development/Peponi/Peponi.Math/Extensions/Collection.cs)
- [Simpson's rule](https://en.wikipedia.org/wiki/Simpson%27s_rule)