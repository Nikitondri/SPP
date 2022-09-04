using TracerLib.Serializer;
using TracerLib.Tracer;

namespace TracerLib.Writer;

public class ConsoleWriter : IWriter
{
    public void Write(TraceResult traceResult, ITraceResultSerializer serializer)
    {
        using var consoleStream = Console.OpenStandardOutput();
        serializer.Serialize(traceResult, consoleStream);
    }
}