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
        var threadId = Thread.CurrentThread.ManagedThreadId;
        NestingLevel2Method(sleepTime);
        var traceResult = _tracer.GetTraceResult();
        Assert.True(traceResult.Threads[0].TimeMs >= sleepTime);
        Assert.AreEqual(nameof(NestingLevel2Method), traceResult.Threads[0].Methods[0].MethodName);
        Assert.AreEqual(nameof(TracerTest), traceResult.Threads[0].Methods[0].ClassName);
        Assert.AreEqual(threadId, traceResult.Threads[0].Id);
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
        var threadsId = new List<int>();
        for (var i = 0; i < threadsCount; i++)
        {
            var thread = new Thread(() => NestingLevel0Method(sleepTime));
            threadsId.Add(thread.ManagedThreadId);
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
        for (var i = 0; i < threadsId.Count; i++)
        {
            Assert.AreEqual(threadsId[i], traceResult.Threads[i].Id);
            Assert.AreEqual(nameof(NestingLevel0Method), traceResult.Threads[i].Methods[0].MethodName);
            Assert.AreEqual(nameof(TracerTest), traceResult.Threads[i].Methods[0].ClassName);
            Assert.AreEqual(nameof(NestingLevel1Method), traceResult.Threads[i].Methods[0].MethodList[0].MethodName);
            Assert.AreEqual(nameof(NestingLevel2Method),
                traceResult.Threads[i].Methods[0].MethodList[0].MethodList[0].MethodName);
        }
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(200)]
    public void TimeOfNestedMethods(int sleepTime)
    {
        var threadId = Thread.CurrentThread.ManagedThreadId;
        NestingLevel0Method(sleepTime);
        var traceResult = _tracer.GetTraceResult();
        Assert.True(traceResult.Threads[0].TimeMs >= sleepTime * 3);
        Assert.AreEqual(threadId, traceResult.Threads[0].Id);
        Assert.AreEqual(nameof(NestingLevel0Method), traceResult.Threads[0].Methods[0].MethodName);
        Assert.AreEqual(nameof(NestingLevel1Method), traceResult.Threads[0].Methods[0].MethodList[0].MethodName);
        Assert.AreEqual(nameof(NestingLevel2Method),
            traceResult.Threads[0].Methods[0].MethodList[0].MethodList[0].MethodName);
        Assert.AreEqual(nameof(TracerTest), traceResult.Threads[0].Methods[0].ClassName);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100)]
    [TestCase(200)]
    public void MultipleMethodSingleThreadTest(int sleepTime)
    {
        var threadId = Thread.CurrentThread.ManagedThreadId;
        
        NestingLevel2Method(sleepTime);
        NestingLevel1Method(sleepTime * 2);

        var traceResult = _tracer.GetTraceResult();
        Assert.AreEqual(1, traceResult.Threads.Count);
        Assert.AreEqual(2, traceResult.Threads[0].Methods.Count);
        Assert.True(traceResult.Threads[0].TimeMs >= sleepTime * 3);
        Assert.AreEqual(nameof(NestingLevel2Method), traceResult.Threads[0].Methods[0].MethodName);
        Assert.AreEqual(nameof(NestingLevel1Method), traceResult.Threads[0].Methods[1].MethodName);
        Assert.AreEqual(nameof(NestingLevel2Method), traceResult.Threads[0].Methods[1].MethodList[0].MethodName);
        Assert.AreEqual(nameof(TracerTest), traceResult.Threads[0].Methods[0].ClassName);
        Assert.AreEqual(threadId, traceResult.Threads[0].Id);
    }
}