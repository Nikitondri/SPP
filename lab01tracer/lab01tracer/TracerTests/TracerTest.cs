using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using TracerLib.Tracer;

namespace TracerTests;

[TestFixture]
public class TracerTest
{
    private ITracer _tracer = null!;

    [SetUp]
    public void Setup()
    {
        _tracer = new Tracer();
    }

    private void NestingLevel2Method(int sleepTime)
    {
        _tracer.StartTrace();
        Thread.Sleep(sleepTime);
        _tracer.StopTrace();
    }

    private void NestingLevel1Method(int sleepTime)
    {
        _tracer.StartTrace();
        Thread.Sleep(sleepTime);
        NestingLevel2Method(sleepTime);
        _tracer.StopTrace();
    }
    
    private void NestingLevel0Method(int sleepTime)
    {
        _tracer.StartTrace();
        Thread.Sleep(sleepTime);
        NestingLevel1Method(sleepTime);
        _tracer.StopTrace();
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(200)]
    public void TimeOfSingleThreadTest(int sleepTime)
    {
        NestingLevel2Method(sleepTime);
        var traceResult = _tracer.GetTraceResult();
        Assert.True(traceResult.Threads[0].TimeMs >= sleepTime);
    }
    
    [TestCase(0, 0)]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(200, 20)]
    public void TimeOfMultiThreadTest(int sleepTime, int threadsCount)
    {
        var threads = new List<Thread>();
        for (var i = 0; i < threadsCount; i++)
        {
            var thread = new Thread(() => NestingLevel0Method(sleepTime));
            threads.Add(thread);
            thread.Start();
        }
        foreach (var thread in threads)
        {
            thread.Join();
        }

        var traceResult = _tracer.GetTraceResult();
        var actualTime = traceResult.Threads.Sum(t => t.TimeMs);

        Assert.True(actualTime >= sleepTime * threadsCount);
        Assert.AreEqual(threadsCount, traceResult.Threads.Count);
    }
    
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(200)]
    public void TimeOfNestedMethods(int sleepTime)
    {
        NestingLevel0Method(sleepTime);
        var traceResult = _tracer.GetTraceResult();
        Assert.True(traceResult.Threads[0].TimeMs >= sleepTime * 3);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(200)]
    public void MultipleMethodSingleThreadTest(int sleepTime)
    {
        NestingLevel2Method(sleepTime);
        NestingLevel2Method(sleepTime * 2);

        var traceResult = _tracer.GetTraceResult();
        Assert.AreEqual(1, traceResult.Threads.Count);
        Assert.AreEqual(2, traceResult.Threads[0].Methods.Count);
        Assert.True(traceResult.Threads[0].TimeMs >= sleepTime * 3);
    }
}