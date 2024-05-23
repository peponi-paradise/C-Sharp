using CommunityToolkit.Mvvm.DependencyInjection;
using System.Diagnostics;
using Workloads.JobOperator;

namespace TestApp
{
    public partial class Form1 : Form
    {
        private IJobOperator _operator;

        public Form1()
        {
            InitializeComponent();
            _operator = Ioc.Default.GetRequiredService<IJobOperator>();
            _operator.JobExecutionChanged += _operator_JobExecutionChanged;
            _operator.StepExecutionChanged += _operator_StepExecutionChanged;
        }

        private void _operator_StepExecutionChanged(object? sender, Workloads.ExecutionData e)
        {
            Trace.WriteLine($"From Step - {sender}: {e.Name}, {e.ExecutionId}, {e.ExecutionStatus}");
        }

        private void _operator_JobExecutionChanged(object? sender, Workloads.ExecutionData e)
        {
            Trace.WriteLine($"From Job - {sender}: {e.Name}, {e.ExecutionId}, {e.ExecutionStatus}");
        }

        private void _startButton_Click(object sender, EventArgs e)
        {
            _operator.Start();
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
        }
    }
}