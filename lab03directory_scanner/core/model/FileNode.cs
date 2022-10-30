using core.model.node;

namespace core.model;

public class FileNode : Node
{
    public override NodeType GetType()
    {
        return NodeType.File;
    }
}