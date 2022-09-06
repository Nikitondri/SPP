using TracerLib.Tracer;
using TracerLib.Writer;

namespace TracerApp;

internal static class Runner
{
    // private const string YamlSerializerType = "YamlTraceResultSerializerPlugin.YamlTraceResultSerializer";
    // private const string JsonSerializerType = "JsonSerializerPlugin.JsonTraceResultSerializer";
    // private const string XmlSerializerType = "XmlSerializerPlugin.XmlTraceResultSerializer";
    //
    // private const string YamlSerializerPluginPath =
    //     "../../../../YamlTraceResultSerializerPlugin/bin/Debug/net6.0/YamlTraceResultSerializerPlugin.dll";
    //
    // private const string JsonSerializerPluginPath =
    //     "../../../../JsonSerializerPlugin/bin/Debug/net6.0/JsonSerializerPlugin.dll";
    //
    // private const string XmlSerializerPluginPath =
    //     "../../../../XmlSerializerPlugin/bin/Debug/net6.0/XmlSerializerPlugin.dll";
    //
    // private const string JsonFileName =
    //     "../../../results/result.json";
    //
    // private const string XmlFileName =
    //     "../../../results/result.xml";
    //
    // private const string YamlFileName =
    //     "../../../results/result.yaml";


    private static readonly ITracer Tracer = new Tracer();
    private static readonly ITraceResultWriter Writer = new TraceResultWriter();
    

    private static void Main()
    {
        var example = new Example(Tracer);
        example.StartTest();
        var result = Tracer.GetTraceResult();

        Writer.WriteToFile("C:\\Users\\nikita.zakharenko\\Desktop\\tracer\\SPP\\lab01tracer\\lab01tracer\\plugins", result);
    }
}