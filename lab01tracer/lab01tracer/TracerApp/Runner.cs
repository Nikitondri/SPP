using System.Reflection;
using TracerLib.Tracer;

namespace TracerApp;

internal static class Runner
{
    private const string YamlSerializerType = "YamlTraceResultSerializerPlugin.YamlTraceResultSerializer";
    private const string JsonSerializerType = "JsonSerializerPlugin.JsonTraceResultSerializer";
    private const string XmlSerializerType = "XmlSerializerPlugin.XmlTraceResultSerializer";

    private const string YamlSerializerPluginPath =
        "../../../../YamlTraceResultSerializerPlugin/bin/Debug/net6.0/YamlTraceResultSerializerPlugin.dll";

    private const string JsonSerializerPluginPath =
        "../../../../JsonSerializerPlugin/bin/Debug/net6.0/JsonSerializerPlugin.dll";

    private const string XmlSerializerPluginPath =
        "../../../../XmlSerializerPlugin/bin/Debug/net6.0/XmlSerializerPlugin.dll";

    private const string JsonFileName =
        "../../../results/result.json";

    private const string XmlFileName =
        "../../../results/result.xml";

    private const string YamlFileName =
        "../../../results/result.yaml";


    private static readonly Tracer Tracer = new();

    private static void Main()
    {
        var example = new Example(Tracer);
        example.StartTest();
        var result = Tracer.GetTraceResult();

        LoadToFile(YamlSerializerPluginPath, YamlSerializerType, result, YamlFileName);
        LoadToFile(JsonSerializerPluginPath, JsonSerializerType, result, JsonFileName);
        LoadToFile(XmlSerializerPluginPath, XmlSerializerType, result, XmlFileName);
    }

    private static void LoadToFile(string pluginPath, string typeStr, TraceResult result, string fileName)
    {
        var a = Assembly.LoadFrom(pluginPath);
        var myType = a.GetType(typeStr);
        var myMethod = myType?.GetMethod("Serialize");
        var obj = Activator.CreateInstance(myType!);
        myMethod?.Invoke(obj, new object?[] { result, new FileStream(fileName, FileMode.Create) });
    }
}