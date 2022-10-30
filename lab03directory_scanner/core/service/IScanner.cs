using core.model.node;

namespace core.service;

public interface IScanner
{
    bool IsFinish();
    void Start(string path);
    void Stop();
    Node GetResult();
}