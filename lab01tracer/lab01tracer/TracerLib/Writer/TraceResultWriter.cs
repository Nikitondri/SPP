using System.Reflection;
using TracerLib.Tracer;

namespace TracerLib.Writer;

public class TraceResultWriter : ITraceResultWriter
{
    public void WriteToFile(string pluginPath, string typeStr, TraceResult result, string fileName)
    {
        var assembly = Assembly.LoadFrom(pluginPath);
        var type = assembly.GetType(typeStr);
        var method = type?.GetMethod("Serialize");
        var obj = Activator.CreateInstance(type!);
        method?.Invoke(obj, new object?[] { result, new FileStream(fileName, FileMode.Create) });
    }
}