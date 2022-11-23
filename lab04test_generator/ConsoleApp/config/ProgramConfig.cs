namespace ConsoleApp.config;

public class ProgramConfig
{
    public string Input { get; set; }
    public string Output { get; set; }
    public int MaxReadThread { get; set; }
    public int MaxWriteThread { get; set; }
    public int MaxGenerateThread { get; set; }

    public ProgramConfig(string input, string output, int maxReadThread, int maxWriteThread, int maxGenerateThread)
    {
        Input = input;
        Output = output;
        MaxReadThread = maxReadThread;
        MaxWriteThread = maxWriteThread;
        MaxGenerateThread = maxGenerateThread;
    }
}