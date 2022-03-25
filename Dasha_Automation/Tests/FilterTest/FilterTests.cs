using NUnit.Framework;
using Dasha_Automation.PageModels.POM;
using Dasha_Automation.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Dasha_Automation.Tests.FilterTest
{
  
    class FilterTests : BaseTest
    {
        //URL-ul paginii principale a site-ului  
        string url = Utilities.FrameworkConstants.GetUrl();



//Nota: metoda de CITIRE a datelor de teste este GetCredentialsDataCsv3 si se afla in clasa parinte BaseTest.cs

        
        //metoda pt a ajunge pe pagina de INCALTAMINTE
        //[TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        // [Test]
        [Category("Filter")]
        [Test,TestCaseSource("GetCredentialsDataCsv3"), Order(8)]
        public void ClickOnIncaltaminte(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

 
        //userul completeaza credentialele valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);
            

            //userul da click pe meniul INCALTAMINTE
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnIncaltaminte();

            Assert.AreEqual("Încălțăminte dama din piele naturala", filter.CheckMainMenuCategories());
        }




        //metoda pt a ajunge pe pagina de IMBRACAMINTE
        //[TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        //   [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv3"), Order(9)]
        public void ClickOnImbracaminte(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

         //userul completeaza credentialele valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

        //userul da click pe meniul IMBRACAMINTE
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnImbracaminte();

            Assert.AreEqual("Haine dama", filter.CheckMainMenuCategories());
        }





        //metoda pt a ajunge pe pagina de GENTI
        //[TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        //  [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv3"), Order(10)]
        public void ClickOnGenti(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina principala a site-ului
            _driver.Navigate().GoToUrl(url);


        //userul completeaza credentiale valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

        //userul da click pe meniul GENTI
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnGenti();
            


            Assert.AreEqual("Genti din piele naturala", filter.CheckMainMenuCategories());
        }











        //metoda pt a ajunge in submeniul COSMETICE
        // [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv3"), Order(11)]
        public void ClickOnCosmetice(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);


            //merg pe pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

            //userul completeaza credentiale valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

            //userul da click pe meniul COSMETICE
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnCosmetice();

        }






        //metoda pt a ajunge in pagina de COSMETICE/INGRIJIREA TENULUI
        //[TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        //  [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv3"), Order(12)]
        public void ClickOnIngrijireaTenului(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //merg pe pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

            //userul completeaza credentiale valide pt a se loga in contul sau
         //   IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

            //userul da click pe meniul COSMETICE/Ingrijirea Tenului
            FilterFunctionality filter = new FilterFunctionality(_driver);
  
            filter.ClickOnIngrijireaTenului();


              Assert.AreEqual("Ingrijirea tenului", filter.CheckSubmenuCategories());
        }




        //metoda pt a ajunge in pagina de COSMETICE/INGRIJIREA PARULUI
        //[TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        //  [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv3"), Order(13)]
        public void ClickOnIngrijireaParului(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //merg pe pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

            //userul completeaza credentiale valide pt a se loga in contul sau
            //   IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

            //userul da click pe submeniul COSMETICE/Ingrijirea Parului
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnIngrijireaParului();
            Assert.AreEqual("Ingrijirea parului", filter.CheckSubmenuCategories());
        }



        //metoda pt a ajunge in pagina de NOUTATI
        //[TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        // [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv3"), Order(14)]
        public void ClickOnNoutati(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //merg pe pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

        //userul completeaza credentialele valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

        //dau click pe meniul NOUTATI
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnNoutati();


            Assert.AreEqual("Produse nou adaugate", filter.CheckMainMenuCategories());
        }






        //metoda pt a ajunge in pagina de OUTLET
        //  [TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        // [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv3"), Order(15)]
        public void ClickOnOutlet(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

        //userul completeaza credentiale valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

        //userul da click pe meniul OUTLET
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnOutlet();


            Assert.AreEqual("OUTLET", filter.CheckOutletPage());
        }





    }
}
