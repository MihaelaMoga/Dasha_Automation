using Dasha_Automation.PageModels.POM;
using Dasha_Automation.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.Tests.MainPageTests
{
    public class MainPageTests : BaseTest
    {

        string url = FrameworkConstants.GetUrl();


        [Test]
        public void GoToContNou()
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);


            //dechid pagina principala a site-ului
            _driver.Navigate().GoToUrl(url);

            MainPage mainPage = new MainPage(_driver);
            //pe pagina principala inchid bannerul de cookie-uri
            mainPage.CloseTheCookies();


            //pe pagina principala: dau click pe optiunea Contul Meu/CONT NOU
            mainPage.GoToContNouPage();
            //verific ca am ajuns pe pagina unde apare formularul numit "Client nou"
            Assert.AreEqual("Client nou",mainPage.CheckContNouPage());
        }

    }
}
