using System.Diagnostics;
using System.Reflection;

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
        MethodBase currentMethod = new StackTrace().GetFrame(1).GetMethod();
        _traceResult.StartTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
    }

    public void StopTrace()
    {
        _traceResult.StopTrace(Thread.CurrentThread.ManagedThreadId);
    }

    public TraceResult GetTraceResult()
    {
        return _traceResult;
    }
}