using System.Threading.Tasks.Dataflow;

namespace Core.service.pipeline.item;

public interface IPropagatorPipelineItem<S, D>
{
    IPropagatorBlock<S, D> InitAndGetItem();
}