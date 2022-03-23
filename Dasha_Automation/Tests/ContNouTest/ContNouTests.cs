using Dasha_Automation.PageModels.POM;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.Tests.ContNouTest
{
    public class ContNouTests : BaseTest
    {

        //merg pe pagina principala a site-ului 
        string url = Utilities.FrameworkConstants.GetUrl();



        [Category("ContNou")]
        [Test, Order(3)]
        public void CheckLabelsContNou()
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);


        //merg pe pagina de CONT NOU, adica pe url: https://www.dasha.ro/cont-nou/
            _driver.Navigate().GoToUrl(url);


           MainPage mainPage = new MainPage(_driver);
           mainPage.CloseTheCookies();
            

            //dau click pe Contul Meu/CONT NOU
            mainPage.GoToContNouPage();
   
        //pt a testa pagina CONT NOU creez un obiect de tip ContNouPage 
            ContNouPage contNou = new ContNouPage(_driver);


        //parametrii de mai jos ii iau din specificatii
            Assert.IsTrue(contNou.ContNouLabels("Nume", "Prenume", "Email", "Parola"));
        }








        /*

        //TC: Verify user can register a Dasha account using VALID data
                        //Nume valid: minim 3 litere oriunde in Nume 
                        [TestCase("Ion", "Faur", "craciun20@yahoo.com", "parola", true, true, "", "", "")]

                        //Prenume valid: minim 3 litere oriunde in Prenume 
                        [TestCase("Ion", "Ana", "ana7@yahoo.com", "parola", true, true, "", "", "")]

                        //structura email valid: {1}@{2}.{2} +  inainte de @ sunt acceptate doar anumite caractere (.-_)
                        //Parola valida: minim 3 caractere (litere/numere/caractere speciale)


                        //newsletter NU e mandatory
                        [TestCase("Ioana", "Dragomir", "cirja@yahoo.com", "balauri", false, true, "", "", "")]
         */

        [Category("ContNou")]
        [Category("Smoke")]
        [Test(Description ="Create a new Dasha account using the Random generic class"), Order(4)]
        public void ContNouValid()
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //merg pe pagina de CONT NOU, adica pe url: https://www.dasha.ro/cont-nou/
            _driver.Navigate().GoToUrl(url);

            MainPage mainPage = new MainPage(_driver);
            mainPage.CloseTheCookies();
            mainPage.GoToContNouPage();

            //creez un obiect de tip ContNouPage pt a testa pagina de CONT NOU 
            ContNouPage contNou = new ContNouPage(_driver);


//ASEERT-uri pt INPUT VALID
    //caz 1: newsletter = true
            var nume = Utilities.Utils.GenerateRandomNumePrenumeStringCount(3);
            var prenume = Utilities.Utils.GenerateRandomNumePrenumeStringCount(3);
                var emailPart1 = Utilities.Utils.GenerateRandomParolaAndEmailStringCount(1);
                var emailPart2 = Utilities.Utils.GenerateRandomEmailPart22StringCount(2);
                var emailPart3 = Utilities.Utils.GenerateRandomEmailPart32StringCount(2);
            var email = emailPart1 + "@" + emailPart2 + "." + emailPart3;
            var parola = Utilities.Utils.GenerateRandomParolaAndEmailStringCount(3);
            contNou.ContNou(nume, prenume, email, parola, true, true);
            Assert.AreEqual("Istoric comenzi", contNou.TextAfterContNouValid());




            //la CREAREA UNUI CONT CU SUCCES: vreau sa afisez credentialele generate prin folosirea clasei Random
            /*
                 StringBuilder sb = new StringBuilder();
                var numeContCreat = sb.Append(nume).ToString();
                var prenumeContCreat = sb.Append(prenume).ToString();
                var emailContCreat = sb.Append(emailPart1 + "@"+ emailPart2 +"."+ emailPart3).ToString();
                var parolaContCreat = sb.Append(parola).ToString();
                Console.WriteLine("Nume: {0}, Prenume: {1}, Email: {2}, Parola: {3}", numeContCreat, prenumeContCreat, emailContCreat, parolaContCreat);
            */
            Console.WriteLine("nume: {0}, prenume: {1}, email: {2}, parola: {3}", nume, prenume, email, parola);
          

        }
 


       


         //TC: Verify user cannot register a new Dasha account using INVALID data
                //Nume invalid: Nume din 2 litere
                [TestCase("eu", "Craciun", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //caz invalid: Nume din 3 caractere dar incepe numai 2 litere
                [TestCase("e12", "Craciun", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //caz invalid: Nume din 3 numere
                [TestCase("123", "Craciun", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //caz invalid: Nume din 2 litere si 1 caracter special
                [TestCase("12.", "Craciun", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]





                //Prenume invalid: 
                [TestCase("Ion", "eu", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //caz invalid: Nume din 3 caractere dar incepe numai 2 litere
                [TestCase("Ion", "e12", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //caz invalid: Nume din 3 numere
                [TestCase("Ion", "123", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //caz invalid: Nume din 2 litere si 1 caracter special
                [TestCase("Ion", "12.", "perna@yahoo.com", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]



                //Email invalid:
                //lipseste @
                [TestCase("Ion", "Ana", "ago.ro", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //lipseste punctul 
                [TestCase("Ion", "Ana", "a@goro", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //nici un caracter intre @ si punct
                [TestCase("Ion", "Ana", "aa@.ro", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //nimic dupa punct
                [TestCase("Ion", "Ana", "a@go.", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                //doar caracter special inainte de @
                [TestCase("Ion", "Ana", "/@go.ro", "parola", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]



                //Parola invalida
                [TestCase("Ion", "Ana", "bambus@yahoo.com", "pa", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                [TestCase("Ion", "Ana", "bambus@yahoo.com", "12", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                [TestCase("Ion", "Ana", "bambus@yahoo.com", "1#", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]




     //ALTE CAZURI INVALIDE
                //campurile Nume, Prenume, Email, Parola si termenii sunt mandatory
                //userul NU completeaza unul din campurile Nume, Prenume, Email, Parola
                [TestCase("", "Violeta", "abc@yahoo.com", "balauri", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                [TestCase("Ioana", "", "abc@yahoo.com", "balauri", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]
                [TestCase("Ioana", "Violeta", "", "balauri", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]

                [TestCase("Ioana", "Violeta", "abc@yahoo.com", "", true, true, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "", "")]


            //userul NU bifeaza termenii (checkbox Mandatory)
                [TestCase("Ioana", "Violeta", "abc@yahoo.com", "balauri", true, false, "", "Trebuie sa fiti de acord cu termenii si conditiile.", "")]


                //userul nu completeaza Nume si NU bifeaza termenii - NU-MI IESE
                [TestCase("", "Violeta", "abc@yahoo.com", "balauri", true, false, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "Trebuie sa fiti de acord cu termenii si conditiile.", "")]

                //userul NU completeaza nici un camp - NU-MI IESE
                [TestCase("", "", "", "", false, false, "Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate", "Trebuie sa fiti de acord cu termenii si conditiile.", "")]




        //userul completeaza un email deja folosit la crearea unui cont => apare mesaj de eroare: Exista deja un cont cu aceasta adresa de email    
                [TestCase("Ioana", "Dragomir", "cirja@yahoo.com", "balauri", false, true, "", "", "Exista deja un cont cu aceasta adresa de email")]



        [Category("ContNou")]
        [Test, Order(5)]
        public void ContNouInvalid(string expectedNume, string expectedPrenume, string expectedEmail, string expectedParola, bool newsletter, bool terms, string expected4FieldsErrMessage, string expectedTermsAlertErr, string expectedSameEmailErrMessage)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //merg pe pagina de CONT NOU, adica pe url: https://www.dasha.ro/cont-nou/
            _driver.Navigate().GoToUrl(url);

            MainPage mainPage = new MainPage(_driver);
            mainPage.CloseTheCookies();
            mainPage.GoToContNouPage();

            //creez un obiect de tip ContNouPage pt a testa pagina de CONT NOU 
            ContNouPage contNou = new ContNouPage(_driver);


         
            //trimit datele de test in campurile necesare pt creare CONT NOU
            contNou.ContNou(expectedNume, expectedPrenume, expectedEmail, expectedParola, newsletter, terms);
              


 //ASSERT-uri pt input invalid
                //cazul cand exista input VALID in campurile Nume, Prenume, Email, Parola DAR nu e bifat checkboxul pt "Terms and conditions"=> "Trebuie sa fiti de acord cu termenii si conditiile."
                if (terms == false & expected4FieldsErrMessage == "")
                {
                    IAlert alert2 = _driver.SwitchTo().Alert();
                    Assert.AreEqual(expectedTermsAlertErr, alert2.Text);
                    alert2.Accept();
                }



                //cazul cand exista input invalid si in campurile Nume, Prenume, Email, Parola + nu e bifat "Terms and conditions" => apar ambele mesaje de eroare deodata
                if (terms == false & expected4FieldsErrMessage != "")
                {
                    IAlert alert2 = _driver.SwitchTo().Alert();
                    Assert.AreEqual(expectedTermsAlertErr, alert2.Text);
                    alert2.Accept();

                    Assert.AreEqual(expected4FieldsErrMessage, contNou.ActualContNouErrMessage());
                }





                //cazul cand "Terms and conditions" e bifat, dar exista input invalid in campurile Nume si/sau Prenume si/sau Email si/sau Parola
                //=> apare alerta: Toate campurile sunt obligatorii. Va rugam sa completati campurile ramase necompletate
                if (terms == true & expected4FieldsErrMessage != "")
                {

                    Assert.AreEqual(expected4FieldsErrMessage, contNou.ActualContNouErrMessage());
                }





                //cazul cand userul completeaza un email pt care deja exista cont
                //=> apare alerta: Exista deja un cont cu aceasta adresa de email
                if (terms == true & expected4FieldsErrMessage == "" & expectedSameEmailErrMessage != "")
                {
                    Assert.AreEqual(expectedSameEmailErrMessage, contNou.ActualSameEmailErrMessage());
                }


        }

    }
}
