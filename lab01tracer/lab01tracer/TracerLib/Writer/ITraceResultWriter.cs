using TracerLib.Tracer;

namespace TracerLib.Writer;

public interface ITraceResultWriter
{
    void WriteToFile(string pluginPath, string typeStr, TraceResult result, string fileName);
}