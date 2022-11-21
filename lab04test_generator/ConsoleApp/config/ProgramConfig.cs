namespace ConsoleApp.config;

public class ProgramConfig
{
    public string Input { get; set; }
    public string Output { get; set; }

    public ProgramConfig(string input, string output)
    {
        Input = input;
        Output = output;
    }
}