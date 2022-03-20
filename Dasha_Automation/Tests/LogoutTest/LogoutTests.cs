using Dasha_Automation.PageModels.POM;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.Tests.LogoutTest
{
    class LogoutTests : BaseTest
    {

        //URL-ul paginii principale a site-ului  
        string url = Utilities.FrameworkConstants.GetUrl();


        //Nota: metoda de CITIRE a datelor de teste este GetCredentialsDataCsv3 si se afla in clasa parinte BaseTest.cs
         // [TestCase("testarescoalainfo@gmail.com", "papadie456","")]
       //  [Test]

        [Test, TestCaseSource("GetCredentialsDataCsv3")]
        public void Logout(string expectedEmail, string expectedParola,string expectedInvalidLoginErr)
        {
            //urmatoarele 2 linii sunt necesare pt ca Testul sa apara in Test Report
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            //userul deschide pagina princilala a site-ului
            _driver.Navigate().GoToUrl(url);

        //userul completeaza credentialele valide pt a se loga in contul sau
            IntraInCont(expectedEmail, expectedParola, expectedInvalidLoginErr);
        

         //userul logat da click pe optiunea "Log out"
            LogoutPage logoutButton = new LogoutPage(_driver);
            logoutButton.ClickOnLogoutButton();

        //ASSERTURI:
            //verific daca apare label-urile specifice optiunii INTRA IN CONT si Log out
            IntraInContPage intraInCont = new IntraInContPage(_driver);    
            Assert.IsTrue(intraInCont.IntraInContLabels("E-mail:", "Parola:", "SAU INTRA IN CONT FOLOSIND:", "Ai uitat parola?", "Nu ai cont? Creeaza cont."));


            //verific daca dupa delogare apare textul "V-ați deconectat cu succes."
            Assert.AreEqual("V-ați deconectat cu succes.",logoutButton.TextAfterLogout());
        }




    }
}
