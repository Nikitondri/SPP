using System.Runtime.Serialization;

namespace AbstractionSerializerPlugin.Dto;

[Serializable]
[DataContract]
public class TraceResultDto
{
    [DataMember(Name = "threads")]
    public List<ThreadTraceResultDto> Threads { get; }

    public TraceResultDto(List<ThreadTraceResultDto> threads)
    {
        Threads = threads;
    }
}