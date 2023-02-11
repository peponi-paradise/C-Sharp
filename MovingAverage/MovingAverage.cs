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