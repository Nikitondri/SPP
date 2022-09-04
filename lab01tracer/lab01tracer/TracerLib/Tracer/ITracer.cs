using TracerLib.domain;

namespace TracerLib.Tracer;

public interface ITracer
{
    void StartTrace();

    void StopTrace();
    
    TraceResult GetTraceResult();
}