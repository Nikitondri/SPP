using TracerLib.Serializer;
using TracerLib.Tracer;
using TracerLib.Writer;

namespace TracerApp;

internal static class Runner
{
    private static readonly Tracer Tracer = new();

    private static void Main()
    {
        var example = new Example(Tracer);
        example.StartTest();
        var result = Tracer.GetTraceResult();

        const string jsonFileName = "/home/nikita/university/semester05/SPP/lab01tracer/lab01tracer/TracerApp/results/result.json";
        const string xmlFileName = "/home/nikita/university/semester05/SPP/lab01tracer/lab01tracer/TracerApp/results/result.xml";

        LoadToFile(new JsonTraceResultSerializer(), result, jsonFileName);
        LoadToFile(new XmlTraceResultSerializer(), result, xmlFileName);
        LoadToConsole(new JsonTraceResultSerializer(), result);
    }

    static void LoadToFile(ITraceResultSerializer serializer, TraceResult result, string fileName)
    {
        IWriter writer = new FileWriter(fileName);
        writer.Write(result, serializer);
    }

    static void LoadToConsole(ITraceResultSerializer serializer, TraceResult result)
    {
        IWriter writer = new ConsoleWriter();
        writer.Write(result, serializer);
    }
}

