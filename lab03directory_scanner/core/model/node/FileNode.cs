namespace core.model.node;

public class FileNode : Node
{
    public override NodeType GetType()
    {
        return NodeType.File;
    }
}