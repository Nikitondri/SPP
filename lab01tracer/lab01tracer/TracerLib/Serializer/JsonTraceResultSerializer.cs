using System.Runtime.Serialization.Json;
using TracerLib.domain;

namespace TracerLib.Serializer;

public class JsonTraceResultSerializer : ITraceResultSerializer
{
    protected readonly DataContractJsonSerializer jsonSerializer;

    public void Serialize(Stream outStream, TraceResult traceResult)
    {
        using (Stream stream = outStream)
        {
            jsonSerializer.WriteObject(stream, traceResult);
        }
    }

    public JsonTraceResultSerializer()
    {
        jsonSerializer = new DataContractJsonSerializer(typeof(TraceResult));
    }
}