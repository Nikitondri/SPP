namespace lab03directory_scanner.view.node;

public class Package : Node
{
    public Package(string name, long size, double percent) : base(name, size, percent)
    {
    }

    public override string GetPathToIcon()
    {
        return "/resources/package.png";
    }
}