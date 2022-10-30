using System.Collections.Concurrent;
using core.model;

namespace core.service;

public class ThreadPoolScanner
{
    private const int MaxThreads = 5;
    private Semaphore _semaphore;
    private int _count;
    private bool _scannerStarted;
    private Thread _queueHandler;
    private readonly ConcurrentQueue<TaskInfo> _taskQueue;
    private readonly CancellationTokenSource _tokenSource;
    private object _lock = new object();


    public ThreadPoolScanner()
    {
        _semaphore = new Semaphore(MaxThreads, MaxThreads);
        _count = MaxThreads;
        _scannerStarted = false;
        _taskQueue = new ConcurrentQueue<TaskInfo>();
        _tokenSource = new CancellationTokenSource();
    }

    public bool IsFinish()
    {
        return (_taskQueue.IsEmpty && _count == MaxThreads && !_scannerStarted) || _tokenSource.Token.IsCancellationRequested;
    }

    public void Start()
    {
        _count = MaxThreads;
        _scannerStarted = true;
        _queueHandler = new Thread(QueueHandler);
        _queueHandler.Start(_tokenSource.Token);
    }

    private void QueueHandler(object? obj)
    {
        var token = (CancellationToken)obj;

        while (!token.IsCancellationRequested)
        {
            if (_taskQueue.IsEmpty)
                continue;

            _semaphore.WaitOne();
            lock (_lock)
            { _count--; }
				
            _taskQueue.TryDequeue(out var taskInfo);

            ThreadPool.QueueUserWorkItem(TaskWrapper, taskInfo, true);
        }
    }
    
    private void TaskWrapper(TaskInfo data)
    {
        try
        {
            data.Task(data.TaskData);
        }
        catch { }
        finally
        {
            lock (_lock)
            { _count++; }
            _semaphore.Release();
        }
    }
    
    public void AddTask(Action<FileScanData> task, FileScanData scanData)
    {
        _scannerStarted = false;
        var info = new TaskInfo
        {
            Task = task,
            TaskData = scanData
        };

        _taskQueue.Enqueue(info);
    }
    
    public void Stop()
    {
        _tokenSource.Cancel();
    }
}