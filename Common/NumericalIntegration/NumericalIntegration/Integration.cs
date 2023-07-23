using System;
using System.Collections.Generic;
using System.Linq;

namespace Integration
{
    public static class Integration
    {
        public enum IntegrationMethod
        {
            Trapezoidal = 1,
            MidPoint = 2,
            SimpsonOneDivThree = 3,
        }

        public static float Integrate(IntegrationMethod Method, List<float> Xs, List<float> Ys)
        {
            try
            {
                return Method switch
                {
                    IntegrationMethod.Trapezoidal => TrapezoidalRule(Xs, Ys),
                    IntegrationMethod.MidPoint => MidpointRule(Xs, Ys),
                    IntegrationMethod.SimpsonOneDivThree => SimpsonOneDivThree(Xs, Ys),
                    _ => -999,
                };
            }
            catch
            {
                return -999;
            }
        }

        /// <summary>
        /// 사다리꼴은 이산값 직선으로 이을수록 정확성 올라감
        /// float으로도 대부분의 정밀도 요구 만족
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <returns>Calculated value</returns>
        static float TrapezoidalRule(List<float> Xs, List<float> Ys)
        {
            float result = 0;

            for (int i = 0; i < Ys.Count; i++) Ys[i] = Ys[i] > 0 ? Ys[i] : 0;   // 0 이하 값 일괄 0으로 처리

            // 끝점 제외한 나머지 계산
            for (int i = 0; i < Xs.Count - 1; i++) result += Math.Abs(Xs[i + 1] - Xs[i]) * Math.Abs(Ys[i + 1] + Ys[i]) / 2;

            return result;
        }

        /// <summary>
        /// 중점공식은 바 그래프 형태에서 가장 정확성 높음
        /// float으로도 대부분의 정밀도 요구 만족
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <returns>Calculated value</returns>
        static float MidpointRule(List<float> Xs, List<float> Ys)
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
        public static bool IsEven(int count) => count % 2 == 0 ? true : false;
    }
}