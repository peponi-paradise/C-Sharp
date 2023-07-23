using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericalIntegration
{
    public static class Integration
    {
        /// <summary>
        /// 사다리꼴은 이산값 직선으로 이을수록 정확성 올라감
        /// float으로도 대부분의 정밀도 요구 만족
        /// </summary>
        /// <param name="XList"></param>
        /// <param name="YList"></param>
        /// <returns>Calculated value</returns>
        public static float TrapezoidalRule(List<float> xList, List<float> yList)
        {
            float result = 0;

            for (int i = 0; i < yList.Count; i++) yList[i] = yList[i] > 0 ? yList[i] : 0;   // 0 이하 값 일괄 0으로 처리

            // 끝점 제외한 나머지 계산
            for (int i = 0; i < xList.Count - 1; i++) result += Math.Abs(xList[i + 1] - xList[i]) * Math.Abs(yList[i + 1] + yList[i]) / 2;

            return result;
        }
    }
}