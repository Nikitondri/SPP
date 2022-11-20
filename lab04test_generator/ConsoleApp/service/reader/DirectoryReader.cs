using ConsoleApp.util;

namespace ConsoleApp.service.reader;

public class DirectoryReader : IReader<string, string[]>
{
    public string[] read(string source)
    {
        CorrectPathChecker.ThrowExceptionIfIncorrectPath(source);
        return ReadDirectory(source);
    }

    private string[] ReadDirectory(string path)
    {
        return Directory
            .EnumerateFiles(path, "*", SearchOption.AllDirectories)
            .ToArray();
    }
}