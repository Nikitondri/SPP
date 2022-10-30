namespace core.model;

public class TaskInfo
{
    public Action<FileScanData> Task { get; set; }
    public FileScanData TaskData { get; set; }
}