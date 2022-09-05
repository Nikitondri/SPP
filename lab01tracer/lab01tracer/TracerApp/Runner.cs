using System.Reflection;
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

        // const string jsonFileName = "/home/nikita/university/semester05/SPP/lab01tracer/lab01tracer/TracerApp/results/result.json";
        const string xmlFileName = "/home/nikita/university/semester05/SPP/lab01tracer/lab01tracer/TracerApp/results/result.xml";
        const string yamlFileName = "/home/nikita/university/semester05/SPP/lab01tracer/lab01tracer/TracerApp/results/result.yaml";

        var a = Assembly.LoadFrom("/home/nikita/university/semester05/SPP/lab01tracer/lab01tracer/YamlTraceResultSerializerPlugin/bin/Debug/net6.0/ref/YamlTraceResultSerializerPlugin.dll");
        // var a = Assembly.LoadFrom("/home/nikita/university/semester05/SPP/lab01tracer/lab01tracer/XmlSerializerPlugin/bin/Debug/net6.0/ref/XmlSerializerPlugin.dll");
        var myType = a.GetType("YamlTraceResultSerializer");
        var myMethod = myType?.GetMethod("Serialize");
        var obj = Activator.CreateInstance(myType!);
        myMethod?.Invoke(obj, new object?[]{result, new FileStream(xmlFileName, FileMode.Create)});

        // LoadToFile(new JsonTraceResultSerializer(), result, jsonFileName);
        // LoadToFile(new XmlTraceResultSerializer(), result, xmlFileName);
        // LoadToFile(new YamlTraceResultSerializer(), result, yamlFileName);
        // LoadToConsole(new YamlTraceResultSerializer(), result);
        // LoadToConsole(new JsonTraceResultSerializer(), result);
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

