using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfMapApplication;
using System.Threading.Tasks;

namespace UnitTestProject1
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Sanity()
    {
      MainViewModel vm = new MainViewModel();
      vm.SearchText = "Test";
      vm.SummaryText = vm.SearchText;

      Assert.IsTrue(vm.SummaryText == "Test");
    }

    [TestMethod]
    public async Task SearchTest()
    {
      MainViewModel vm = new MainViewModel();
      vm.SearchText = "palm";

      await vm.DoSearch();

      Assert.IsTrue(vm.SearchResults != null);
      Assert.IsTrue(vm.SearchResults.Count > 0);
    }
  }
}
