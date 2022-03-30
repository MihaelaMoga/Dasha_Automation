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





 //metoda pt a ajunge pe pagina de INCALTAMINTE/IMBRACAMINTE/GENTI/COSMETICE/NOUTATI/OUTLET
        //[TestCase("testarescoalainfo@gmail.com", "papadie456", "")]
        //  [Test]
        [Category("Filter")]
        [Test, TestCaseSource("GetCredentialsDataCsv4"), Order(10)]
    
        public void ClickOnAnyMainCategory(string expectedEmail, string expectedParola, string expectedInvalidLoginErr, string expectedItemCategory, string expectedCategoryName)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina principala a site-ului
            _driver.Navigate().GoToUrl(url);


        //userul completeaza credentiale valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);

        //userul da click pe meniul principal GENTI/COSMETICE/INCALTAMINTE/IMBRACAMINTE/NOUTATI/OUTLET (conform info din TestData)
            FilterFunctionality filter = new FilterFunctionality(_driver, expectedItemCategory);
            filter.GoToItemMainCategory();
            
        //verificam ca am ajuns in meniul corect   
           if(expectedItemCategory != "6")
            {
                Assert.AreEqual(expectedCategoryName, filter.CheckMainMenuCategories());
            }
            else
            {
                Assert.AreEqual(expectedCategoryName, filter.CheckOutletPage());
            }
        }

       


    }
}
