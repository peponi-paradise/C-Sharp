<h1 id="title">Integration - Simpson 1/3 rule</h1>

<h2 id="intro">Introduction</h2>

<br>

1. 적분이란 정의된 함수의 그래프와 그 구간으로 둘러싸인 도형의 넓이를 구하는 것이다[footnote][위키피디아](https://ko.wikipedia.org/wiki/%EC%A0%81%EB%B6%84)[/footnote]<br>
    ![적분이미지](Integration.svg)<br>
    ![적분이미지2](Integration%20-%20TrapezoidalRule1.png)<br><br>
2. 수치 적분이란 연속된 데이터 포인트를 적당한 하나의 함수`function`를 포함한 급수합으로 근사하여 적분하는 것을 말한다.<br>
    ![적분이미지3](Integration%20-%20SimpsonOneDivThree.png)<br>
3. 앞선 글과는 달리, 연속된 데이터 `(X1, X2, X3...), (Y1, Y2, Y3...)`를 곡선 함수로 근사하고 적분하는 것을 `Simpson rule`이라고 한다.
    - 1/3 rule은 이차함수로 근사한다.
    - 3/8 rule은 삼차함수로 근사한다.
4. 아래 코드는 1/3 rule에 대한 코드이다.

<br>

<h2 id="code">Code</h2>

<br>

```csharp
// 23.08.31 수정 : 
// 1. List형 integration 음수 값을 가져도 적분이 가능하도록
// 2. 함수형 integration 메서드 추가

using Peponi.Math.Extensions;

namespace Peponi.Math.Integration;

public static class Simpson1over3
{
    /// <summary>
    /// 심슨 1/3오더는 곡선구간이고 각 구간이 짧을 수록 정확, 반드시 구간의 수가 짝수여야 함 <br/>
    /// 각 구간의 길이는 일정해야 함
    /// </summary>
    public static double Integrate(List<double> xs, List<double> ys)
    {
        if (xs.Count != ys.Count) throw new ArgumentException($"Input value count mismatched. xs : {xs.Count}, ys : {ys.Count}");
        else if (xs.Count < 3) throw new ArgumentException("Required at least 3 points");
        else if (!xs.Count.IsOdd()) throw new ArgumentException("Input array's count should be odd");
        else if (!xs.IsIntervalUniform()) throw new ArgumentException("X axis array's interval should be uniform");

        double sumOdd = 0;
        double sumEven = 0;
        double intervalH = (xs.Max() - xs.Min()) / (xs.Count - 1) / 3;

        for (int i = 1; i < ys.Count - 1; i++)
        {
            if (i.IsEven()) sumEven += ys[i];
            else sumOdd += ys[i];
        }

        sumOdd = 4 * sumOdd;
        sumEven = 2 * sumEven;

        double yTotal = ys[0] + ys[ys.Count - 1] + sumOdd + sumEven;
        double result = intervalH * yTotal;

        return result;
    }

    public static double Integrate(Func<double, double> fx, double lowLimit, double upperLimit, int intervalCount)
    {
        if (lowLimit > upperLimit) throw new ArgumentException($"Low limit ({lowLimit}) could not bigger than upper limit ({upperLimit})");
        else if (fx == null) throw new ArgumentNullException("fx is null");
        else if (intervalCount.IsOdd()) throw new ArgumentException("Interval count should be even");

        double deltaX = (upperLimit - lowLimit) / intervalCount;
        double sumOdd = 0;
        double sumEven = 0;

        for (int i = 1; i < intervalCount; i++)
        {
            if (i.IsEven()) sumEven += fx(lowLimit + deltaX * i);
            else sumOdd += fx(lowLimit + deltaX * i);
        }

        sumOdd = 4 * sumOdd;
        sumEven = 2 * sumEven;

        double yTotal = fx(lowLimit) + fx(upperLimit) + sumOdd + sumEven;

        return yTotal * deltaX / 3;
    }
}
```

```cs
namespace Peponi.Math.Extensions;

public static class IntegerExtension
{
    public static bool IsOdd(this int value)
    {
        return value % 2 == 1;
    }
}

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

- [Peponi Library - Simpson1over3](https://github.com/peponi-paradise/Peponi/blob/Development/Peponi/Peponi.Math/Integrations/Simpson1over3.cs)
- [Peponi Library - Integer extension](https://github.com/peponi-paradise/Peponi/blob/Development/Peponi/Peponi.Math/Extensions/Integer.cs)
- [Peponi Library - Collection extension](https://github.com/peponi-paradise/Peponi/blob/Development/Peponi/Peponi.Math/Extensions/Collection.cs)
- [Simpson's rule](https://en.wikipedia.org/wiki/Simpson%27s_rule)