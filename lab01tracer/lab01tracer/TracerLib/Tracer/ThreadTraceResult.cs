namespace TracerLib.Tracer;


public sealed class ThreadTraceResult
{
    private int _id;
    private List<MethodTraceResult> _methods;
    private Stack<MethodTraceResult> _callMethods;

    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    public long TimeMs { get; private set; }

    public IReadOnlyList<MethodTraceResult> Methods
    {
        get => _methods;
    }

    public ThreadTraceResult()
    {
        TimeMs = 0;
        _methods = new List<MethodTraceResult>();
        _callMethods = new Stack<MethodTraceResult>();
    }

    internal ThreadTraceResult(int threadId)
    {
        _id = threadId;
        TimeMs = 0;
        _methods = new List<MethodTraceResult>();
        _callMethods = new Stack<MethodTraceResult>();
    }

    internal void StartTrace(MethodTraceResult method)
    {
        if (_callMethods.Count == 0)
        {
            _methods.Add(method);
        }
        else
        {
            _callMethods.Peek().AddNestedMethod(method);
        }

        _callMethods.Push(method);
        method.StartTrace();
    }

    internal void StopTrace()
    {
        var lastMethod = _callMethods.Pop();
        lastMethod.StopTrace();
        if (_callMethods.Count == 0)
        {
            TimeMs += lastMethod.TimeMs;
        }

    }
}