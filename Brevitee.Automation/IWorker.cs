using System;
namespace Brevitee.Automation
{
    public interface IWorker
    {
        Job Job { get; set; }
        string Name { get; set; }
        string StepNumber { get; set; }
        bool Busy { get; set; }
        WorkState<T> State<T>(WorkState<T> state);
        WorkState Do(Job job);
    }
}
