using System.Threading.Tasks.Dataflow;

namespace ConsoleApp.model;

public class Pipeline
{
    public ISourceBlock<string> SourceBlock { get; set; }

    public List<IPropagatorBlock<string, string>> PropagatorBlocks { get; set; }

    public ITargetBlock<string> TargetBlock { get; set; }
}