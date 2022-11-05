using core.exception;
using core.factory;
using core.model;
using core.model.node;
using core.repository;
using core.service.calculate;

namespace core.service;

public class LocalStorageScanner : IScanner
{
    private readonly IRepository<Node> _repository;
    private readonly ThreadPoolScanner _scanner;
    private readonly INodesCalculator _percentCalculator;
    private readonly INodesCalculator _sizeCalculator;
    private readonly object _obj = new();


    public LocalStorageScanner()
    {
        _repository = new InMemoryNodesRepository();
        _scanner = new ThreadPoolScanner();
        _percentCalculator = new PercentNodesCalculator();
        _sizeCalculator = new SizeNodeCalculator();
    }

    public bool IsFinish()
    {
        return _scanner.IsFinish();
    }

    public void Start(string path)
    {
        ThrowExceptionIfNotDirectory(path);
        _scanner.Start();
        _scanner.AddTask(ScanFile, new FileScanData(null, path));
    }

    private void ScanFile(FileScanData data)
    {
        string filePath;
        NodeType fileType;
        long? fileSize;
        
        if (data.ParentId.HasValue)
        {
            var parentNode = _repository.FindById(data.ParentId.Value);
            filePath = parentNode.Route + "/" + data.FileName;
        }
        else
            filePath = data.FileName;

        var attr = File.GetAttributes(filePath);

        if (attr.HasFlag(FileAttributes.Directory))
        {
            fileSize = null;
            fileType = NodeType.Package;
        }
        else
        {
            fileSize = (new FileInfo(filePath)).Length;
            fileType = NodeType.File;
        }

        var node = NodeFactory.CreateNode(fileType);
        node.Name = data.FileName;
        if (fileSize != null) node.Size = fileSize.Value;

        AddNode(data.ParentId, node);

        if (fileType != NodeType.Package) return;
        var subFiles = Directory.GetFileSystemEntries(filePath);
        foreach (var file in subFiles)
        {
            var scanData = new FileScanData(node.Id, Path.GetFileName(file));
            _scanner.AddTask(ScanFile, scanData);
        }
    }
    
    

    private void AddNode(int? parentId, Node newNode)
    {
        if (_repository.GetSize() != 0 && parentId.HasValue)
        {
            lock (_obj)
            {
                var parentNode = _repository.FindById(parentId.Value);
                if (parentNode is null)
                    throw new ArgumentException();
            
                newNode.Parent = parentNode;
                parentNode.Childrens.Add(newNode);
            }
        }
        _repository.Add(newNode);
    }

    private void ThrowExceptionIfNotDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new IncorrectPathException();
        }
    }

    public void Stop()
    {
        _scanner.Stop();
    }

    public Node GetResult()
    {
        var root = _repository.FindById(0);
        _percentCalculator.Calculate(root);
        _sizeCalculator.Calculate(root);
        return root;
    }
}