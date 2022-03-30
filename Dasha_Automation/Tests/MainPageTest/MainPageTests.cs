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


      //  [Category("Smoke")]
        [Category("MainPage")]

        [Test(Description = "step1: Go to main page & close the cookies banner; step 2: go to CONT NOU page and check for Client nou text"),Order(2)]
       // [Parallelizable(ParallelScope.Self)]
        public void GoToContNou()
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);


            //dechid pagina principala a site-ului
            _driver.Navigate().GoToUrl(url);
          

            MainPage mainPage = new MainPage(_driver);

            //testez ca valoarea cookie-ului rgisanonymous = true
            Assert.AreEqual("true",mainPage.GetACookieValue());

            //inchid bannerul de cookie-uri din pagina principala 
            mainPage.CloseTheCookies();


            //pe pagina principala: dau click pe optiunea Contul Meu/CONT NOU
            mainPage.GoToContNouPage();
            //verific ca am ajuns pe pagina unde apare formularul numit "Client nou"
            Assert.AreEqual("Client nou",mainPage.CheckContNouPage());
        }





        [Category("MainPage")]
        [Test, Order(1)]
        // [Parallelizable(ParallelScope.Self)]
        public void GetSiteTitle()
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);


            //dechid pagina principala a site-ului
            _driver.Navigate().GoToUrl(url);

            MainPage mainPage = new MainPage(_driver);
            Assert.AreEqual("DASHA.ro - Magazin online de incaltaminte, haine, genti si accesorii de dama", mainPage.GetDocumentTitle());
        }

    }
}
