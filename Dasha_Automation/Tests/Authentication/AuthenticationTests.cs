using Dasha_Automation.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.Tests.Authentication
{
    public class AuthenticationTests : BaseTest
    {

        string url = FrameworkConstants.GetUrl();

        [Test]
        public void AuthenticationPositive()
        {
        //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            _driver.Navigate().GoToUrl(url);
        }
        


    }
}
