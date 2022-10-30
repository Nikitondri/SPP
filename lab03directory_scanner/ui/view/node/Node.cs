using System;
using System.Collections.ObjectModel;

namespace lab03directory_scanner.view.node;

public abstract class Node
{
    protected Node(string name, long size, double percent)
    {
        Name = name;
        Size = size;
        Percent = percent;
    }

    public string Name { get; set; }

    public long Size { get; set; }
    
    public double Percent { get; set; }

    public ObservableCollection<Node> ViewNodes { get; set; }

    public abstract string GetPathToIcon();
}