using lab01tracer.main.com.zakharenko.lab01tracer.entity;

namespace lab01tracer.main.com.zakharenko.lab01tracer.serializer;

public interface ITraceResultSerializer
{
    void Serialize(TraceResult traceResult, Stream to);
}