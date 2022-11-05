namespace core.model;

public class FileScanData
{
    
    public int? ParentId { get; set; }
    public string FileName { get; set; }
    
    public FileScanData(int? parentId, string fileName)
    {
        ParentId = parentId;
        FileName = fileName;
    }
}