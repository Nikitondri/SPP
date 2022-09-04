using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TracerLib.Tracer;

namespace TracerLib.domain;

[DataContract(Name = "result")]
public class TraceResult
{
    [DataMember(Name = "threads")]
    [XmlElement(ElementName = "threads")]
    public IDictionary<int, ThreadTraceResult> dThreadTracerResults { get; private set; }

    public TraceResult(ConcurrentDictionary<int, ThreadTracer> cdThreadTracers)
    {
        dThreadTracerResults = new Dictionary<int, ThreadTraceResult>();
        foreach (var threadTracer in cdThreadTracers)
        {
            dThreadTracerResults[threadTracer.Key] = ThreadTraceResult.GetTraceResult(threadTracer.Value);
        }
    }
}