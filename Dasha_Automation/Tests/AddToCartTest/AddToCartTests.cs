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

        [Category("AddToCart")]
        [Category("Smoke")]

//DE ADAUGAT DATELE DE TEST AICI SI IN PARAMETRII TESTULUI DE MAI JOS
        
        [TestCase("Cosmetice", "248758", "14500 lei")]
        [TestCase("Genti din piele naturala", "535882", "26900 lei")]
        [Test, Order(17)]
        public void AddToCart(string expectedItemCategory, string expectedCodProdus,string expectedPrice)
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
            


            if (expectedItemCategory == "Cosmetice")
            {
                filter.ClickOnCosmetice();

                ItemPage ip = new ItemPage(_driver);
                if (expectedCodProdus == "248758")
                  {
                    ip.GoToItem2Page();
                  }
            }


            if(expectedItemCategory == "Genti din piele naturala" & expectedCodProdus == "535882")
            {
                filter.ClickOnGenti();

                ItemPage ip = new ItemPage(_driver);
                ip.GoToItem3Page();
            }
            
        //adaug produsul in cos
            AddToCartPage itemToCart = new AddToCartPage(_driver);
             itemToCart.ClickOnAdauga();
          


           //verific ca am adaugat produsul in cos
          //  itemToCart.CheckContinutulCosului();
            //  Assert.AreEqual(expectedPrice, itemToCart.CheckContinutulCosului());
            // Console.WriteLine("Pretul produsului este {0}", itemToCart.CheckContinutulCosului());

            Assert.AreEqual("«  VEZI SI ALTE PRODUSE", itemToCart.CheckContinutulCosuluiFinal());

    //de facut assert pt numarul de produse din cos
    //de facut assert pt cod produse in cos
    //click pe Detalii cos


        }



    }
}
