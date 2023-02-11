<h1 id="title">Moving average</h1>

<h2 id="intro">Introduction</h2>


- 이동 평균`Moving average`이란, 전체 집합`U` 안에서 일정 크기의 부분 집합`u`가 이동하며 산출한 평균이다.<br>
    <details>
        <summary>위키 백과</summary>
        <p>
        &nbsp;&nbsp;In statistics, a moving average (rolling average or running average) is a calculation to analyze data points by creating a series of averages of different subsets of the full data set. It is also called a moving mean (MM) or rolling mean and is a type of finite impulse response filter. Variations include: simple, cumulative, or weighted forms.
        </p>
        <p>
        &nbsp;&nbsp;Given a series of numbers and a fixed subset size, the first element of the moving average is obtained by taking the average of the initial fixed subset of the number series. Then the subset is modified by "shifting forward"; that is, excluding the first number of the series and including the next value in the subset.
        </p>
        <p>
        &nbsp;&nbsp;A moving average is commonly used with time series data to smooth out short-term fluctuations and highlight longer-term trends or cycles. The threshold between short-term and long-term depends on the application, and the parameters of the moving average will be set accordingly. For example, it is often used in technical analysis of financial data, like stock prices, returns or trading volumes. It is also used in economics to examine gross domestic product, employment or other macroeconomic time series. Mathematically, a moving average is a type of convolution and so it can be viewed as an example of a low-pass filter used in signal processing. When used with non-time series data, a moving average filters higher frequency components without any specific connection to time, although typically some kind of ordering is implied. Viewed simplistically it can be regarded as smoothing the data.
        </p>
        <p><a href="https://en.wikipedia.org/wiki/Moving_average">[위키 백과]</a></p>
    </details>
- 이동 평균에 이용되는 방법은 주로 `Simple moving average`와 `Exponential moving average`가 있다. (여기서는 Simple moving average만 서술한다)
- `Simple moving average`의 계산 방법 및 결과에 대해서는 아래 이미지와 같이 표현할 수 있다.

[이미지]
<br>

- 위 그림과 같이, 일정 크기를 가진 `u`가 `U` 안에서 움직이며 계속해서 평균값을 산출한다.
- 이를 식으로 표현하면 아래와 같다.

[이미지]

<br><br>

<h2 id="code">Code</h2>


```cs
using System;
using System.Collections.Generic;

namespace MovingAverageTest
{
    public class MovingAverage<T>
    {
        List<T> MovingAverageData;
        int WindowSize;

        /// <summary>
        /// Initialize Moving average
        /// </summary>
        /// <param name="WindowSize">int, Set subset size</param>
        public MovingAverage(int WindowSize)
        {
            MovingAverageData = new List<T>();
            this.WindowSize = WindowSize;
        }

        /// <summary>
        /// Get simple moving average data
        /// </summary>
        /// <param name="InputData">Any type of numerical data</param>
        /// <returns>Success or fail</returns>
        public bool Averaging(T InputData, out T OutputData)
        {
            bool rtn = true;
            OutputData = default(T);
            try
            {
                if (MovingAverageData.Count >= WindowSize) MovingAverageData.RemoveAt(0);

                MovingAverageData.Add(InputData);

                double AverageData = 0;

                //Temp. double change for calculate
                foreach (var data in MovingAverageData) AverageData += (double)Convert.ChangeType(data, typeof(double));
                AverageData /= MovingAverageData.Count;

                OutputData = (T)Convert.ChangeType(AverageData, typeof(T));
            }
            catch
            {
                rtn = false;
            }
            return rtn;
        }
    }
}
```