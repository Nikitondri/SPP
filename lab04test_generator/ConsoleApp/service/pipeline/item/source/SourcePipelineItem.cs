using System.Threading.Tasks.Dataflow;
using ConsoleApp.service.reader;

namespace ConsoleApp.service.pipeline.item.source;

public class SourcePipelineItem : ISourcePipelineItem<string>
{
    private IReader<string, string[]> _directoryReader;
    private IReader<string, Task<string>> _fileReader;
    private readonly int _maxReadThreads;
    private TransformManyBlock<string, string> _startBlock;

    public SourcePipelineItem(IReader<string, string[]> directoryReader, IReader<string, Task<string>> fileReader,
        int maxReadThreads)
    {
        _directoryReader = directoryReader;
        _fileReader = fileReader;
        _maxReadThreads = maxReadThreads;
    }

    public ISourceBlock<string> InitAndGetItem()
    {
        var directoryReader = new TransformManyBlock<string, string>(_directoryReader.Read);
        var fileReader = CreateFileReader();

        _startBlock = directoryReader;

        var opt = new DataflowLinkOptions { PropagateCompletion = true };
        directoryReader.LinkTo(fileReader, opt);
        return fileReader;
    }

    private TransformBlock<string, string> CreateFileReader()
    {
        var opt = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _maxReadThreads };
        return new TransformBlock<string, string>(_fileReader.Read, opt);
    }

    public void Start(string source)
    {
        _startBlock.Post(source);
        _startBlock.Complete();
    }
}