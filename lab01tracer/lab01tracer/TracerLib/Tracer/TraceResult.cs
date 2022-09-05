using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;

namespace TracerLib.Tracer;

[Serializable]
[DataContract]
public class TraceResult
{
    private ConcurrentDictionary<int, ThreadTraceResult> _threadsList;

    [DataMember(Name = "threads")]
    public List<ThreadTraceResult> Threads
    {
        get
        {
            var sortedDictionary = new SortedDictionary<int, ThreadTraceResult>(_threadsList);
            return new List<ThreadTraceResult>(sortedDictionary.Values);
        }
    }

    public TraceResult()
    {
        _threadsList = new ConcurrentDictionary<int, ThreadTraceResult>();
    }

    public void StartTrace(int id, MethodBase? method)
    {
        var threadTraceResult = _threadsList.GetOrAdd(id, new ThreadTraceResult(id));
        threadTraceResult.StartTrace(new MethodTraceResult(method));
    }

    public void StopTrace(int id)
    {
        if (!_threadsList.TryGetValue(id, out var threadTraceResult))
        {
            throw new ArgumentException("Invalid thread ID");
        }

        threadTraceResult.StopTrace();
    }
}