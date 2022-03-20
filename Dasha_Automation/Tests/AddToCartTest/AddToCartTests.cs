using NUnit.Framework;
using Dasha_Automation.PageModels.POM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.Tests.AddToCartTest
{
    class AddToCartTests :BaseTest
    {

        //URL-ul paginii principale a site-ului  
        string url = Utilities.FrameworkConstants.GetUrl();



//DE ADAUGAT DATELE DE TEST AICI SI IN PARAMETRII TESTULUI DE MAI JOS
        [Test]
        public void AddToCart()
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-uluiexpectedCodProdusAfisat
            _driver.Navigate().GoToUrl(url);

            MainPage mainPage = new MainPage(_driver);
            mainPage.CloseTheCookies();
 
         //intru in meniul COSMETICE si apoi click pe   
            FilterFunctionality filter = new FilterFunctionality(_driver);
            filter.ClickOnCosmetice();

            ItemPage ip = new ItemPage(_driver);
            ip.ClickOnSelectedSampon();

        //adaug primul produs in cos
            AddToCart itemToCart = new AddToCart(_driver);
            itemToCart.ClickOnAdauga();


         //verific ca am adaugat produsul in cos
            itemToCart.CheckContinutulCosului();
            Assert.AreEqual("5800 lei", itemToCart.CheckContinutulCosului());
            Console.WriteLine("Pretul produsului este {0}", itemToCart.CheckContinutulCosului());



    //de facut assert pt numarul de produse din cos
    //de facut assert pt cod produse in cos
    //click pe Detalii cos


        }



    }
}
