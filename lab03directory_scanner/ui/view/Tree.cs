using System.Collections.ObjectModel;
using core.model.node;
using lab03directory_scanner.factory;
using lab03directory_scanner.view.node;

namespace lab03directory_scanner.view;

internal static class Tree
{
    public static NodeView ToTreeViewNode(this Node node)
    {
        var viewNode = ViewNodeFactory.CreateNode(node.GetType());
        viewNode.Name = node.Name;
        viewNode.Size = viewNode.Size;
        viewNode.Percent = node.Percent;

        if (node.Childrens != null)
        {
            viewNode.ViewNodes = new ObservableCollection<NodeView>();
        
            foreach (var child in node.Childrens)
            {
                var childNode = child.ToTreeViewNode();
                viewNode.ViewNodes.Add(childNode);
            }
        }
        return viewNode;
    }
}