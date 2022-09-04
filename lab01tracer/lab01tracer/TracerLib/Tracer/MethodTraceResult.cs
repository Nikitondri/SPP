using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TracerLib.Tracer;

[Serializable]
[DataContract]
public sealed class MethodTraceResult
{
    private string _methodName;
    private string _className;
    private long _time;

    private List<MethodTraceResult> _methodsList;
    private Stopwatch _stopWatch;

    [DataMember(Name = "name", Order = 0)]
    public string MethodName
    {
        get { return _methodName; }
        private set => _methodName = value;
    }
    [DataMember(Name = "class", Order = 1)]
    public string ClassName
    {
        get { return _className; }
        private set => _methodName = value;
    }

    [DataMember(Name = "time", Order = 2)]
    public string Time
    {
        get { return _time.ToString() + "ms"; }
        private set => _methodName = value;
    }

    [XmlIgnore]
    public long TimeInt
    {
        get { return _time; }
    }

    [DataMember(Name = "methods", Order = 3)]
    public List<MethodTraceResult> Methodlist
    {
        get { return _methodsList; }
        private set { _methodsList = value; }
    }

    public MethodTraceResult()
    {
        _methodsList = new List<MethodTraceResult>();
        _stopWatch = new Stopwatch();
        _time = 0;
    }

    public MethodTraceResult(MethodBase method)
    {
        _methodsList = new List<MethodTraceResult>();
        _stopWatch = new Stopwatch();
        _methodName = method.Name;
        _className = method.DeclaringType.Name;
        _time = 0;
    }

    public void StartTrace()
    {
        _stopWatch.Start();
    }

    public void StopTrace()
    {
        _stopWatch.Stop();
        _time = _stopWatch.ElapsedMilliseconds;
        _stopWatch.Reset();
    }

    public void AddNestedMethod(MethodTraceResult method)
    {
        _methodsList.Add(method);
    }
}