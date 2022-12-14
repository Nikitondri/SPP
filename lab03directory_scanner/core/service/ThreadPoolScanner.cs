using System.Collections.Concurrent;
using core.model;

namespace core.service;

public class ThreadPoolScanner
{
    private const int MaxThreads = 5;
    private readonly SemaphoreSlim _semaphore;
    private int _count;
    private bool _scannerStarted;
    private readonly Thread _queueHandler;
    private readonly ConcurrentQueue<TaskInfo> _taskQueue;
    private readonly CancellationTokenSource _tokenSource;
    private readonly object _lock = new();


    public ThreadPoolScanner()
    {
        _semaphore = new SemaphoreSlim(MaxThreads);
        _count = MaxThreads;
        _scannerStarted = false;
        _taskQueue = new ConcurrentQueue<TaskInfo>();
        _tokenSource = new CancellationTokenSource();
        _queueHandler = new Thread(QueueHandler);
    }

    public bool IsFinish()
    {
        return (_count == MaxThreads && _taskQueue.IsEmpty && !_scannerStarted) ||
               _tokenSource.Token.IsCancellationRequested;
    }

    public void Start()
    {
        _count = MaxThreads;
        _scannerStarted = true;
        _queueHandler.Start(_tokenSource.Token);
    }

    private void QueueHandler(object? obj)
    {
        var token = (CancellationToken)obj!;

        while (!token.IsCancellationRequested)
        {
            if (_taskQueue.IsEmpty)
                continue;

            _semaphore.WaitAsync(token);
            lock (_lock)
            {
                _count--;
            }

            _taskQueue.TryDequeue(out var taskInfo);

            ThreadPool.QueueUserWorkItem(TaskWrapper!, taskInfo, true);
        }
    }

    private void TaskWrapper(TaskInfo data)
    {
        try
        {
            data.Task(data.TaskData);
        }
        catch
        {
            // ignored
        }
        finally
        {
            lock (_lock)
            {
                _count++;
            }

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