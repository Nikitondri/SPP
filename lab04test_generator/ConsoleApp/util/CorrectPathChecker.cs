using ConsoleApp.exception;

namespace ConsoleApp.util;

public static class CorrectPathChecker
{
    public static void ThrowExceptionIfIncorrectPath(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new IncorrectPathException();
        }
    }
}