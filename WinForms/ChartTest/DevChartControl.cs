using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ATIK
{
    public partial class DevChartControl : DevExpress.XtraEditors.XtraUserControl
    {
        /*-------------------------------------------
         * 
         *      Design time properties
         * 
         -------------------------------------------*/

        [Description("Line Thickness - int"), Category("Chart Property")]
        public int LineThickness { get; set; } = 1;

        [Description("Show Legend"), Category("Chart Property")]
        public bool IsShowLegend { get; set; } = true;

        [Description("Show Date"), Category("Chart Property")]
        public bool IsShowDate { get; set; } = false;

        [Description("Data Clearing Chart"), Category("Chart Property")]
        public bool IsClearingChart { get; set; } = false;

        /*-------------------------------------------
         * 
         *      Events
         * 
         -------------------------------------------*/

        public delegate void DoubleClickEventHandler();
        public event DoubleClickEventHandler GraphDoubleClick;

        /*-------------------------------------------
         * 
         *      Public members
         * 
         -------------------------------------------*/

        public ViewType LineType = ViewType.Line;

        /*-------------------------------------------
         * 
         *      Private members
         * 
         -------------------------------------------*/

        readonly SynchronizationContext SyncContext;
        object DataLocker = new object();
        GraphPurpose Purpose;
        SeriesPoint[] Points;
        double AxisScale = 0.00000001;

        /*-------------------------------------------
         * 
         *      Constructor / Destructor
         * 
         -------------------------------------------*/

        public DevChartControl()
        {
            InitializeComponent();
            chart.UseDirectXPaint = true;
            while (true) { if (this.Handle != null) break; }
            SyncContext = SynchronizationContext.Current;

            chart.DoubleClick += ChartDoubleClick;
        }

        ~DevChartControl()
        {
        }

        public void dispose()
        {
            this.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /*-------------------------------------------
         * 
         *      Event functions
         * 
         -------------------------------------------*/

        private void ChartDoubleClick(object sender, EventArgs e) => SyncContext.Post(delegate
        {
            GraphDoubleClick?.Invoke();
        }, null);

        private void button_Reset_Click(object sender, EventArgs e)
        {
            dynamic diagram = null;
            if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
            else diagram = (XYDiagram)chart.Diagram;
            if (diagram != null)
            {
                diagram.ResetZoom();
                diagram.AxisY.VisualRange.Auto = true;
                ResetScale();
                button_Reset.Visible = false;
            }
        }

        private void button_YPlus_Click(object sender, EventArgs e)
        {
            try
            {
                dynamic diagram = null;
                if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
                else diagram = (XYDiagram)chart.Diagram;
                if (diagram != null)
                {
                    if (button_Reset.Visible == false) button_Reset.Visible = true;
                    AxisScale = AxisScale / 5;
                    diagram.AxisY.VisualRange.Auto = false;
                    diagram.AxisY.VisualRange.MinValue = 0;
                    diagram.AxisY.VisualRange.MaxValue = AxisScale;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(LogType.Error, "Invalid operation - Chart : " + ex.ToString());
                MsgFrm_NofityOnly frm = new MsgFrm_NofityOnly("Invalid operation");
                frm.ShowDialog();
            }
        }

        private void button_YMinus_Click(object sender, EventArgs e)
        {
            try
            {
                dynamic diagram = null;
                if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
                else diagram = (XYDiagram)chart.Diagram;
                if (diagram != null)
                {
                    if (button_Reset.Visible == false) button_Reset.Visible = true;
                    AxisScale = AxisScale * 5;
                    diagram.AxisY.VisualRange.Auto = false;
                    diagram.AxisY.VisualRange.MinValue = 0;
                    diagram.AxisY.VisualRange.MaxValue = AxisScale;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(LogType.Error, "Invalid operation - Chart : " + ex.ToString());
                MsgFrm_NofityOnly frm = new MsgFrm_NofityOnly("Invalid operation");
                frm.ShowDialog();
            }
        }

        private void chart_Zoom(object sender, ChartZoomEventArgs e)
        {
            if (button_Reset.Visible == false) button_Reset.Visible = true;
        }

        /*-------------------------------------------
         * 
         *      Public functions
         * 
         -------------------------------------------*/

        public bool AddPoint<T>(string seriesName, double X, T Y, bool useOADate = false, bool isSecondaryAxis = false)      //요소 1개 추가
        {
            if (IsValidYAxis<T>(default(T)) == false) return false;

            bool isSuccess = true;
            try
            {
                SetSeries(seriesName, useOADate, isSecondaryAxis);

                SyncContext.Post(delegate
                {
                    double yData = double.Parse(Y.ToString());

                    if (useOADate) chart.Series[seriesName].Points.AddPoint(DateTime.FromOADate(X), yData);
                    else chart.Series[seriesName].Points.AddPoint(X, yData);
                }, null);
            }
            catch (Exception e)
            {
                isSuccess = false;
                Log.WriteLog(LogType.Exception, e.ToString());
            }
            return isSuccess;
        }

        /// <summary>
        /// 그래프 1개 추가<br/>
        /// Dev chart의 IsClearingChart를 true로 설정 시 전체 데이터 지우고 그리고 반복
        /// </summary>
        /// <typeparam name="T">int, float, double</typeparam>
        /// <param name="seriesName"></param>
        /// <param name="X">OADate로 인해 강제 double</param>
        /// <param name="Y"></param>
        /// <param name="useOADate">X축을 시간으로 표시</param>
        /// <param name="isSecondaryAxis"></param>
        /// <returns>Success or fail</returns>
        public bool AddPoints<T>(string seriesName, List<double> X, List<T> Y, bool useOADate = false, bool isSecondaryAxis = false)
        {
            if (X.Count != Y.Count) return false;
            if (IsValidYAxis<T>(default(T)) == false) return false;

            bool isSuccess = true;
            try
            {
                SetSeries(seriesName, useOADate, isSecondaryAxis);

                SyncContext.Post(delegate
                {
                    double yData = 0;
                    Points = new SeriesPoint[X.Count];

                    if (useOADate)
                    {
                        for (int i = 0; i < X.Count; i++)
                        {
                            yData = double.Parse(Y[i].ToString());
                            Points[i] = new SeriesPoint(DateTime.FromOADate(X[i]), yData);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < X.Count; i++)
                        {
                            yData = double.Parse(Y[i].ToString());
                            Points[i] = new SeriesPoint(X[i], yData);
                        }
                    }

                    //아래처럼 표현할 수도 있지만, 매번 OADate를 확인하기 때문에 연산량에서 비효율적. 코드가 늘어나도 한번만 확인하여 연산량 줄일 수 있게 가자
                    //for (int i = 0; i < XDataArray.Count; i++) points[i] = UseOADate ? new SeriesPoint(DateTime.FromOADate(XDataArray[i]), double.Parse(YDataArray[i].ToString())) : new SeriesPoint(XDataArray[i], double.Parse(YDataArray[i].ToString()));

                    if (IsClearingChart) chart.GetSeriesByName(seriesName).Points.Clear();
                    chart.GetSeriesByName(seriesName).Points.AddRange(Points);
                }, null);
            }
            catch (Exception e)
            {
                Log.WriteLog(LogType.Exception, "DevChartControl - AddPoints exception : " + e.ToString());
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool DeleteFirstPoint(string seriesName)
        {
            bool isSuccess = true;
            try
            {
                SyncContext.Post(delegate
                {
                    chart.Series[seriesName].Points.RemoveAt(0);
                }, null);
            }
            catch (Exception e)
            {
                Log.WriteLog(LogType.Exception, "DeleteFirstPoint exception : " + e.ToString());
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool DeleteFirstPoints(List<string> seriesNames)
        {
            bool isSuccess = true;
            try
            {
                SyncContext.Post(delegate
                {
                    foreach (var name in seriesNames) chart.Series[name].Points.RemoveAt(0);
                }, null);
            }
            catch (Exception e)
            {
                Log.WriteLog(LogType.Exception, "DeleteEveryFirstPoint exception : " + e.ToString());
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool DeletePoints(string seriesName, int count)
        {
            bool isSuccess = true;
            try
            {
                SyncContext.Post(delegate
                {
                    chart.Series[seriesName].Points.RemoveRange(0, count);
                }, null);
            }
            catch (Exception e)
            {
                isSuccess = false;
                Log.WriteLog(LogType.Exception, "DeletePoints exception : " + e.ToString());
            }
            return isSuccess;
        }

        public bool DeletesPoints(List<string> seriesNames, int count)
        {
            bool isSuccess = true;
            try
            {
                SyncContext.Post(delegate
                {
                    foreach (var name in seriesNames) chart.Series[name].Points.RemoveRange(0, count);
                }, null);
            }
            catch (Exception e)
            {
                isSuccess = false;
                Log.WriteLog(LogType.Exception, "DeleteEveryPoints exception : " + e.ToString());
            }
            return isSuccess;
        }

        public bool DeleteSeries(string seriesName)
        {
            bool isSuccess = true;
            if (this.Handle != null)
            {
                try
                {
                    SyncContext.Post(delegate
                    {
                        chart.Series.Remove(chart.GetSeriesByName(seriesName));
                    }, null);
                }
                catch (Exception e)
                {
                    isSuccess = false;
                    Log.WriteLog(LogType.Exception, "DeleteSeries exception : " + e.ToString());
                }
            }
            return isSuccess;
        }

        public void ShowSeries(string seriesName, bool isShow) => SyncContext.Post(delegate
        {
            Series series = chart.GetSeriesByName(seriesName);
            if (series != null)
            {
                series.Visible = isShow;
                var isVisible = isShow ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

                dynamic diagram = null;
                if (LineType == ViewType.Line) diagram = chart.Diagram != null ? ((XYDiagram)chart.Diagram).SecondaryAxesY.GetAxisByName(seriesName) : null;
                else if (LineType == ViewType.SwiftPlot) diagram = chart.Diagram != null ? ((SwiftPlotDiagram)chart.Diagram).SecondaryAxesY.GetAxisByName(seriesName) : null;

                if (diagram != null) diagram.Visibility = isVisible;
            }
        }, null);

        public void ShowAllSeries(bool isShow)
        {
            SyncContext.Post(delegate
            {
                foreach (Series series in chart.Series) series.Visible = isShow;
            }, null);
        }

        /// <returns>-1 when series is null</returns>
        public int GetDataCount(string seriesName)
        {
            try
            {
                Series series = chart.GetSeriesByName(seriesName);
                if (series != null) return series.Points.Count;
                else return -1;
            }
            catch (Exception e)
            {
                Log.WriteLog(LogType.Exception, "DataCount exception : " + e.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 지우개
        /// </summary>
        public void ClearChart()
        {
            if (this.Handle != null)
            {
                SyncContext.Post(delegate
                {
                    dynamic diagram = null;
                    if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
                    else diagram = (XYDiagram)chart.Diagram;
                    if (diagram != null) { diagram.AxisY.ConstantLines.Clear(); diagram.AxisX.ConstantLines.Clear(); diagram.SecondaryAxesY.Clear(); chart.Series.Clear(); }
                }, null);
            }
        }

        public void SetConstantLine(double value, string name = "", AxisOption axisOption = AxisOption.Y)
        {
            if (chart.Diagram == null)
            {
                Series series = new Series("dummy", LineType);
                chart.Series.Add(series);
            }
            dynamic diagram = null;
            if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
            else diagram = (XYDiagram)chart.Diagram;

            name = string.IsNullOrEmpty(Name) ? value.ToString() : name;

            var constantLine = new ConstantLine(name);
            constantLine.LegendText = name;
            constantLine.AxisValue = value;
            constantLine.Visible = true;
            constantLine.ShowInLegend = true;
            constantLine.ShowBehind = false;

            constantLine.Color = axisOption == AxisOption.Y ? Color.Crimson : Color.DarkBlue;
            constantLine.LineStyle.DashStyle = DashStyle.Dash;
            constantLine.LineStyle.Thickness = 2;

            SyncContext.Post(delegate
            {
                switch (axisOption)
                {
                    case AxisOption.Y:
                        bool isYMatched = false;
                        foreach (ConstantLine line in diagram.AxisY.ConstantLines)
                        {
                            if ((double)line.AxisValue == (double)constantLine.AxisValue)
                            {
                                line.Name = constantLine.Name;
                                isYMatched = true;
                                break;
                            }
                        }
                        if (isYMatched == false) diagram.AxisY.ConstantLines.Add(constantLine);
                        break;
                    case AxisOption.X:
                        bool isXMatched = false;
                        foreach (ConstantLine line in diagram.AxisX.ConstantLines)
                        {
                            if ((double)line.AxisValue == (double)constantLine.AxisValue)
                            {
                                line.Name = constantLine.Name;
                                isXMatched = true;
                                break;
                            }
                        }
                        if (isXMatched == false) diagram.AxisX.ConstantLines.Add(constantLine);
                        break;
                }
            }, null);
        }

        public void DeleteConstantLine(AxisOption axisOption)
        {
            dynamic diagram = null;
            if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
            else diagram = (XYDiagram)chart.Diagram;
            if (diagram != null)
            {
                switch (axisOption)
                {
                    case AxisOption.X:
                        diagram.AxisX.ConstantLines.Clear();
                        break;
                    case AxisOption.Y:
                        diagram.AxisY.ConstantLines.Clear();
                        break;
                }
            }
        }

        public bool SetAxisName(AxisOption axisOption, string name)
        {
            dynamic diagram = null;
            if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
            else diagram = (XYDiagram)chart.Diagram;
            if (diagram != null)
            {
                switch (axisOption)
                {
                    case AxisOption.X:
                        diagram.AxisX.Title.Text = name;
                        diagram.AxisX.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                        diagram.AxisX.Title.Alignment = StringAlignment.Center;
                        diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                        diagram.AxisX.Title.TextColor = Color.Black;
                        break;
                    case AxisOption.Y:
                        diagram.AxisY.Title.Text = name;
                        diagram.AxisY.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
                        diagram.AxisY.Title.Alignment = StringAlignment.Center;
                        diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                        diagram.AxisY.Title.TextColor = Color.Black;
                        break;
                }
            }
            else return false;
            return true;
        }

        public void SetAxisMax(AxisOption axisOption, double maxValue)
        {
            dynamic diagram = null;
            if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
            else diagram = (XYDiagram)chart.Diagram;
            if (diagram != null)
            {
                switch (axisOption)
                {
                    case AxisOption.X:
                        diagram.AxisX.WholeRange.MaxValue = maxValue;
                        break;
                    case AxisOption.Y:
                        diagram.AxisY.WholeRange.MaxValue = maxValue;
                        break;
                }
            }
        }

        public Color GetLineColor(string seriesName)
        {
            try
            {
                Series series = chart.GetSeriesByName(seriesName);
                if (series == null) return Color.Transparent;
                return chart.PaletteRepository[chart.PaletteName][chart.Series.IndexOf(series)].Color;
            }
            catch (Exception e)
            {
                Log.WriteLog(LogType.Exception, "GetLineColor exception : " + e.ToString());
                return Color.Transparent;
            }
        }

        public void SetLineColor(string seriesName, Color color)
        {
            try
            {
                SetSeries(seriesName);
                Series series = chart.GetSeriesByName(seriesName);

                dynamic seriesView = null;
                if (LineType == ViewType.Line) seriesView = series.View as LineSeriesView;
                else seriesView = series.View as SwiftPlotSeriesView;

                seriesView.Color = color;
            }
            catch (Exception e)
            {
                Log.WriteLog(LogType.Exception, "SetLineColor exception : " + e.ToString());
            }
        }

        public void SetRandomColorPalette()
        {
            var paletteData = chart.GetPaletteNames();
            Thread.Sleep(10);
            Random rand = new Random();
            int stop = rand.Next(0, 50);
            for (int i = 0; i < paletteData.Length; i++)
            {
                if (i >= stop)
                {
                    chart.PaletteName = paletteData[i];
                    break;
                }
            }
        }

        public void ShowScaleButtons(GraphPurpose purpose = GraphPurpose.RunningData)
        {
            this.Purpose = purpose;
            ResetScale();
            button_YPlus.Visible = true;
            button_YMinus.Visible = true;
        }

        /*-------------------------------------------
         * 
         *      Private functions
         * 
         -------------------------------------------*/

        void CheckChartSetting(bool useOADate = false)
        {
            if (LineThickness <= 0) LineThickness = 1;
            if (chart.GetSeriesByName("dummy") != null) DeleteSeries("dummy");

            dynamic diagram;
            if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
            else diagram = (XYDiagram)chart.Diagram;

            if (diagram != null)
            {
                if (useOADate == false)
                {
                    diagram.AxisX.Label.TextPattern = "{V}";
                    diagram.AxisX.Label.Angle = 0;
                }
                else
                {
                    diagram.AxisX.Label.TextPattern = IsShowDate ? "{A: yy-MM-dd HH:mm:ss}" : "{A: HH:mm:ss}";
                    diagram.AxisX.Label.Angle = -30;
                    diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
                    diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Millisecond;
                }

                diagram.ZoomingOptions.UseKeyboard = true;
                diagram.ZoomingOptions.UseKeyboardWithMouse = true;
                diagram.ZoomingOptions.UseMouseWheel = true;
                diagram.ZoomingOptions.UseTouchDevice = true;
                diagram.ZoomingOptions.ZoomOutShortcuts.Add((DevExpress.Portable.Input.PortableKeys)Keys.Escape);
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYZooming = true;
                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisYScrolling = true;

                chart.AutoLayout = false;
                chart.Legend.Visibility = IsShowLegend ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            }
        }

        void SetSeries(string seriesName, bool useOADate = false, bool isSecondaryAxis = false)
        {
            if (chart.GetSeriesByName(seriesName) == null)
            {
                SyncContext.Send(delegate
                {
                    var dataSeries = new Series(seriesName, LineType);

                    if (LineType == ViewType.Line || LineType == ViewType.SwiftPlot)
                    {
                        dynamic seriesView = null;
                        if (LineType == ViewType.Line) seriesView = dataSeries.View as LineSeriesView;
                        else seriesView = dataSeries.View as SwiftPlotSeriesView;

                        seriesView.LineStyle.Thickness = LineThickness;
                        dataSeries.View = seriesView;

                        if (isSecondaryAxis)
                        {
                            dynamic yAxis = null;
                            if (LineType == ViewType.Line) yAxis = new SecondaryAxisY(seriesName);
                            else yAxis = new SwiftPlotDiagramSecondaryAxisY(seriesName);

                            yAxis.Title.Text = seriesName;
                            yAxis.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

                            if (LineType == ViewType.Line) { ((XYDiagram)chart.Diagram).SecondaryAxesY.Add(yAxis); ((LineSeriesView)dataSeries.View).AxisY = yAxis; }
                            else { ((SwiftPlotDiagram)chart.Diagram).SecondaryAxesY.Add(yAxis); ((SwiftPlotSeriesView)dataSeries.View).AxisY = yAxis; }
                        }
                    }

                    chart.Series.Add(dataSeries);
                    CheckChartSetting(useOADate);
                }, null);
            }
        }

        void ResetScale()
        {
            dynamic diagram = null;
            if (LineType == ViewType.SwiftPlot) diagram = (SwiftPlotDiagram)chart.Diagram;
            else diagram = (XYDiagram)chart.Diagram;
            if (diagram != null)
            {
                switch (Purpose)
                {
                    case GraphPurpose.RunningData:
                        AxisScale = 0.00000001;
                        break;
                    case GraphPurpose.Parameter:
                        AxisScale = (double)diagram.AxisY.VisualRange.MaxValue;
                        break;
                }
            }
        }

        /*-------------------------------------------
         * 
         *      Helper functions
         * 
         -------------------------------------------*/

        /// <summary>
        /// Y축에 사용할 수 있는지 자료형 T 검사
        /// </summary>
        /// <typeparam name="T">int, float, double</typeparam>
        /// <param name="dummy">안씀</param>
        /// <returns>쓸 수 있다면 true, 아니면 false</returns>
        bool IsValidYAxis<T>(T dummy)
        {
            if (typeof(T) != typeof(double) && typeof(T) != typeof(float) && typeof(T) != typeof(int)) return false;
            else return true;
        }
    }
}