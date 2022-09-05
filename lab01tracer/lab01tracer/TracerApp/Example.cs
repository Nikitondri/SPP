using TracerLib.Tracer;

namespace TracerApp;

public class Example
{
    private readonly ITracer _tracer;

    public Example(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void StartTest()
    {
        var threads = new List<Thread>();

        for (var i = 0; i < 5; i++)
        {
            Thread thread;
            switch (i)
            {
                case 0:
                case 1:
                    thread = new Thread(FirstMethod);
                    break;
                case 2:
                case 3:
                    thread = new Thread(SecondMethod);
                    break;
                default:
                    thread = new Thread(SixthMethod);
                    break;
            }

            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    private void FirstMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(100);
        ThirdMethod();
        _tracer.StopTrace();
    }

    private void SecondMethod()
    {
        _tracer.StartTrace();
        var threads = new List<Thread>();

        for (var i = 0; i < 3; i++)
        {
            var thread = new Thread(FifthMethod);
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Thread.Sleep(200);
        FourthMethod();
        FifthMethod();
        _tracer.StopTrace();
    }

    private void ThirdMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(300);
        _tracer.StopTrace();
    }

    private void FourthMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(400);
        _tracer.StopTrace();
    }

    private void FifthMethod()
    {
        _tracer.StartTrace();
        var threads = new List<Thread>();

        for (var i = 0; i < 2; i++)
        {
            var thread = new Thread(SixthMethod);
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Thread.Sleep(500);
        SixthMethod();
        _tracer.StopTrace();
    }

    private void SixthMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(600);
        _tracer.StopTrace();
    }
}