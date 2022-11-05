namespace lab03directory_scanner.view.node;

public class FileView : NodeView
{
    public FileView(string name, long size, double percent) : base(name, size, percent)
    {
    }
    
    public FileView()
    {
    }

    public override string GetPathToIcon()
    {
        return "C:\\Users\\nikita.zakharenko\\Desktop\\SPP\\SPP\\lab03directory_scanner\\ui\\resources\\file.png";
    }
}