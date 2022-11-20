using System.Threading.Tasks.Dataflow;

namespace ConsoleApp.service.pipeline.item.target;

public interface ITargetPipelineItem<T>
{
    ITargetBlock<T> InitAndGetItem();
    Task IsFinished();
}