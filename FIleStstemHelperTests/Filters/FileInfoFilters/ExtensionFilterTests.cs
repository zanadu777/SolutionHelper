using FileSystemHelper.Filters;
using FileSystemHelper.Filters.FileInfoFilters;
using FluentAssertions;

namespace FIleStstemHelperTests
{
  public class ExtensionFilterTests
  {

    List<FileInfo> Files { get; set; } = new();
    [SetUp]
    public void Setup()
    {
      Files.Add(new FileInfo(@"c:\test\web1.html"));
    }

    [Test]
    public void SingleExtensionTest()
    {
      var filter = new ExtensionFilter  { ExclusionType = ExclusionType.Inclusive, IsEnabled = true, Extensions = new List<string>{".html"}};
      var file = new FileInfo(@"c:\test\web1.html");


      filter.IsIncluded(file).Should().BeTrue();
      filter.IsExcluded(file).Should().BeFalse();

      Assert.Pass();
    }

    public void MultipleExtensionTest()
    {

    }
  }
}