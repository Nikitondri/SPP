namespace lab03directory_scanner.view.node;

public class File : Node
{
    public File(string name, long size, double percent) : base(name, size, percent)
    {
    }

    public override string GetPathToIcon()
    {
        return "/resources/file.png";
    }
}