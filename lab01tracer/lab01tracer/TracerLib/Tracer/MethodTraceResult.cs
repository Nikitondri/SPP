using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

namespace TracerLib.Tracer;

[Serializable]
[DataContract]
public sealed class MethodTraceResult
{
    private string _methodName;
    private string _className;
    private List<MethodTraceResult> _methodsList;
    private Stopwatch _stopWatch;

    [DataMember(Name = "name", Order = 0)]
    public string MethodName
    {
        get => _methodName;
        private set => _methodName = value;
    }

    [DataMember(Name = "class", Order = 1)]
    public string ClassName
    {
        get => _className;
        private set => _methodName = value;
    }

    [DataMember(Name = "timeMs", Order = 2)]
    public long TimeMs { get; private set; }

    [DataMember(Name = "methods", Order = 3)]
    public List<MethodTraceResult> MethodList
    {
        get => _methodsList;
        private set => _methodsList = value;
    }

    public MethodTraceResult(string methodName, string className)
    {
        _methodName = methodName;
        _className = className;
        _methodsList = new List<MethodTraceResult>();
        _stopWatch = new Stopwatch();
        TimeMs = 0;
    }

    public MethodTraceResult(MethodBase? method)
    {
        _methodsList = new List<MethodTraceResult>();
        _stopWatch = new Stopwatch();
        _methodName = method!.Name;
        _className = method.DeclaringType!.Name;
        TimeMs = 0;
    }

    public void StartTrace()
    {
        _stopWatch.Start();
    }

    public void StopTrace()
    {
        _stopWatch.Stop();
        TimeMs = _stopWatch.ElapsedMilliseconds;
        _stopWatch.Reset();
    }

    public void AddNestedMethod(MethodTraceResult method)
    {
        _methodsList.Add(method);
    }
}