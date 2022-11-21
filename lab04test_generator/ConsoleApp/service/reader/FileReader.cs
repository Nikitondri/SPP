using System.Text;
using ConsoleApp.util;

namespace ConsoleApp.service.reader;

public class FileReader : IReader<string, Task<string>>
{
    
    public Task<string> Read(string source)
    {
        CorrectPathChecker.ThrowExceptionIfIncorrectFilePath(source);
        return ReadFileAsync(source);
    }

    private async Task<string> ReadFileAsync(string path)
    {
        using var reader = new StreamReader(CreateFileStream(path), Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    private FileStream CreateFileStream(string path)
    {
        return new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.None,
            4096,
            FileOptions.Asynchronous
        );
    }
}