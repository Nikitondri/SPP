using core.exception;
using core.model.node;
using core.service;

namespace tests;

public class LocalStorageScannerTest
{
    [Test]
    public void InvalidPathTest()
    {
        var scanner = new LocalStorageScanner();
        Assert.Throws<IncorrectPathException>(
            () => scanner.Start("Invalid")
        );
    }

    [Test]
    public void EmptyPackageTest()
    {
        var root = StartScannerAndGetResult("../../../resources/empty_package");

        Assert.Multiple(() =>
        {
            Assert.That(root.Childrens, Is.Null);
            Assert.That(root.Size, Is.EqualTo(0));
        });
    }

    [Test]
    public void PackageWithLinkTest()
    {
        var root = StartScannerAndGetResult("../../../resources/package_with_link");

        Assert.Multiple(() =>
        {
            Assert.That(root.Size, Is.EqualTo(9));
            Assert.That(root.Childrens!, Has.Count.EqualTo(2));
            Assert.That(root.Percent, Is.EqualTo(1.0));
            Assert.True(IsFilesInDirectoryExist(root, new List<string>() { "link.lnk", "text.txt" }));
            Assert.That(
                root.Childrens!.ToArray().Aggregate<Node?, long?>(0, (current, x) => current + x.Size),
                Is.EqualTo(9)
            );
            Assert.That(
                root.Childrens!.ToArray().Aggregate<Node?, double?>(0, (current, x) => current + x.Percent),
                Is.EqualTo(1)
            );
        });
    }

    [Test]
    public void PackageWithPackageAndFilesTest()
    {
        var root = StartScannerAndGetResult("../../../resources/package_with_package");
        Node childPackage = new Package();
        foreach (var child in root.Childrens!)
        {
            if (child.Childrens is not null && child.Childrens.Count == 2)
            {
                childPackage = child;
            }
        }

        Assert.Multiple(() =>
        {
            Assert.That(root.Size, Is.EqualTo(20));
            Assert.That(root.Childrens!, Has.Count.EqualTo(3));
            Assert.True(IsFilesInDirectoryExist(root, new List<string>() { "file1.txt", "file2.txt", "package" }));
            Assert.True(IsFilesInDirectoryExist(childPackage, new List<string> { "file3.txt", "file4.txt" }));
        });
    }

    [Test]
    public void StopScannerTest()
    {
        Node rootWithoutStop = StartScannerAndGetResult("C:\\Users\\nikita.zakharenko\\Desktop");
        Node rootWithStop = StartScannerAndGetResultWithStop("C:\\Users\\nikita.zakharenko\\Desktop");
        Assert.True(rootWithoutStop.Size > rootWithStop.Size);
    }

    private bool IsFilesInDirectoryExist(Node node, IEnumerable<string> fileNames)
    {
        var names = node.Childrens!.ToArray().Select(child => child.Name).ToList();
        return fileNames.All(name => names.Contains(name));
    }

    private Node StartScannerAndGetResult(string path)
    {
        var scanner = new LocalStorageScanner();
        scanner.Start(path);
        while (!scanner.IsFinish())
        {
        }

        scanner.Stop();
        return scanner.GetResult();
    }
    
    private Node StartScannerAndGetResultWithStop(string path)
    {
        var scanner = new LocalStorageScanner();
        var task = Task.Run(() => scanner.Start(path));
        Thread.Sleep(100);
        scanner.Stop();
        while (!scanner.IsFinish())
        {
        }
        Task.WaitAll(task);
        return scanner.GetResult();
    }
}