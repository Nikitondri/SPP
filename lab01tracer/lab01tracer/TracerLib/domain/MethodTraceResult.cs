using System.Runtime.Serialization;
using System.Xml.Serialization;
using TracerLib.Tracer;

namespace TracerLib.domain;

[DataContract(Name = "method")]
[XmlType(TypeName = "method")]
public class MethodTraceResult
{
    [DataMember(Name = "name", Order = 0)]
    [XmlElement(ElementName = "name")]
    public string ClassName { get; private set; }
    
    [DataMember(Name = "class", Order = 1)]
    [XmlElement(ElementName = "class")]
    public string MethodName { get; private set; }
    
    [DataMember(Name = "time", Order = 2)]
    [XmlElement(ElementName = "time")]
    public TimeSpan Time { get; private set; }
    
    [DataMember(Name = "methods", Order = 3)]
    [XmlElement(ElementName = "methods")]
    public List<MethodTraceResult> lInnerMethodTracerResults;

    
    public static MethodTraceResult GetTraceResult(MethodTracer methodTracer)
    {
        MethodTraceResult methodTracerResult = new MethodTraceResult();
        methodTracerResult.ClassName = methodTracer.ClassName;
        methodTracerResult.MethodName = methodTracer.MethodName;
        methodTracerResult.Time = methodTracer.Time;
        methodTracerResult.lInnerMethodTracerResults = new List<MethodTraceResult>();

        foreach (var innerMethodTracer in methodTracer.lInnerMethodTracers)
        {
            methodTracerResult.lInnerMethodTracerResults.Add(MethodTraceResult.GetTraceResult(innerMethodTracer));
        }

        return methodTracerResult;
    }
}