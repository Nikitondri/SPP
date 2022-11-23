using ConsoleApp.config;
using ConsoleApp.config.builder;
using ConsoleApp.service.pipeline;
using ConsoleApp.service.pipeline.item.source;
using ConsoleApp.service.pipeline.item.target;
using ConsoleApp.service.reader;
using ConsoleApp.service.writer;
using Core.service.pipeline.item;

namespace ConsoleApp;

public static class Program
{
    private static readonly ProgramConfig ProgramConfig;

    static Program()
    {
        ProgramConfig = ConfigBuilder.Build();
    }

    public static void Main()
    {
        var pipelineRunner = PipelineRunner();
        pipelineRunner.Run(ProgramConfig.Input);
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
            ProgramConfig.MaxReadThread);
    }

    private static ITargetPipelineItem<string> TargetPipelineItem()
    {
        return new TargetPipelineItem(
            ProgramConfig.MaxWriteThread,
            ProgramConfig.Output,
            new FileAsyncWriter()
        );
    }

    private static IPropagatorPipelineItem<string, string> PropagatorPipelineItem()
    {
        return new PropagatorPipelineItem(ProgramConfig.MaxGenerateThread);
    }
}