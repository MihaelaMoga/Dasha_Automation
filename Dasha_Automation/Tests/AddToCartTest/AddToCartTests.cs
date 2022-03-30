using NUnit.Framework;
using Dasha_Automation.PageModels.POM;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Dasha_Automation.Tests.AddToCartTest
{
    class AddToCartTests :BaseTest
    {

        //URL-ul paginii principale a site-ului  
        string url = Utilities.FrameworkConstants.GetUrl();



//metoda pt CITIRE TestData din fisier de tip csv
        //citesc datele de test din fisierul testDataLogin.csv care e salvat pe calea TestData\\testDataLogin.csv
        private static IEnumerable<TestCaseData> GetCredentialsDataCsv()
        {
            string path = "TestData\\testDataAddToCart.csv";
            //using permite sa folosim variabila reader in metoda de mai jos DOAR pe durata lui using
            //=> cand se incheie using, reader e distrusa (disposed)
            //=> using e utila cand avem de citit fisiere sau de facut CONEXIUNI LA BAZA DE DATE (inchide singur conexiunea)
            var index = 0;
            using (var reader = new StreamReader(path))
            {
                //atata timp cat nu ajung la finalul csv-ului
                while (!reader.EndOfStream)
                {
                    //citesc fiecare linie din fisierul csv
                    var line = reader.ReadLine();
                    //valorile dintr-o linie din csv sunt separate prin virgula
                    var values = line.Split(',');
                    //pentru fiecare linie , mai putin headerul
                    if (index > 0)
                    {
                        //valorile astfel separate sunt returnate ca array de valori, valorile fiind despartite prin virgula
                        yield return new TestCaseData(values[0].Trim(), values[1].Trim(), values[2].Trim(), values[3].Trim(), values[4].Trim(), values[5].Trim(),values[6].Trim(),values[7].Trim(),values[8].Trim(), values[9].Trim(), values[10].Trim(), values[11].Trim(), values[12].Trim(), values[13].Trim(), values[14].Trim());
                       
                    }
                    index++;
                }
            }
        }



        [Category("AddToCart")]
        [Category("Smoke")]
         [Test, Order(17), TestCaseSource("GetCredentialsDataCsv")]
        public void AddToCart(string expectedEmail, string expectedPass, string expectedErrMessage, string expectedItemCategory, string expectedCategoryName, string expectedCodProdusOnFilter, string expectedQuantity, string expectedUnitPrice, string expectedItemCodeOnItemPage, string expectedCartTotal, string expectedItemCodeOnContinutulCosului, string expectedItem2Category, string expectedCategory2Name, string expectedCodProdus2OnFilter, string expectedQuantityItem2)
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);


            //userul deschide pagina principala a site-ului 
            _driver.Navigate().GoToUrl(url);


        //metoda apelata din BaseTest (e in BaseTest pt ca e metoda folosita si in AddToCartTests si in CheckoutTests)
            AddToCartUserIsLogged(expectedEmail, expectedPass, expectedErrMessage, expectedItemCategory, expectedCategoryName,expectedCodProdusOnFilter, expectedQuantity, expectedUnitPrice, expectedItemCodeOnItemPage, expectedCartTotal, expectedItemCodeOnContinutulCosului, expectedItem2Category, expectedCategory2Name, expectedCodProdus2OnFilter, expectedQuantityItem2);
        

        }



    }
}
