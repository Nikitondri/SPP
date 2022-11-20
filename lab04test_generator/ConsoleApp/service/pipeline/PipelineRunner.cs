using System.Threading.Tasks.Dataflow;
using ConsoleApp.service.pipeline.item.source;
using ConsoleApp.service.pipeline.item.target;
using Core.pipeline.item;

namespace ConsoleApp.service.pipeline;

public class PipelineRunner : IPipelineRunner
{
    private ISourcePipelineItem<string> _sourceItem;
    private IPropagatorPipelineItem<string, string> _propagatorItem;
    private ITargetPipelineItem<string> _targetItem;

    public PipelineRunner(ISourcePipelineItem<string> sourceItem,
        IPropagatorPipelineItem<string, string> propagatorItem, ITargetPipelineItem<string> targetItem)
    {
        _sourceItem = sourceItem;
        _propagatorItem = propagatorItem;
        _targetItem = targetItem;
    }

    public async Task Run(string source)
    {
        var sourceItem = _sourceItem.InitAndGetItem();
        var propagatorItem = _propagatorItem.InitAndGetItem();
        var targetItem = _targetItem.InitAndGetItem();
        
        var opt = new DataflowLinkOptions { PropagateCompletion = true };
        
        sourceItem.LinkTo(propagatorItem, opt);
        propagatorItem.LinkTo(targetItem, opt);
        
        _sourceItem.Start(source);
        await _targetItem.IsFinished();
    }
}