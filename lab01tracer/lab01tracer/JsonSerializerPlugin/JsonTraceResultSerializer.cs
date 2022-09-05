using System.Runtime.Serialization.Json;
using System.Text;
using TracerLib.Serializer;
using TracerLib.Tracer;

namespace JsonSerializerPlugin;

public class JsonTraceResultSerializer : ITraceResultSerializer
{
    private readonly DataContractJsonSerializer _jsonFormatter;

    public JsonTraceResultSerializer()
    {
        _jsonFormatter = new DataContractJsonSerializer(typeof(TraceResult));
    }

    public void Serialize(TraceResult traceResult, Stream to)
    {
        using var jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(to, Encoding.UTF8, ownsStream: true,
            indent: true, indentChars: "     ");
        _jsonFormatter.WriteObject(jsonWriter, traceResult);
    }
}