using System.Threading.Tasks.Dataflow;

namespace Core.pipeline.item;

public interface IPropagatorPipelineItem<S, D>
{
    IPropagatorBlock<S, D> InitAndGetItem();
}