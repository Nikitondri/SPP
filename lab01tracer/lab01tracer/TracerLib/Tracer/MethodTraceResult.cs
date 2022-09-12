using System.Diagnostics;
using System.Reflection;

namespace TracerLib.Tracer;


public sealed class MethodTraceResult
{
    private string _methodName;
    private string _className;
    private List<MethodTraceResult> _methodsList;
    private Stopwatch _stopWatch;

    public string MethodName
    {
        get => _methodName;
        private set => _methodName = value;
    }

    public string ClassName
    {
        get => _className;
        private set => _methodName = value;
    }

    public long TimeMs { get; private set; }

    public IReadOnlyList<MethodTraceResult> MethodList
    {
        get => _methodsList.AsReadOnly();
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
        
        //TODO: 1) аттрибуты сериализации убрать +
        //2) плагины загружать динамически +
        //3) list -> IReadOnlyXXX +
        //4) тесты
        //5) отдельная иерархия / internal + 
    }

    internal void StartTrace()
    {
        _stopWatch.Start();
    }

    internal void StopTrace()
    {
        _stopWatch.Stop();
        TimeMs = _stopWatch.ElapsedMilliseconds;
        _stopWatch.Reset();
    }

    internal void AddNestedMethod(MethodTraceResult method)
    {
        _methodsList.Add(method);
    }
}