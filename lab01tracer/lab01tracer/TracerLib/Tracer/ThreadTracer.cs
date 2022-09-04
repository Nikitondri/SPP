namespace TracerLib.Tracer;

public class ThreadTracer
{
    public int Id { get; private set; }
    public List<MethodTracer> lFirstLvlMethodTracers { get; private set; }
    private Stack<MethodTracer> sUnstoppedMethodTracers;
    public TimeSpan Time { get; private set; }

    public ThreadTracer(int id)
    {
        Id = id;
        lFirstLvlMethodTracers = new List<MethodTracer>();
        sUnstoppedMethodTracers = new Stack<MethodTracer>();
        Time = new TimeSpan();
    }

    public void StartTrace()
    {
        MethodTracer methodTracer = new MethodTracer();

        // check thread inner methods
        if (sUnstoppedMethodTracers.Count > 0)
        {
            MethodTracer lastUnstoppedMethodTracer = sUnstoppedMethodTracers.Peek();
            lastUnstoppedMethodTracer.lInnerMethodTracers.Add(methodTracer);
        }

        sUnstoppedMethodTracers.Push(methodTracer);
        methodTracer.StartTrace();
    }

    public void StopTrace()
    {
        MethodTracer lastUnstoppedMethodTracer = sUnstoppedMethodTracers.Pop();
        lastUnstoppedMethodTracer.StopTrace();
        if (!sUnstoppedMethodTracers.Any())
        {
            lFirstLvlMethodTracers.Add(lastUnstoppedMethodTracer);
            Time += lastUnstoppedMethodTracer.Time;
        }
    }
}