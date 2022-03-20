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


//metoda pt a ajunge pe pagina produsului
         [TestCase("Cosmetice", "252833","4.50","4300 lei", "OYSTER", "COD PRODUS: 252833", "Cantitate: 250 ml.")]
         [Test]
     //   [Test, TestCaseSource("GetCredentialsDataCsv3")]
        public void ItemPage(string expectedItemCategory, string expectedCodProdus, string expectedRating, string expectedPrice, string expectedProducator,string expectedCodProdusAfisat,string expectedCantitate)
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
            filter.ClickOnCosmetice();
        //verific ca am ajuns pe pagina COSMETICE
            Assert.AreEqual(expectedItemCategory, filter.CheckMainMenuCategories());

        //inainte sa selectez produsul, gasesc si verific codul produsului
            ItemPage selectedItem = new ItemPage(_driver);
            Assert.AreEqual(expectedCodProdus, selectedItem.GetCodProdus());
        //dau click pe produsul cu codul de mai sus
            selectedItem.ClickOnSelectedSampon();
          

            //verific ratingul produsului 252833
            Assert.AreEqual(expectedRating, selectedItem.CheckRating());


        //verific ca am ajuns pe pagina produsului cu cod 252833
            Assert.AreEqual(expectedCodProdusAfisat, selectedItem.CheckCodSampon());
            Console.WriteLine(selectedItem.CheckCodSampon());


        //verific pret produs
             Assert.AreEqual(expectedPrice, selectedItem.CheckPrice());

        //verific daca e corect nr de telefon pt comanda telefonica
            Assert.AreEqual("0378.11.99.39 (comanda telefonica)", selectedItem.CheckTelephone());


        //verific nume producator
            Assert.AreEqual(expectedProducator, selectedItem.CheckNumeProducator());
        //merg pe site-ul producatorului si apoi Back => de verificat ca m-am intors pe pagina produsului
           selectedItem.ClickOnNumeProducator();


         //verific cantitatea produsului care apare la DESCRIERE  
            Assert.AreEqual(expectedCantitate, selectedItem.CheckCantitateCosmetice());

            


          


            /*
                    //click pe meniul IMBRACAMINTE si apoi click pe primul produs din pagina IMBRACAMINTE
                        filter.ClickOnImbracaminte();
                        Assert.AreEqual("Haine dama", filter.CheckMainMenuCategories());



                        selectedItem.ClickOnRochie();
                        //verific ca am ajuns pe pagina rochiei cu cod 508303
                        Assert.AreEqual("COD PRODUS: 508303", selectedItem.CheckRochiePage());

            */

        }





    }
}
