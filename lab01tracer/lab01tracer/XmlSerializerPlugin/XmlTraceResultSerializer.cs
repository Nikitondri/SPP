using System.Runtime.Serialization;
using System.Xml;
using AbstractionSerializerPlugin.Dto;
using AbstractionSerializerPlugin.Serializer;

namespace XmlSerializerPlugin;

public class XmlTraceResultSerializer : ITraceResultSerializer
{
    private readonly DataContractSerializer _xmlConverter;
    private readonly XmlWriterSettings _xmlWriterSettings;

    public XmlTraceResultSerializer()
    {
        _xmlConverter = new DataContractSerializer(typeof(TraceResultDto));
        _xmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "     "
        };
    }

    public void Serialize(TraceResultDto traceResult, Stream to)
    {
        using var xmlWriter = XmlWriter.Create(to, _xmlWriterSettings);
        _xmlConverter.WriteObject(xmlWriter, traceResult);
    }
}