using NUnit.Framework;
using Dasha_Automation.PageModels.POM;
using Dasha_Automation.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Dasha_Automation.Tests.ItemTest
{
    class ItemTests : BaseTest
    {

        //URL-ul paginii principale a site-ului  
        string url = Utilities.FrameworkConstants.GetUrl();



//Nota: metoda de CITIRE a datelor de teste este GetCredentialsDataCsv3 si se afla in clasa parinte BaseTest.cs

        [Category("Item")]
        [Category("Smoke")]
    //metoda pt a ajunge pe pagina produsului
       
       
        [TestCase("Cosmetice", "252833","4.50","4300 lei", false, "OYSTER", "COD PRODUS: 252833")]
        [TestCase("Cosmetice", "248758", "", "13000 lei", false, "ANUBIS", "COD PRODUS: 248758")]
        [TestCase("Genti din piele naturala", "535882", "", "26900 lei", true, "", "COD PRODUS: 535882")]
        

        [Test, Order(16)]
     //   [Test, TestCaseSource("GetCredentialsDataCsv3")]


        
            
        public void ItemPage(string expectedItemCategory, string expectedCodProdus, string expectedRating, string expectedPrice, bool discountPrice, string expectedProducator,string expectedCodProdusAfisat)
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-uluiexpectedCodProdusAfisat
            _driver.Navigate().GoToUrl(url);

            MainPage mainPage = new MainPage(_driver);
            mainPage.CloseTheCookies();

        //merg pe pagina Cosmetice
            FilterFunctionality filter = new FilterFunctionality(_driver);

            if(expectedItemCategory == "Cosmetice")
            {
                filter.ClickOnCosmetice();
                //verific ca am ajuns pe pagina COSMETICE
                Assert.AreEqual(expectedItemCategory, filter.CheckMainMenuCategories());
            }
           
            if(expectedItemCategory == "Genti din piele naturala")
            {
                filter.ClickOnGenti();
                Assert.AreEqual(expectedItemCategory, filter.CheckMainMenuCategories());
            }

        //inainte sa selectez produsul, gasesc si verific codul produsului
            ItemPage selectedItem = new ItemPage(_driver);


            if (expectedCodProdus == "252833")
            {
                Assert.AreEqual(expectedCodProdus, selectedItem.GetCodeOfItem1());
                //dau click pe produsul cu codul de mai sus
                selectedItem.GoToItem1Page();
            }

            if(expectedCodProdus == "248758")
            {
                Assert.AreEqual(expectedCodProdus, selectedItem.GetCodeOfItem2());
                selectedItem.GoToItem2Page();
            }
           
          if(expectedCodProdus == "535882")
            {
                Assert.AreEqual(expectedCodProdus, selectedItem.GetCodeOfItem3());
                selectedItem.GoToItem3Page();
            }

            //in cazul in care produsele au rating: verific ratingul produsului
            //pun if pt ca unele produse NU au rating
            if (expectedRating != "")
            {
                Assert.AreEqual(expectedRating, selectedItem.CheckRating());
            }


            //verific ca am ajuns pe pagina produsului
           
                Assert.AreEqual(expectedCodProdusAfisat, selectedItem.CheckCodeOfItem());
                Console.WriteLine(selectedItem.CheckCodeOfItem());
            

        //verific pret produs
        if(discountPrice == true)
            {
                Assert.AreEqual(expectedPrice, selectedItem.CheckDiscountPrice());
            }
            else
            {
                //pret fara discount
                Assert.AreEqual(expectedPrice, selectedItem.CheckPrice());
            }
             

        //verific daca e corect nr de telefon pt comanda telefonica
            Assert.AreEqual("0378.11.99.39 (comanda telefonica)", selectedItem.CheckTelephone());


            //verific nume producator
            //pun if pt ca nu toate produsele au producator
            if (expectedProducator != "")
            {
                Assert.AreEqual(expectedProducator, selectedItem.CheckNumeProducator());
                //merg pe site-ul producatorului si apoi Back => de verificat ca m-am intors pe pagina produsului
                selectedItem.ClickOnNumeProducator();
            }
        }
    }
}
