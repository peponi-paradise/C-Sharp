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