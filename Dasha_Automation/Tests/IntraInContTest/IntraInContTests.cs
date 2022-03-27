using NUnit.Framework;
using Dasha_Automation.PageModels.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dasha_Automation.Tests.IntraInContTest
{
    public class IntraInContTests : BaseTest
    {

        //merg pe pagina principala a site-ului 
        string url = Utilities.FrameworkConstants.GetUrl();

        [Category("IntraInCont")]
        [Test, Order(6)]
        public void GoToIntraInContPage()
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            _driver.Navigate().GoToUrl(url);
            MainPage mainPage = new MainPage(_driver);
            mainPage.CloseTheCookies();


            //vreau sa ajung pe pagina de login => pt asta creez un obiect de tip LoginPage
            IntraInContPage intraInCont = new IntraInContPage(_driver);

            //dau click pe iconita "Contul meu"
            intraInCont.ClickOnContulMeu();
            intraInCont.ClickOnIntraInCont();

        //verific ca pe pagina de Login apare textul "SAU INTRA IN CONT FOLOSIND"
            Assert.AreEqual("SAU INTRA IN CONT FOLOSIND:", intraInCont.ActualIntraInContText());

        //verific labels din pagina Intra in Cont
            Assert.IsTrue(intraInCont.IntraInContLabels("E-mail:", "Parola:", "SAU INTRA IN CONT FOLOSIND:", "Ai uitat parola?", "Nu ai cont? Creeaza cont."));
        }








//metoda pt CITIRE TestData din fisier de tip csv
        //citesc datele de test din fisierul testDataLogin.csv care e salvat pe calea TestData\\testDataLogin.csv
        private static IEnumerable<TestCaseData> GetCredentialsDataCsv()
        {
            string path = "TestData\\testDataLogin.csv";
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
                        yield return new TestCaseData(values[0].Trim(), values[1].Trim(), values[2].Trim(), bool.Parse(values[3].Trim()));
                    }
                    index++;
                }
            }
        }




        /*

                //TC: Verify user can login using only VALID data
        //input VALID
                [TestCase("124@gmail.com", "papadie123", "",true)]

        //INPUT INVALID
                [TestCase("124@gmail.com", "", "Adresa de email sau parola sunt incorecte",false)]
                [TestCase("", "papadie123", "Adresa de email sau parola sunt incorecte",false)]
                [TestCase("", "", "Adresa de email sau parola sunt incorecte",false)]

            //user sau parola pt care nu exista cont   
                //email corect + parola gresita
                [TestCase("124@gmail.com", "p5tfr#$", "Adresa de email sau parola sunt incorecte", false)]
                //email gresit + parola corecta
                [TestCase("999@gmail.com", "papadie123", "Adresa de email sau parola sunt incorecte", false)]
                //email gresit + parola gresita
                [TestCase("999@gmail.com", "p5tfr#$", "Adresa de email sau parola sunt incorecte", false)]
        */



        [Category("IntraInCont")]
        [Category("Smoke")]
       // [Test]
        [Test, TestCaseSource("GetCredentialsDataCsv"), Order(7)]
        public void Login(string expectedEmail, string expectedParola, string expectedInvalidLoginErr, bool loginValid)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //cu comanda de mai jos ajung pe pagina principala
            _driver.Navigate().GoToUrl(url);


            IntraInCont2(expectedEmail, expectedParola, expectedInvalidLoginErr);
            
            IntraInContPage intraInCont = new IntraInContPage(_driver);
            if (loginValid == true)
            {
                //daca Login e valid, trebuie sa ajunga pe pagina urmatoare unde apare "Istoric comenzi"
                Assert.AreEqual("Istoric comenzi", intraInCont.TextAfterValidLogin());
            }
            else
            {
                //compar expectedEmailErr (mesajul de eroare pt lipsa email DIN SPECIFICATII si preluate in TestCase) cu mesajul care apare efectiv in site
                Assert.AreEqual(expectedInvalidLoginErr, intraInCont.ActualInvalidLoginErrMessage());
            }


        }
    }
}
