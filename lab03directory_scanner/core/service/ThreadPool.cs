using System.Collections.Concurrent;

namespace core.service;

public class ThreadPool
{
    private const int MaxThreads = 5;
    private Semaphore _semaphore;
    private int _count;
    private bool _scannerStarted;
    private Thread _queueHandler;
    private readonly ConcurrentQueue<TaskInfo> _taskQueue;
    private readonly CancellationTokenSource _tokenSource;
    private object _lock = new object();
}