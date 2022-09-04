using System.Runtime.Serialization;
using System.Xml;
using TracerLib.Tracer;

namespace TracerLib.Serializer;

public class XmlTraceResultSerializer : ITraceResultSerializer
{
    private readonly DataContractSerializer _xmlConverter;
    private readonly XmlWriterSettings _xmlWriterSettings;

    public XmlTraceResultSerializer()
    {
        _xmlConverter = new DataContractSerializer(typeof(TraceResult));
        _xmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "     "
        };
    }

    public void Serialize(TraceResult traceResult, Stream to)
    {
        using var xmlWriter = XmlWriter.Create(to, _xmlWriterSettings);
        _xmlConverter.WriteObject(xmlWriter, traceResult);
    }
}