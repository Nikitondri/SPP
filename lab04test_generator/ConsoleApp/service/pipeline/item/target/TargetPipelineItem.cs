using System.Threading.Tasks.Dataflow;
using ConsoleApp.service.writer;

namespace ConsoleApp.service.pipeline.item.target;

public class TargetPipelineItem : ITargetPipelineItem<string>
{
    private readonly int _maxWriteThreads;
    private readonly string _outputDirectory;
    private IAsyncWriter<string, string> _fileWriter;

    private ActionBlock<string> _lastBlock;

    public TargetPipelineItem(int maxWriteThreads, string outputDirectory, IAsyncWriter<string, string> fileWriter)
    {
        _maxWriteThreads = maxWriteThreads;
        _outputDirectory = outputDirectory;
        _fileWriter = fileWriter;
    }

    public ITargetBlock<string> GetItem()
    {
        var writer = CreateWriteFileBlock();
        _lastBlock = writer;
        return writer;
    }

    private ActionBlock<string> CreateWriteFileBlock()
    {
        CreateDirectoryIfNotExist(_outputDirectory);
        var opt = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _maxWriteThreads };
        return new ActionBlock<string>(text => _fileWriter.Write(_outputDirectory, text), opt);
    }

    private void CreateDirectoryIfNotExist(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    public Task IsFinished()
    {
        return _lastBlock.Completion;
    }
}