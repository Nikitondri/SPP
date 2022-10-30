namespace core.model;

public class FileScanData
{
    public FileScanData(int? parentId, string fileName)
    {
        ParentId = parentId;
        FileName = fileName;
    }

    public int? ParentId { get; set; }
    public string FileName { get; set; }
}