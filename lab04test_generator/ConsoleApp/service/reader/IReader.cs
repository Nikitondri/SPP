namespace ConsoleApp.service.reader;

public interface IReader<S, D>
{
    D read(S source);
}