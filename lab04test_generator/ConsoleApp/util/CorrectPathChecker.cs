using ConsoleApp.exception;

namespace ConsoleApp.util;

public static class CorrectPathChecker
{
    public static void ThrowExceptionIfIncorrectDirPath(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new IncorrectPathException();
        }
    }
    
    public static void ThrowExceptionIfIncorrectFilePath(string path)
    {
        if (!File.Exists(path))
        {
            throw new IncorrectPathException();
        }
    }
}