using TracerLib.Serializer;
using TracerLib.Tracer;

namespace TracerLib.Writer;

public interface IWriter
{
    void Write(TraceResult traceResult, ITraceResultSerializer serializer);
}