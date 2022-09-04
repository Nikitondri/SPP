using System.Collections.Concurrent;
using TracerLib.domain;

namespace TracerLib.Tracer;

public class Tracer : ITracer
{
    TraceResult TracerResult { get; set; }
    private readonly ConcurrentDictionary<int, ThreadTracer> _cdThreadTracers;
    private static readonly object Locker = new object();

    public Tracer()
    {
        _cdThreadTracers = new ConcurrentDictionary<int, ThreadTracer>();
    }

    public void StartTrace()
    {
        ThreadTracer curThreadTracer = AddOrGetThreadTracer(Thread.CurrentThread.ManagedThreadId);
        curThreadTracer.StartTrace();
    }

    public void StopTrace()
    {
        ThreadTracer currThreadTracer = AddOrGetThreadTracer(Thread.CurrentThread.ManagedThreadId);
        currThreadTracer.StopTrace();
    }

    public TraceResult GetTraceResult()
    {
        lock (Locker)
        {
            TracerResult = new TraceResult(_cdThreadTracers);
        }

        return TracerResult;
    }

    private ThreadTracer AddOrGetThreadTracer(int id)
    {
        // synchronization
        lock(Locker)
        {
            // check if exists
            if (!_cdThreadTracers.TryGetValue(id, out var threadTracer))
            {
                threadTracer = new ThreadTracer(id);
                _cdThreadTracers[id] = threadTracer;
            }
            return threadTracer;
        }
    }
}