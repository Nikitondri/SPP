using System.Runtime.Serialization;

namespace AbstractionSerializerPlugin.Dto;

[Serializable]
[DataContract]
public class ThreadTraceResultDto
{
    private int _id;
    private List<MethodTraceResultDto> _methods;
    private long _timeMs;

    public ThreadTraceResultDto(int id, List<MethodTraceResultDto> methods, long timeMs)
    {
        _id = id;
        _methods = methods;
        _timeMs = timeMs;
    }

    [DataMember(Name = "id", Order = 0)]
    public int Id
    {
        get => _id;
        private set => _id = value;
    }

    [DataMember(Name = "timeMs", Order = 1)]
    public long TimeMs
    {
        get => _timeMs;
        private set => _timeMs = value;
    }

    [DataMember(Name = "methods", Order = 2)]
    public List<MethodTraceResultDto> Methods
    {
        get => _methods;
        private set => _methods = value;
    }
}