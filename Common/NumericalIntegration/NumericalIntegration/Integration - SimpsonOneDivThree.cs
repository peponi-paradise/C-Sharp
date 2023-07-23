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