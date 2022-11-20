namespace ConsoleApp.service.pipeline;

public interface IPipelineRunner
{
    Task Run(string source);
}