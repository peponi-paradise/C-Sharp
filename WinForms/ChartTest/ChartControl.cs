using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ATIK
{
    public partial class ChartControl : UserControl
    {
        public ChartControl()
        {
            InitializeComponent();

            chart.MouseWheel += Chart_MouseWheel;   //마우스 휠 확대 축소
            chart.MouseMove += Chart_MouseMove;     //그래프 데이터 트래킹

            numeric_Min.Minimum = -100000000;
            numeric_Min.Maximum = 100000000;
            numeric_Max.Minimum = -100000000;
            numeric_Max.Maximum = 100000000;

            ChartClear();
        }

        #region 데이터

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        bool IsUseTimeChart = false;

        public SeriesChartType LineType = SeriesChartType.Line;

        [Description("Scale Min Y - double"), Category("Chart Property")]
        public double ScaleMinY
        {
            get
            {
                return chart.ChartAreas[0].AxisY.Minimum;
            }
            set
            {
                chart.ChartAreas[0].AxisY.Minimum = value;
            }
        }
        [Description("Scale Max Y - double"), Category("Chart Property")]
        public double ScaleMaxY
        {
            get
            {
                return chart.ChartAreas[0].AxisY.Maximum;
            }
            set
            {
                chart.ChartAreas[0].AxisY.Maximum = value;
            }
        }

        //[Description("Scale Min X - double"), Category("Chart Property")]
        //public double ScaleMinX
        //{
        //    get
        //    {
        //        return chart.ChartAreas[0].AxisX.Minimum;
        //    }
        //    set
        //    {
        //        chart.ChartAreas[0].AxisX.Minimum = value;
        //    }
        //}
        //[Description("Scale Max X - double"), Category("Chart Property")]
        //public double ScaleMaxX
        //{
        //    get
        //    {
        //        return chart.ChartAreas[0].AxisX.Maximum;
        //    }
        //    set
        //    {
        //        chart.ChartAreas[0].AxisX.Maximum = value;
        //    }
        //}

        [Description("Line Thickness - int"), Category("Chart Property")]
        public int LineThickness { get; set; } = 1;

        #endregion

        #region 이벤트 처리
        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    var pos = e.Location;
                    if (prevPosition.HasValue && pos == prevPosition.Value)
                        return;
                    tooltip.RemoveAll();
                    prevPosition = pos;
                    var results = chart.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
                    foreach (var result in results)
                    {
                        if (result.ChartElementType == ChartElementType.DataPoint)
                        {
                            var Position = result.Object as DataPoint;
                            if(IsUseTimeChart==false)
                            tooltip.Show("X=" + Position.XValue + ", Y=" + Position.YValues[0], chart, pos.X, pos.Y);
                            else
                                tooltip.Show("X=" + DateTime.FromOADate(Position.XValue).ToString() + ", Y=" + Position.YValues[0], chart, pos.X, pos.Y);

                        }
                    }
                }
                catch
                { }
            }));
        }

        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                var chart = (Chart)sender;
                var xAxis = chart.ChartAreas[0].AxisX;
                var yAxis = chart.ChartAreas[0].AxisY;

                try
                {
                    if (e.Delta < 0) // Scrolled down.
                    {
                        xAxis.ScaleView.ZoomReset();
                        yAxis.ScaleView.ZoomReset();
                    }
                    else if (e.Delta > 0) // Scrolled up.
                    {
                        var xMin = xAxis.ScaleView.ViewMinimum;
                        var xMax = xAxis.ScaleView.ViewMaximum;
                        var yMin = yAxis.ScaleView.ViewMinimum;
                        var yMax = yAxis.ScaleView.ViewMaximum;

                        var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                        var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                        var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                        var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                        xAxis.ScaleView.Zoom(posXStart, posXFinish);
                        yAxis.ScaleView.Zoom(posYStart, posYFinish);
                    }
                }
                catch { }
            }));
        }

        private void numeric_Min_ValueChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                if (numeric_Max.Value <= numeric_Min.Value)
                {
                    return;
                }
                try
                {
                    ScaleMinY = (double)numeric_Min.Value;
                }
                catch
                {
                    return;
                }
            }));
        }

        private void numeric_Max_ValueChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                if (numeric_Max.Value <= numeric_Min.Value)
                {
                    return;
                }
                try
                {
                    ScaleMaxY = (double)numeric_Max.Value;
                }
                catch
                {
                    return;
                }
            }));
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            ChartClear();
        }

        private void comboBox_ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_ChartType.SelectedItem)
            {
                case "Line":
                    LineType = SeriesChartType.Line;
                    break;
                case "Column":
                    LineType = SeriesChartType.Column;
                    break;
                case "Point":
                    LineType = SeriesChartType.Point;
                    break;
            }
        }

        #endregion

        #region functions

        public bool AddData_Array(string SeriesName, double[] XDataArray, double[] YDataArray)     //그래프 1개 추가
        {
            bool IsSuccess = false;

            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Series DataSeries = chart.Series.FindByName(SeriesName);

                    if (DataSeries == null)
                    {
                        ChartSettingCheck();

                        DataSeries = chart.Series.Add(SeriesName);
                        DataSeries.ChartType = LineType;
                        DataSeries.BorderWidth = LineThickness;

                        if (ChartRangeCheck(XDataArray, YDataArray) == false)   //Scale Min,Max값 잘못 설정 되어 있을 경우(ex - Min이 Max보다 크거나 같은 경우) 오토스케일로 돌림
                        {
                            chart.Series.Remove(DataSeries);
                            IsSuccess = false;
                        }
                    }

                    //기존 데이터가 있을 경우 덮어쓰기
                    List<double> InputX = XDataArray.ToList();
                    List<double> InputY = YDataArray.ToList();
                    DataSeries.Points.DataBindXY(InputX, InputY);
                    IntervalCheck();
                    IsSuccess = true;
                }));

                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }

        public bool AddData_Array(string SeriesName, List<double> XDataArray, List<double> YDataArray,bool UseOADate=false)       //그래프 1개 추가
        {
            bool IsSuccess = false;
            IsUseTimeChart = UseOADate;

            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Series DataSeries = chart.Series.FindByName(SeriesName);

                    if (DataSeries == null)
                    {
                        ChartSettingCheck();

                        DataSeries = chart.Series.Add(SeriesName);
                        DataSeries.ChartType = LineType;
                        DataSeries.BorderWidth = LineThickness;

                        if (ChartRangeCheck(XDataArray, YDataArray) == false)   //Scale Min,Max값 잘못 설정 되어 있을 경우(ex - Min이 Max보다 크거나 같은 경우) 오토스케일로 돌림
                        {
                            chart.Series.Remove(DataSeries);
                            IsSuccess = false;
                        }
                    }

                    //기존 데이터가 있을 경우 덮어쓰기
                    if(UseOADate==false)
                    DataSeries.Points.DataBindXY(XDataArray, YDataArray);
                    else
                    {
                        chart.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
                        List<DateTime> XDataArrayTime = new List<DateTime>();
                        for(int i=0;i<XDataArray.Count;i++)
                        {
                            XDataArrayTime.Add(DateTime.FromOADate(XDataArray[i]));
                        }
                        DataSeries.Points.DataBindXY(XDataArrayTime, YDataArray);
                    }
                        
                    IntervalCheck();
                    IsSuccess = true;
                }));

                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }

        public bool AddData_Single(string SeriesName, double XData, double YData)      //요소 1개 추가
        {
            bool IsSuccess = false;

            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Series DataSeries = chart.Series.FindByName(SeriesName);

                    if (DataSeries == null)
                    {
                        ChartSettingCheck();

                        DataSeries = chart.Series.Add(SeriesName);
                        DataSeries.ChartType = LineType;
                        DataSeries.BorderWidth = LineThickness;

                        if (ChartRangeCheck(XData, YData) == false)   //Scale Min,Max값 잘못 설정 되어 있을 경우(ex - Min이 Max보다 크거나 같은 경우) 오토스케일로 돌림
                        {
                            chart.Series.Remove(DataSeries);
                            IsSuccess = false;
                        }
                    }

                    DataSeries.Points.AddXY(XData, YData);
                    IntervalCheck();
                    IsSuccess = true;
                }));

                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }

        public bool DeleteData_Array(string SeriesName)      //한 그래프 전체 제거
        {
            bool IsSuccess = false;
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Series DataSeries = chart.Series.FindByName(SeriesName);
                    if (DataSeries != null)
                    {
                        chart.Series.Remove(DataSeries);
                        IsSuccess = true;
                    }
                    else
                        IsSuccess = false;  //non-exist
                }));
                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }

        public bool DeleteData_Single(string SeriesName, double YData)    //일치하는 첫 Y좌표의 요소 제거
        {
            bool IsSuccess = false;
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Series DataSeries = chart.Series.FindByName(SeriesName);
                    if (DataSeries != null)
                    {
                        DataPoint point = DataSeries.Points.FindByValue(YData);
                        bool ok = DataSeries.Points.Remove(point);
                        if (ok == true)
                        { IsSuccess = true; IntervalCheck(); }
                        else IsSuccess = false;
                    }
                    else
                        IsSuccess = false;  //non-exist
                }));
                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }

        public bool DeleteData_Single_EndPoint(string SeriesName)    //맨 끝의 요소 제거
        {
            bool IsSuccess = false;
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Series DataSeries = chart.Series.FindByName(SeriesName);
                    if (DataSeries != null)
                    {
                        DataSeries.Points.RemoveAt(DataSeries.Points.Count - 1);
                        IntervalCheck();

                        IsSuccess = true;
                    }
                    else
                        IsSuccess = false;  //non-exist
                }));
                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }

        public void ChartClear()     //지우개
        {
            if (this.Handle != null)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    chart.Series.Clear();
                    chart.ChartAreas.Clear();
                    chart.ChartAreas.Add("Area");   //그래프 그릴 공간 1개는 필요
                    //chart.ChartAreas[0].AxisX.LabelStyle.Format = "0.0000";
                    //chart.ChartAreas[0].AxisY.LabelStyle.Format = "0.0000";
                }));
            }
            else
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.ChartAreas.Add("Area");
                //chart.ChartAreas[0].AxisX.LabelStyle.Format = "0.0000";
                //chart.ChartAreas[0].AxisY.LabelStyle.Format = "0.0000";
            }
        }

        public void ChartSettingCheck()
        {
            if (LineThickness <= 0) LineThickness = 1;
        }

        public bool ChartRangeCheck(double[] XDataArray, double[] YDataArray)
        {
            if (XDataArray == null || XDataArray.Length < 1) return false;
            if (YDataArray == null || YDataArray.Length < 1) return false;

            List<double> DataArray = YDataArray.ToList();

            this.Invoke(new MethodInvoker(delegate ()
            {
                bool IsInvalidate = false;
                if (ScaleMaxY < DataArray.Max())
                {
                    ScaleMaxY = DataArray.Max();
                    IsInvalidate = true;
                }
                if (ScaleMinY > DataArray.Min())
                {
                    ScaleMinY = DataArray.Min();
                    IsInvalidate = true;
                }
                if (IsInvalidate == true) chart.Invalidate();
            }));

            return true;
        }

        public bool ChartRangeCheck(List<double> XDataArray, List<double> YDataArray)
        {
            if (XDataArray == null || XDataArray.Count < 1) return false;
            if (YDataArray == null || YDataArray.Count < 1) return false;

            this.Invoke(new MethodInvoker(delegate ()
            {
                bool IsInvalidate = false;
                if (ScaleMaxY < YDataArray.Max())
                {
                    ScaleMaxY = YDataArray.Max();
                    IsInvalidate = true;
                }
                if (ScaleMinY > YDataArray.Min())
                {
                    ScaleMinY = YDataArray.Min();
                    IsInvalidate = true;
                }
                if (IsInvalidate == true) chart.Invalidate();
            }));

            return true;
        }

        public bool ChartRangeCheck(double XData, double YData)
        {
            if (XData == double.NaN) return false;
            if (YData == double.NaN) return false;

            bool IsInvalidate = false;
            if (YData > ScaleMaxY)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ScaleMaxY = YData;
                    IsInvalidate = true;
                }));
            }
            if (YData < ScaleMinY)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ScaleMinY = YData;
                    IsInvalidate = true;
                }));
            }
            if (IsInvalidate == true)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    chart.Invalidate();
                }));
            }

            return true;
        }

        public bool SetScaleRangeY(double YMin, double YMax)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ScaleMaxY = YMax;
                    ScaleMinY = YMin;
                    chart.Invalidate();
                    IntervalCheck();
                }));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetScaleRangeX(double XMin, double XMax)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    chart.ChartAreas[0].AxisX.Minimum = XMin;
                    chart.ChartAreas[0].AxisX.Maximum = XMax;
                    chart.Invalidate();
                }));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetScaleDefaultY()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ScaleMaxY = double.NaN;
                    ScaleMinY = double.NaN;
                    chart.Invalidate();
                    IntervalCheck();
                }));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetScaleDefaultX()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    chart.ChartAreas[0].AxisX.Minimum = double.NaN;
                    chart.ChartAreas[0].AxisX.Maximum = double.NaN;
                    chart.Invalidate();
                }));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// type - Line, Column, Point
        /// </summary>
        /// <param name="SeriesName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool SetLineType(string SeriesName, string type)
        {
            try
            {
                switch (type)
                {
                    case "Line":
                        LineType = SeriesChartType.Line;
                        break;
                    case "Column":
                        LineType = SeriesChartType.Column;
                        break;
                    case "Point":
                        LineType = SeriesChartType.Point;
                        break;
                    default:
                        return false;
                }
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Series series = chart.Series.FindByName(SeriesName);
                    series.ChartType = LineType;
                    chart.Invalidate();
                }));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SetLineColor(string SeriesName, Color color)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                Series series = chart.Series.FindByName(SeriesName);
                series.Color = color;
                chart.Invalidate();
            }));
        }

        private void IntervalCheck()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                chart.Update();
                double value = (chart.ChartAreas[0].AxisY.Maximum - chart.ChartAreas[0].AxisY.Minimum) / 6;
                chart.ChartAreas[0].AxisY.Interval = value;
                chart.Invalidate();
            }));
        }

        #endregion
    }
}
