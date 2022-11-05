using System;
using System.Collections.ObjectModel;

namespace lab03directory_scanner.view.node;

public abstract class NodeView
{
    protected NodeView(string name, long size, double percent)
    {
        Name = name;
        Size = size;
        Percent = percent;
    }
    
    protected NodeView()
    {
    }

    public string Name { get; set; }

    public long Size { get; set; }
    
    public double Percent { get; set; }
    public string PathToIcon
    {
        get => GetPathToIcon();
    }

    public ObservableCollection<NodeView> ViewNodes { get; set; }

    public abstract string GetPathToIcon();
}