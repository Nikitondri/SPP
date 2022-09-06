using System.Runtime.Serialization;

namespace AbstractionSerializerPlugin.Dto;

[Serializable]
[DataContract]
public class MethodTraceResultDto
{
    private string _methodName;
    private string _className;
    private long _timeMs;
    private List<MethodTraceResultDto> _methodsList;

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
    public long TimeMs
    {
        get => _timeMs;
        private set => _timeMs = value;
    }

    [DataMember(Name = "methods", Order = 3)]
    public List<MethodTraceResultDto> MethodList
    {
        get => _methodsList;
        private set => _methodsList = value;
    }

    public MethodTraceResultDto(string methodName, string className, long timeMs, List<MethodTraceResultDto> methodsList)
    {
        _methodName = methodName;
        _className = className;
        _timeMs = timeMs;
        _methodsList = methodsList;
    }
}