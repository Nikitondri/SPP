using System.Diagnostics;

namespace TracerLib.Tracer;

public class Tracer : ITracer
{
    private readonly TraceResult _traceResult;

    public Tracer()
    {
        _traceResult = new TraceResult();
    }

    public void StartTrace()
    {
        var currentMethod = new StackTrace().GetFrame(1)?.GetMethod();
        _traceResult.StartTrace(Environment.CurrentManagedThreadId, currentMethod);
    }

    public void StopTrace()
    {
        _traceResult.StopTrace(Environment.CurrentManagedThreadId);
    }

    public TraceResult GetTraceResult()
    {
        return _traceResult;
    }
}