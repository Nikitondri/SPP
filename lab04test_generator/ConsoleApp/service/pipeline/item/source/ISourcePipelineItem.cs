using System.Threading.Tasks.Dataflow;

namespace ConsoleApp.service.pipeline.item.source;

public interface ISourcePipelineItem<T>
{
    void Start(T source);
    
    ISourceBlock<T> InitAndGetItem();
}