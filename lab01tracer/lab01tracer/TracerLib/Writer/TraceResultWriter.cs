using System.Reflection;
using AbstractionSerializerPlugin.Dto;
using TracerLib.Tracer;

namespace TracerLib.Writer;

public class TraceResultWriter : ITraceResultWriter
{
    public void WriteToFile(string folderPath, TraceResult result)
    {
        var files = Directory.GetFiles(folderPath);
        foreach (var filePath in files)
        {
            var assembly = Assembly.LoadFrom(filePath);
            var type = assembly.GetExportedTypes()[0];
            var method = type?.GetMethod("Serialize");
            var obj = Activator.CreateInstance(type!);
            method?.Invoke(obj, new object?[]
                {
                    CreateTraceResultDto(result),
                    new FileStream("../../../results/" + type, FileMode.Create)
                }
            );
        }
    }

    private TraceResultDto CreateTraceResultDto(TraceResult traceResult)
    {
        return new TraceResultDto(AddThreadResultDto(traceResult.Threads));
    }

    private List<ThreadTraceResultDto> AddThreadResultDto(IReadOnlyList<ThreadTraceResult> traceResults)
    {
        return traceResults.Select(traceResult => new ThreadTraceResultDto(
                traceResult.Id,
                AddMethodToResultDto(traceResult.Methods),
                traceResult.TimeMs))
            .ToList();
    }

    private List<MethodTraceResultDto> AddMethodToResultDto(IReadOnlyList<MethodTraceResult> methodResultList)
    {
        var methodResultDtoList = new List<MethodTraceResultDto>();
        foreach (var methodResult in methodResultList)
        {
            var nestedMethodResultDtoList = new List<MethodTraceResultDto>();
            if (methodResult.MethodList.Count != 0)
            {
                nestedMethodResultDtoList = AddMethodToResultDto(methodResult.MethodList);
            }

            methodResultDtoList.Add(new MethodTraceResultDto(
                    methodResult.MethodName,
                    methodResult.ClassName,
                    methodResult.TimeMs,
                    nestedMethodResultDtoList
                )
            );
        }

        return methodResultDtoList;
    }
}