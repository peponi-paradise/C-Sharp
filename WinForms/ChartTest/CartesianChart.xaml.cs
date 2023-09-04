using AppLL.Common.Define.Data;
using DevExpress.Xpf.Charts;
using System.Windows;
using System.Windows.Controls;

namespace AppLL.Common.Components;

/// <summary>
/// CartesianChart.xaml에 대한 상호 작용 논리
/// </summary>
public partial class CartesianChart : UserControl
{
    public static readonly DependencyProperty ChartDataProperty = DependencyProperty.Register(nameof(ChartData), typeof(object), typeof(CartesianChart), new PropertyMetadata(null));

    public object ChartData
    {
        get => GetValue(ChartDataProperty);
        set => SetValue(ChartDataProperty, value);
    }

    public CartesianChart()
    {
        InitializeComponent();
        Chart.Diagram.Series.CollectionChanged += Series_CollectionChanged;
    }

    private void Series_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e != null && e.NewItems != null)
        {
            switch (e.NewItems[0])
            {
                case AreaStepSeries2D:
                    ((XYDiagram2D)Chart.Diagram).AxisX.Logarithmic = true;
                    break;

                case LineSeries2D:
                    ((XYDiagram2D)Chart.Diagram).AxisX.Logarithmic = false;
                    break;
            }
        }
    }
}

internal class TemplateSelector : DataTemplateSelector
{
    // Common
    public DataTemplate? BarSeriesTemplate { get; set; }

    public DataTemplate? SplineSeriesTemplate { get; set; }

    // HSensor
    public DataTemplate? PHDSeriesTemplate { get; set; }

    public DataTemplate? PSDSeriesTemplate { get; set; }
    public DataTemplate? PDSeriesTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        switch (item)
        {
            case ChartSeriesData seriesData:
                switch (seriesData.SeriesType)
                {
                    case Define.Enums.SeriesType.Bar:
                        return BarSeriesTemplate!;

                    case Define.Enums.SeriesType.Line:
                        return SplineSeriesTemplate!;
                }
                break;

            case HSensorSeriesData hSensorSeriesData:
                switch (hSensorSeriesData.SeriesType)
                {
                    case Define.Enums.HSensorSeriesType.PHD:
                        return PHDSeriesTemplate!;

                    case Define.Enums.HSensorSeriesType.PSD:
                        return PSDSeriesTemplate!;

                    case Define.Enums.HSensorSeriesType.PD:
                        return PDSeriesTemplate!;
                }
                break;
        }
        return base.SelectTemplate(item, container);
    }
}