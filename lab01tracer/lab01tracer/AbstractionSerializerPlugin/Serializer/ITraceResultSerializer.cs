using AbstractionSerializerPlugin.Dto;

namespace AbstractionSerializerPlugin.Serializer;

public interface ITraceResultSerializer
{
    void Serialize(TraceResultDto traceResult, Stream to);
}