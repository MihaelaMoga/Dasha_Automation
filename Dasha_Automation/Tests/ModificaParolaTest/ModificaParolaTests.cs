using NUnit.Framework;
using Dasha_Automation.PageModels.POM;
using Dasha_Automation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Dasha_Automation.Tests.ModificaParolaTests
{
    class ModificaParolaTests : BaseTest
    {

        //URL-ul paginii principale a site-ului 
        string url = Utilities.FrameworkConstants.GetUrl();


        //Nota: metoda de CITIRE a datelor de teste este GetCredentialsDataCsv3 si se afla in clasa parinte BaseTest.cs
       

        //   [TestCase("testarescoalainfo@gmail.com","papadie456","")]
        //   [Test]
        [Test, TestCaseSource("GetCredentialsDataCsv3")]
        public void GoToModificaParolaPage(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina principala
            _driver.Navigate().GoToUrl(url);

            //ma loghez in cont folosind metoda din clasa parinte (BaseTest.cs)
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);
            
            ModificaParolaPage modificaParola = new ModificaParolaPage(_driver);
            modificaParola.ClickOnModificaParola();

        //verific labels din pagina "Modifica parola"
            Assert.IsTrue(modificaParola.ModificaParolaLabels("Vechea Parolă:", "Noua Parolă:", "Reintroduceţi noua parolă:", "*  - Câmpuri obligatorii"));
        }






//metoda pt CITIRE TestData din fisier de tip csv
        //citesc datele de test din fisierul testDataLogin.csv care e salvat pe calea TestData\\testDataModificaParola.csv
        private static IEnumerable<TestCaseData> GetCredentialsDataCsv()
        {
            string path = "TestData\\testDataModificaParola.csv";
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
                        //trim scoate spatiile libere din csv
                        yield return new TestCaseData(values[0].Trim(), values[1].Trim(), values[2].Trim(), values[3].Trim(), values[4].Trim(), values[5].Trim(), bool.Parse(values[6].Trim()), values[7].Trim(), values[8].Trim(), values[9].Trim(), values[10].Trim(), values[11].Trim());
                    }
                    index++;
                }
            }
        }






/*

      

                // cazuri valide: userul completeaza toate campurile corect:
                //Parola valida = minim 3 caractere (litere/numere/caractere speciale)
                //parola din 3 litere
                [TestCase("testarescoalainfo@gmail.com", "papadie456","","papadie456", "par", "par",true,"Parola a fost modificată","","","","")]
                //parola din 3 numere
                    [TestCase("testarescoalainfo@gmail.com", "par", "", "par","123", "123",true,"Parola a fost modificată","","","","")]
                //parola din 3 caractere speciale
                   [TestCase("testarescoalainfo@gmail.com", "123","","123", "!%#", "!%#",true,"Parola a fost modificată","","","","")]
                //parola din 4 caractere
                   [TestCase("testarescoalainfo@gmail.com", "!%#","", "!%#","Pa1%", "Pa1%",true,"Parola a fost modificată","","","","")]
                   [TestCase("testarescoalainfo@gmail.com", "Pa1%", "", "Pa1%", "papadie456", "papadie456",true,"Parola a fost modificată","","","","")]





        //cazuri invalide:
        //nu completez nici un camp
              [TestCase("testarescoalainfo@gmail.com", "papadie456","", "", "", "", false,"", "Acest câmp este obligatoriu.", "Acest câmp este obligatoriu.", "", "Acest câmp este obligatoriu.")]


        //cazuri invalide pt parola veche
            //nu completez vechea parola
                [TestCase("testarescoalainfo@gmail.com", "papadie456", "", "", "parola2", "parola2", false, "", "Acest câmp este obligatoriu.", "", "", "")]
             //parola veche invalida, parola noua valida
                [TestCase("testarescoalainfo@gmail.com", "papadie456", "","pa", "parola2", "parola2",false,"","","", "Parola veche este incorectă","")]


        //cazuri invalide pt noua parola:
             //nu completez noua parola - UNEORI IMI DA FAILED PT CA am folosit sleep in loc de wait !!!!!
                [TestCase("testarescoalainfo@gmail.com", "papadie456", "", "papadie456", "", "parola2", false, "", "", "Acest câmp este obligatoriu.", "", "Te rugăm să reintroduci valoarea.")]


        //cazuri invalide pt retype password:
            //nu completez retypePassword
                [TestCase("testarescoalainfo@gmail.com", "papadie456", "", "papadie456", "parola2", "", false, "", "", "", "", "Acest câmp este obligatoriu.")]
            //parola veche si noua valide, retypePass diferit de noua parola
                [TestCase("testarescoalainfo@gmail.com", "papadie456", "", "papadie456", "parola1", "parola2",false,"","","","", "Te rugăm să reintroduci valoarea.")]


        //parola veche valida, parola noua = retypePass dar invalide
                [TestCase("testarescoalainfo@gmail.com", "papadie456", "", "papadie456", "pa", "pa",false,"","","Noua parola trebuie sa aiba minim 3 caractere (litere/numere/caractere speciale)","","Noua parola trebuie sa aiba minim 3 caractere (litere/numere/caractere speciale)")]
                [TestCase("testarescoalainfo@gmail.com", "pa", "", "pa", "papadie456", "papadie456", true, "Parola a fost modificată", "","","","")]

   */   

    //    [Test]
    [Test, TestCaseSource("GetCredentialsDataCsv")]
        public void ModificaParola(string expectedEmail, string expectedParolaLogare, string expectedInvalidLoginErr, string expectedVecheaParola2, string expectedNouaParola, string expectedRetypePass, bool validChangeOfPass, string expectedModificaParolaSuccessMessage, string expectedVecheaParolaErr, string expectedNouaParolaErr, string expectedComparaParoleErr, string expectedRetypePassErr)
        {

            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina principala
            _driver.Navigate().GoToUrl(url);

        //userul completeaza credentiale valide pt a se loga in contul sau (folosesc metoda din clasa parinte BaseTest.cs)
             IntraInCont(expectedEmail, expectedParolaLogare, expectedInvalidLoginErr);

        //userul logat da click pe optiunea "Modifica Parola"
            ModificaParolaPage modificaParola = new ModificaParolaPage(_driver);
            modificaParola.ClickOnModificaParola();
            

        //completez campurile necesare pt schimbarea parolei
            modificaParola.ModificaParola(expectedVecheaParola2, expectedNouaParola, expectedRetypePass);
            

        //ASSERTURILE
            if (validChangeOfPass == true)
            {

                //verific ca schimbarea parolei s-a facut cu succes
                Assert.AreEqual(expectedModificaParolaSuccessMessage, modificaParola.TextAfterValidChangeOfPass());
            }
            else
            {
                
                if (expectedVecheaParola2 == "")
                    {
                    //verific ca apar mesajele de eroare, daca e cazul
                    Assert.AreEqual(expectedVecheaParolaErr, modificaParola.ActualErrMessageInvalidPassChange());
                    }
                if (expectedNouaParola == "")
                    {
                    Assert.AreEqual(expectedNouaParolaErr, modificaParola.ActualErrMessageInvalidNouaParolaChange());
                    }

                if (expectedRetypePass != expectedNouaParola || expectedRetypePass == "")
                    {
                      
                      Assert.AreEqual(expectedRetypePassErr, modificaParola.ActualErrMessageInvalidRetypePass());
                     
                    }

                if(expectedRetypePass == expectedNouaParola & expectedRetypePass != "")
                    {
                    Assert.AreEqual(expectedNouaParolaErr, modificaParola.ActualErrMessageInvalidNouaParolaChange());
                    Assert.AreEqual(expectedRetypePassErr, modificaParola.ActualErrMessageInvalidRetypePass());
                    }

                if(expectedParolaLogare != expectedVecheaParola2 & expectedParolaLogare != "" & expectedVecheaParola2 != "")
                    {
                   
                    Assert.AreEqual(expectedComparaParoleErr,modificaParola.ActualErrMessageInvalidVecheaParola());
                    }
            }
        }

        




    }
}
