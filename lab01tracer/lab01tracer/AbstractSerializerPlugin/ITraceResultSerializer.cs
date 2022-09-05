namespace AbstractSerializerPlugin;

public interface ITraceResultSerializer
{
    void Serialize(TraceResult traceResult, Stream to);
}