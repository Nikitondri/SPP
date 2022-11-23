using Microsoft.Extensions.Configuration;

namespace ConsoleApp.config.builder;

public static class ConfigBuilder
{
    public static ProgramConfig Build()
    {
        var config = CreateConfig();
        return new ProgramConfig(
            config["input"]!,
            config["output"]!,
            int.Parse(config["maxReadThread"]!),
            int.Parse(config["maxWriteThread"]!),
            int.Parse(config["maxGenerateThread"]!)
        );
    }

    private static IConfigurationRoot CreateConfig()
    {
        return new ConfigurationBuilder()
            .AddJsonFile(
                $"C:\\Users\\nikita.zakharenko\\Desktop\\SPP\\SPP\\lab04test_generator\\ConsoleApp\\appsettings.json",
                true, true
            )
            .Build();
    }
}