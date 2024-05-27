using CommunityToolkit.Mvvm.DependencyInjection;
using Workloads;
using Workloads.JobOperator;

namespace TestApp
{
    internal enum TextType
    {
        Job,
        Step
    }

    public partial class Form1 : Form
    {
        private readonly SynchronizationContext _syncContext;
        private IJobOperator _operator;

        public Form1()
        {
            InitializeComponent();
            _syncContext = SynchronizationContext.Current!;
            _operator = Ioc.Default.GetRequiredService<IJobOperator>();
            _operator.JobExecutionChanged += _operator_JobExecutionChanged;
            _operator.StepExecutionChanged += _operator_StepExecutionChanged;
        }

        private void _operator_StepExecutionChanged(object? sender, ExecutionData e)
        {
            AddText(TextType.Step, e);
        }

        private void _operator_JobExecutionChanged(object? sender, ExecutionData e)
        {
            AddText(TextType.Job, e);
        }

        private void _startButton_Click(object sender, EventArgs e)
        {
            _operator.Start();
        }

        private void AddText(TextType type, ExecutionData data)
        {
            _syncContext.Post(delegate
            {
                textBox1.Text += $"{DateTime.Now.ToString("HH.mm.ss.fff")} - {type} - {data.Name}, {data.ExecutionId}, {data.ExecutionStatus}{Environment.NewLine}";
            }, null);
        }
    }
}