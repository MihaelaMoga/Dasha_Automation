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


      
//metoda pt CITIRE TestData din fisier de tip csv
        //citesc datele de test din fisierul testDataLogin.csv care e salvat pe calea TestData\\testDataLogin.csv
        private static IEnumerable<TestCaseData> GetCredentialsDataCsv()
        {
            string path = "TestData\\testDataItemPage.csv";
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
                        yield return new TestCaseData(values[0].Trim(), values[1].Trim(), values[2].Trim(), values[3].Trim(), values[4].Trim(), values[5].Trim(), values[6].Trim(), bool.Parse(values[7].Trim()), values[8].Trim(), values[9].Trim());
                    }
                    index++;
                }
            }
        }


        //  [TestCase("Cosmetice", "252833","4.56","4300 lei", false, "OYSTER", "COD PRODUS: 252833")]
        //  [TestCase("Cosmetice", "248758", "", "13000 lei", false, "ANUBIS", "COD PRODUS: 248758")]
        //  [TestCase("Genti din piele naturala", "535882", "", "26900 lei", true, "", "COD PRODUS: 535882")]
        // [Test]

        [Category("Item")]
        [Category("Smoke")]
        [Test, Order(16), TestCaseSource("GetCredentialsDataCsv")]
        public void ItemPage(string expectedEmail, string expectedPass, string expectedErrMessage, string expectedItemCategory, string expectedCodProdusOnFilter, string expectedRating, string expectedUnitPrice, bool discountPrice, string expectedProducator,string expectedItemCodeOnItemPage)
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);



         //userul merge pe pagina principala si apoi se logheaza 
            IntraInCont(expectedEmail, expectedPass, expectedErrMessage);
         //userul alege categoria de produse si merge pe pagina unui produs
            GoToItemPageUserIsLogged(expectedItemCategory, expectedCodProdusOnFilter);

            
            ItemPage selectedItem = new ItemPage(_driver);
            //in cazul in care produsele au rating: verific ratingul produsului
            //pun if pt ca unele produse NU au rating
            if (expectedRating != "")
            {
                Assert.AreEqual(expectedRating, selectedItem.CheckRating());
            }


            //verific ca am ajuns pe pagina produsului
           
                Assert.AreEqual(expectedItemCodeOnItemPage, selectedItem.CheckCodeOfItem());
                Console.WriteLine(selectedItem.CheckCodeOfItem());
            

        //verific pret produs
        if(discountPrice == true)
            {
                Assert.AreEqual(expectedUnitPrice, selectedItem.CheckDiscountPrice());
            }
            else
            {
                //pret fara discount
                Assert.AreEqual(expectedUnitPrice, selectedItem.CheckPrice());
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
