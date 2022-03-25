using Dasha_Automation.PageModels.POM;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dasha_Automation.Tests.CheckoutTest
{
    class CheckoutTests : BaseTest
    {
        //merg pe pagina principala a site-ului 
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
                        yield return new TestCaseData(values[0].Trim(), values[1].Trim(), values[2].Trim(), values[3].Trim(), values[4].Trim(), values[5].Trim(), values[6].Trim(), values[7].Trim(), values[8].Trim());

                    }
                    index++;
                }
            }
        }







        //  [TestCase("testarescoalainfo@gmail.com", "papadie456","", "Cosmetice","248758","1","13000 lei", "COD PRODUS: 248758","13000 lei")]
        //  [Test]

        [Category("AddToCart")]
        [Category("Smoke")]
        [Test, Order(18), TestCaseSource("GetCredentialsDataCsv")]

        public void Checkout(string expectedEmail, string expectedPass, string expectedErrMessage, string expectedItemCategory, string expectedCodProdus, string expectedQuantity, string expectedPrice, string expectedCodProdusAfisat, string expectedCartTotal)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-uluiexpectedCodProdusAfisat
            _driver.Navigate().GoToUrl(url);


        //metoda apelata din BaseTest (e in BaseTest pt ca e metoda folosita si in AddToCartTests si in CheckoutTests)
            AddToCartUserIsLogged(expectedEmail, expectedPass, expectedErrMessage, expectedItemCategory, expectedCodProdus, expectedQuantity, expectedPrice, expectedCodProdusAfisat, expectedCartTotal);      

            CheckoutPage checkout = new CheckoutPage(_driver);
            checkout.ClickOnVeziDetaliiCos();
            Assert.AreEqual("Produse comandate",checkout.CheckCheckoutDetails());


        }


    }
}
