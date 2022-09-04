using TracerLib.domain;

namespace TracerLib.Serializer;

public interface ITraceResultSerializer
{
    void Serialize(Stream outStream, TraceResult traceResult);
}