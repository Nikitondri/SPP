using System.Runtime.Serialization;
using System.Xml.Serialization;
using TracerLib.Tracer;

namespace TracerLib.domain;

[DataContract(Name = "thread")]
[XmlType(TypeName = "thread")]
public class ThreadTraceResult
{
    
    [DataMember(Name = "methods", Order = 2)]
    [XmlElement(ElementName = "methods")]
    public IList<MethodTraceResult> lFirstLvlMethodTracersResult { get; private set; }
    
    [DataMember(Name = "id", Order = 0)]
    [XmlElement(ElementName = "id")]
    public int Id { get; private set; }
    
    [DataMember(Name = "time", Order = 1)]
    [XmlElement(ElementName = "time")]
    public TimeSpan Time { get; private set; }

    public static ThreadTraceResult GetTraceResult(ThreadTracer threadTracer)
    {
        ThreadTraceResult threadTracerResult = new ThreadTraceResult();
        threadTracerResult.lFirstLvlMethodTracersResult = new List<MethodTraceResult>();
        threadTracerResult.Id = threadTracer.Id;
        threadTracerResult.Time = threadTracer.Time;

        foreach (var firstLvlMethodTracer in threadTracer.lFirstLvlMethodTracers)
        {
            threadTracerResult.lFirstLvlMethodTracersResult.Add(MethodTraceResult.GetTraceResult(firstLvlMethodTracer));
        }

        return threadTracerResult;
    }
}