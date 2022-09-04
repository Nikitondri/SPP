using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TracerLib.Tracer;

[Serializable]
[DataContract]
public sealed class ThreadTraceResult
{
    private int _id;
    private List<MethodTraceResult> _methods;
    private Stack<MethodTraceResult> _callMethods;

    [DataMember(Name = "id", Order = 0)]
    public int Id
    {
        get => _id;
        private set => _id = value;
    }
    [DataMember(Name = "time", Order = 1)]
    public string Time
    {
        get => TimeInt.ToString() + "ms";
        private set { }
    }
    [XmlIgnore]
    public long TimeInt { get; private set; }

    [DataMember(Name = "methods", Order = 2)]
    public List<MethodTraceResult> Methods
    {
        get { return _methods; }
        private set => _methods = value;
    }

    public ThreadTraceResult()
    {
        TimeInt = 0;
        _methods = new List<MethodTraceResult>();
        _callMethods = new Stack<MethodTraceResult>();
    }

    public ThreadTraceResult(int threadId)
    {
        _id = threadId;
        TimeInt = 0;
        _methods = new List<MethodTraceResult>();
        _callMethods = new Stack<MethodTraceResult>();
    }

    public void StartTrace(MethodTraceResult method)
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

    public void StopTrace()
    {
        MethodTraceResult lastMethod = _callMethods.Peek();
        lastMethod.StopTrace();
        if (_callMethods.Count == 1)
        {
            TimeInt += lastMethod.TimeInt;
        }

        _callMethods.Pop();
    }
}