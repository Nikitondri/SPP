namespace lab03directory_scanner.view.node;

public class PackageView : NodeView
{
    public PackageView(string name, long size, double percent) : base(name, size, percent)
    {
    }
    
    public PackageView()
    {
    }

    public override string GetPathToIcon()
    {
        return "C:\\Users\\nikita.zakharenko\\Desktop\\SPP\\SPP\\lab03directory_scanner\\ui\\resources\\package.png";
    }
}