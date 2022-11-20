namespace ConsoleApp.service.writer;

public interface IAsyncWriter<D, V>
{
    Task Write(D dest, V value);
}