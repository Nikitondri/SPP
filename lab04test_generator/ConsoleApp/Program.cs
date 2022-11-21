using ConsoleApp.service.pipeline;
using ConsoleApp.service.pipeline.item.source;
using ConsoleApp.service.pipeline.item.target;
using ConsoleApp.service.reader;
using ConsoleApp.service.writer;
using Core.service.pipeline.item;

namespace ConsoleApp;

public static class Program
{
    private const string Input =
        @"C:\Users\nikita.zakharenko\Desktop\SPP\SPP\lab04test_generator\ConsoleApp\resources\input";

    private const string Output =
        @"C:\Users\nikita.zakharenko\Desktop\SPP\SPP\lab04test_generator\ConsoleApp\resources\output";

    public static void Main()
    {
        var pipelineRunner = PipelineRunner();
        pipelineRunner.Run(Input);
        Console.ReadLine();
    }

    private static IPipelineRunner PipelineRunner()
    {
        return new PipelineRunner(
            SourcePipelineItem(),
            PropagatorPipelineItem(),
            TargetPipelineItem()
        );
    }

    private static ISourcePipelineItem<string> SourcePipelineItem()
    {
        return new SourcePipelineItem(
            new DirectoryReader(),
            new FileReader(),
            10);
    }

    private static ITargetPipelineItem<string> TargetPipelineItem()
    {
        return new TargetPipelineItem(
            10,
            Output,
            new FileAsyncWriter()
        );
    }

    private static IPropagatorPipelineItem<string, string> PropagatorPipelineItem()
    {
        return new PropagatorPipelineItem(10);
    }
}