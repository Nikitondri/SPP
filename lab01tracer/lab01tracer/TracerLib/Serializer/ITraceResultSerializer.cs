using TracerLib.Tracer;

namespace TracerLib.Serializer;

public interface ITraceResultSerializer
{
    void Serialize(TraceResult traceResult, Stream to);
}