<h1 id="title">Integration - Trapezoidal Rule</h1>

<h2 id="intro">Introduction</h2>

1. 적분이란 정의된 함수의 그래프와 그 구간으로 둘러싸인 도형의 넓이를 구하는 것이다[footnote][위키피디아](https://ko.wikipedia.org/wiki/%EC%A0%81%EB%B6%84)[/footnote]<br>
    ![적분이미지](Integration.svg)<br>
    ![적분이미지2](Integration%20-%20TrapezoidalRule1.png)<br><br>
2. 수치 적분이란 연속된 데이터 포인트를 적당한 하나의 함수`function`를 포함한 급수합으로 근사하여 적분하는 것을 말한다.<br>
    ![적분이미지3](Integration%20-%20TrapezoidalRule3.png)<br>
3. 위 이미지와 같이, 연속된 데이터 `(X1, X2, X3...), (Y1, Y2, Y3...)`를 적당한 하나의 함수로 근사하고, 사다리꼴 형태의 면적을 구하여 적분하는 것을 `Trapezoidal Rule`이라고 한다.
4. 자세한 구현은 아래 코드를 참조

<br>

<h2 id="code">Code</h2>

<br>

```csharp
// 23.08.31 수정 : 
// 1. List형 integration 음수 값을 가져도 적분이 가능하도록
// 2. 함수형 integration 메서드 추가

namespace Peponi.Math.Integration;

public static class Trapezoidal
{
    /// <summary>
    /// 사다리꼴은 이산값 직선으로 이을수록 정확성 올라감
    /// </summary>
    public static double Integrate(List<double> xs, List<double> ys)
    {
        if (xs.Count != ys.Count) throw new ArgumentException($"Input value count mismatched. xs : {xs.Count}, ys : {ys.Count}");
        else if (xs.Count < 2) throw new ArgumentException("Required at least 2 points");

        double result = 0;

        for (int i = 0; i < xs.Count - 1; i++)
        {
            result += (xs[i + 1] - xs[i]) * (ys[i + 1] + ys[i]) / 2;
        }

        return result;
    }

    public static double Integrate(Func<double, double> fx, double lowLimit, double upperLimit, int intervalCount)
    {
        if (lowLimit > upperLimit) throw new ArgumentException($"Low limit ({lowLimit}) could not bigger than upper limit ({upperLimit})");
        else if (fx == null) throw new ArgumentNullException("fx is null");

        double deltaX = (upperLimit - lowLimit) / intervalCount;
        double sum = 0;

        for (int i = 0; i < intervalCount; i++)
        {
            sum += fx(lowLimit + deltaX * i) + fx(lowLimit + deltaX * (i + 1));
        }

        return sum * deltaX / 2;
    }
}
```

<br>

## 참조 자료

<br>

- [Peponi Library](https://github.com/peponi-paradise/Peponi/blob/Development/Peponi/Peponi.Math/Integrations/Trapezoidal.cs)
- [Trapezoidal rule](https://en.wikipedia.org/wiki/Trapezoidal_rule)