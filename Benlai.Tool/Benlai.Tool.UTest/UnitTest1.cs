using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benlai.Tool.Service;
using Newtonsoft.Json;

namespace Benlai.Tool.UTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLoadSln()
        {
            /*
              @"D:\WorkCompany\SH-Benlai\Codes\Benlai.BackstageERP\branches\mmbuy_ors1.0.sln"

               @"D:\WorkCompany\Testcode\Benlai.Tool\Benlai.Tool.sln"
             */
            string path = @"D:\WorkCompany\SH-Benlai\Codes\Benlai.BackstageERP\branches\mmbuy_ors1.0.sln";
            var slnInfo = SolutionService.Instance.LoadSlnProjectInfo(path);
            slnInfo = SolutionService.Instance.LoadProjectInfo(slnInfo);
            var result = SolutionService.Instance.GetPackInfos(slnInfo);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
