using TracerLib.Tracer;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TracerLib.Serializer;

public class YamlTraceResultSerializer : ITraceResultSerializer
{
    private readonly ISerializer _serializer;

    public YamlTraceResultSerializer()
    {
        _serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
    }

    public void Serialize(TraceResult traceResult, Stream to)
    {
        var yamlString = _serializer.Serialize(traceResult);
        using var streamWriter = new StreamWriter(to);
        streamWriter.WriteLine(yamlString);
    }
}