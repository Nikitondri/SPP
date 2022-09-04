using TracerLib.Serializer;
using TracerLib.Tracer;

namespace TracerLib.Writer;

public class FileWriter : IWriter
{
    private readonly string _fileName;

    public FileWriter(string fileName)
    {
        this._fileName = fileName;
    }

    public void Write(TraceResult result, ITraceResultSerializer serializer)
    {
        using (FileStream fs = new FileStream(_fileName, FileMode.Create))
        {
            serializer.Serialize(result, fs);
        }
    }
}