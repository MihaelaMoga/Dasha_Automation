﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Dasha_Automation;
using Dasha_Automation.PageModels.POM;
using Dasha_Automation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Dasha_Automation.Tests
{
    public class BaseTest
    {
        //dupa ce scriu linia de mai jos, din beculetul galben voi selecta "using System.Threading"
        ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        //pt test report, creez _driver 
        public IWebDriver _driver;
        //dupa ce scriu linia de mai jos, din beculetul galben voi selecta/importa "using AventStack.ExtentReports"
        public static ExtentReports _extent;
        public ExtentTest _test;
        public string testName;


        [OneTimeSetUp]
//metoda ExtentStart() va fi folosita pt partea de Test Report
        protected void ExtentStart()
        {
            //path = locatia de unde se iau testele care se ruleaza 
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase; 
            //actualPath = locatia unde se afla folderul bin al proiectului - de ce? aici vom salva test reportul
            var actualPath = path.Substring(0, path.LastIndexOf("bin")); 
            //din actualPath iau calea proiectului
            //uri = uniform resource identifier
            var projectPath = new Uri(actualPath).LocalPath;

            //creez un director (folder cu denumirea Reports) in projectPath (unde e bin-ul proiectului) 
            //pe linia de cod de mai jos, cu beculetul galben dau click pe "using System.IO"
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            //data generarii raportului
            DateTime time = DateTime.Now;
            //in folderul Reports: salvez 1 test report (sub denumirea report_h_mm_ss.html) pt fiecare clasa din Test 
            var reportPath = projectPath + "Reports\\report_" + time.ToString("h_mm_ss") + ".html";
            
            //in reportPath (adica in fisierul report_h_mm_ss.html) adaugam continutul test reportului 
            //pt linia de cod de mai jos, click pe beculet galben si selectez "using AventStack.ExtentReports.Reporter"
            var htmlReporter = new ExtentV3HtmlReporter(reportPath);
            //cream un obiect _extent unde salvam informatiile despre test 
            _extent = new ExtentReports();
            //atasam htmlReporter la obiectul _extent
            _extent.AttachReporter(htmlReporter);
            //URMATOARELE LINII SUNT INFO CARE VREAU SA APARA IN test report
            // daca pun in linia de mai jos Environment.MachineName: in test report va aparea pe ce masina am rulat testul
            _extent.AddSystemInfo("Host Name", Environment.MachineName);
            //intest report vreau sa apara pe ce mediu am rulat testele (pe PROD, UAT, etc)
            //in loc de "Test ENV" pot trece numele environmentului pe care rulez testele 
            _extent.AddSystemInfo("Environment", "Test ENV");
            //in Test Report vrem sa apare numele celui care a rulat testele
            _extent.AddSystemInfo("Username", "Mihaela Moga");
            //pe disk in projectPath, in test report adaugam fisierul report-config.xml"
            htmlReporter.LoadConfig(projectPath + "report-config.xml");
        }




        //before each test
        [SetUp]
        public void Setup()
        {
            //din clasa Browser apelez metoda GetDriver
            //metoda GetDriver nu are parametru => se va utiliza browserul din fisierul de tip text cu numele config.properties

            driver.Value = Browser.GetDriver();
           
           
            //valoarea de mai sus a browserului o vars in _driver
            _driver = driver.Value;
        }




//metoda pt CITIRE date de test dintr-un Data Table cu ajutorul metodei GetDataTableFromCsv din Utils.cs
        //am pus metoda asta in BaseTest pt ca o voi folosi urmatoarele clase copil: ModificaParolaTests, LogoutTests, FilterTests, ItemTests
        public static IEnumerable<TestCaseData> GetCredentialsDataCsv3()
        {
            //variabila csvData returneaza un DataTable
            var csvData = Utils.GetDataTableFromCsv("TestData\\validCredentials.csv");
            for (int i = 0; i < csvData.Rows.Count; i++)
            {
                // (fata de metoda GetCredentialsDataCsv(unde tb sa indicam noi cate coloane sunt), in metoda curenta NU tb sa indicam cate coloane avem)
                yield return new TestCaseData(csvData.Rows[i].ItemArray);
            }
        }






 //metoda pt logarea in cont cu date valide
        public void IntraInCont(string expectedEmail, string expectedParolaLogare, string expectedInvalidLoginErr)
        {
            MainPage mainPage = new MainPage(_driver);
            mainPage.CloseTheCookies();

            //dau click pe iconita "Contul meu"/"INTRA IN CONT"
            IntraInContPage intraInCont = new IntraInContPage(_driver);
            intraInCont.ClickOnContulMeu();
            intraInCont.ClickOnIntraInCont();

            //introduc email si parola pt a ma loga
            intraInCont.Login(expectedEmail, expectedParolaLogare, expectedInvalidLoginErr);
            intraInCont.ClickOnContulMeu();
        }




//metoda pt a alege o categorie de produse si apoi pt a intra pe pagina unui produs
        public void GoToItemPageUserIsLogged(string expectedItemCategory, string expectedCodProdus)
        {

            //merg pe pagina Cosmetice
            FilterFunctionality filter = new FilterFunctionality(_driver);

            if (expectedItemCategory == "Cosmetice")
            {
                filter.ClickOnCosmetice();
                //verific ca am ajuns pe pagina COSMETICE
                Assert.AreEqual(expectedItemCategory, filter.CheckMainMenuCategories());
            }

            if (expectedItemCategory == "Genti din piele naturala")
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

            if (expectedCodProdus == "248758")
            {
                Assert.AreEqual(expectedCodProdus, selectedItem.GetCodeOfItem2());
                selectedItem.GoToItem2Page();
            }

            if (expectedCodProdus == "535882")
            {
                Assert.AreEqual(expectedCodProdus, selectedItem.GetCodeOfItem3());
                selectedItem.GoToItem3Page();
            }

        }


     


//metoda pt adaugare produse in cos

        public void AddToCartUserIsLogged(string expectedEmail, string expectedPass, string expectedErrMessage, string expectedItemCategory, string expectedCodProdusOnFilter, string expectedQuantity, string expectedUnitPrice, string expectedItemCodeOnItemPage, string expectedCartTotal, string expectedItemCodeOnContinutulCosului, string expectedItem2Category, string expectedCodProdus2OnFilter, string expectedQuantityItem2)
        {

            IntraInCont(expectedEmail, expectedPass, expectedErrMessage);
            GoToItemPageUserIsLogged(expectedItemCategory, expectedCodProdusOnFilter);


            //adaug 1 buc in cos
            AddToCartPage itemToCart = new AddToCartPage(_driver);

 
            itemToCart.ClickOnAdauga();

        //verific ca in CONTINUTUL COSULUI s-a adaugat produsul cu codul corect 
            Assert.AreEqual(expectedItemCodeOnContinutulCosului, itemToCart.CheckCodeItemOnContinutulCosului());

            //verific ca se deschide fereastra CONTINUTUL COSULUI (dupa ce adaug produsul in cos)
            //   Assert.AreEqual("«  VEZI SI ALTE PRODUSE", itemToCart.CheckContinutulCosuluiFinal());

            if (expectedQuantity != "1" || expectedQuantityItem2 != "")
            {
                //din CONTINUTUL COSULUI ma intorc in pagina produsului    
                itemToCart.ClickOnVeziSiAlteProduse();


                //verific ca m-am intors pe pagina aceluiasi produs
                ItemPage selectedItem = new ItemPage(_driver);

                //verific ca sunt pe pagina aceluiasi produs
                Assert.AreEqual(expectedItemCodeOnItemPage, selectedItem.CheckCodeOfItem());
             //   Console.WriteLine(selectedItem.CheckCodeOfItem());


                //daca datele de test precizeaza adaugare 2 buc in cos din acelasi produs
                if (expectedCodProdusOnFilter == "535882" & expectedQuantity == "2" & expectedQuantityItem2 == "")
                {
                    //adaug inca o bucata din acelasi produs
                    AddToCartPage itemToCart2ndPieces = new AddToCartPage(_driver);
                    itemToCart2ndPieces.ClickOnAdauga();

                    //assert pt numarul de produse din cos
                    Assert.AreEqual(expectedCartTotal, itemToCart2ndPieces.CheckCartTotalOnContinutulCosului());

//!!! DE FACUT ASSERT si pt cantitate si pret unitar
                }


                if (expectedQuantity == "1" & expectedQuantityItem2 == "1" & expectedCodProdus2OnFilter == "248758")
                {
                    GoToItemPageUserIsLogged(expectedItem2Category, expectedCodProdus2OnFilter);

                 //adaug 1 bucata din al 2-lea produs 
                    AddToCartPage item2ToCart = new AddToCartPage(_driver);
                    item2ToCart.ClickOnAdauga();


            //cele 2 produse diferite le-am pus intr-o lista
                   List<string> itemToList = new List<string>();
                   itemToList.Add(itemToCart.CheckCodeItemOnContinutulCosului());
                   itemToList.Add(item2ToCart.CheckCodeItemOnContinutulCosului());

                //assert pt a verifica ca avem 2 produse diferite in CONTINUTUL COSULUI (pe baza listei)
                    Assert.IsTrue(itemToList.Contains(item2ToCart.CheckCodeItemOnContinutulCosului()));
                    Assert.IsTrue(itemToList.Contains(itemToCart.CheckCodeItemOnContinutulCosului()));


 //!!! DE FACUT ASSERT si pt cantitate si pret unitar pt fiecare produs in parte 

                }

            }
               

                //dupa ce adaug a 2-a bucata in cos: verific ca se deschide fereastra CONTINUTUL COSULUI
               Assert.AreEqual("«  VEZI SI ALTE PRODUSE", itemToCart.CheckContinutulCosuluiFinal());

                
            }

        






    //dupa fiecare test
            [TearDown]
               public void Teardown()
                {
                    //currentStatus poate fi: PASS/FAIL/Incloclusive/Skipped
                    var currentStatus = TestContext.CurrentContext.Result.Outcome.Status;
                   //can be used to create a method with custom message
                   //bool passed = currentStatus == NUnit.Framework.Interfaces.TestStatus.Passed;  
                    var currentStackTrace = TestContext.CurrentContext.Result.StackTrace;
                    //daca currentStackTrace este null atunci stackTrace="", altfel stackTrace=currentStackTrace
                    var stackTrace = string.IsNullOrEmpty(currentStackTrace) ? "" : currentStackTrace;
                    Status logstatus = Status.Pass;
                    String filename, screenshotPath;
                    DateTime time = DateTime.Now;
                    //pt fiecare test in parte salvam un screenshot care va aparea in test report 
                    filename = "SShot_" + time.ToString("HH_mm_ss") + testName + ".png";
                   //in functie de currentStatus (Failed/Pass/Inconclusive, etc)
                    switch (currentStatus)
                    {
                        //daca testul e FAILED
                        case NUnit.Framework.Interfaces.TestStatus.Failed:
                            {
                                logstatus = Status.Fail;
                                //salvam screenshotul in variabila screenshotEntity
                                var screenshotEntity = Utils.CaptureScreenShot(_driver, filename);
                                //setam testul ca fiind fail
                                _test.Log(Status.Fail, "Fail");
                                //si adaugam print screen pt testul failed
                                _test.Fail("Test failed: ", screenshotEntity);
                                //alta metoda de a insera in test report un screenshot deja existent intr-un alt folder 
                                //_test.Log(Status.Fail, _test.AddScreenCaptureFromPath("Screenshots\\" + filename).ToString());
                                break;
                            }
                        //daca testul e PASSED
                        case NUnit.Framework.Interfaces.TestStatus.Passed:
                            {
                                logstatus = Status.Pass;
                                _test.Log(Status.Pass, "Pass");
                                _test.Pass("Test passed: ", Utils.CaptureScreenShot(_driver, filename));
                                break;
                            }
                        case NUnit.Framework.Interfaces.TestStatus.Inconclusive:
                            {
                                logstatus = Status.Warning;
                                _test.Log(Status.Warning, "Test is inconclusive");
                                break;
                            }
                        case NUnit.Framework.Interfaces.TestStatus.Skipped:
                            {
                                logstatus = Status.Skip;
                                _test.Log(Status.Skip, "Test is skipped");
                                break;
                            }
                        default:
                            {
                                logstatus = Status.Error;
                                _test.Log(Status.Error, "The test had errors while running");
                                break;
                            }

                    }
                    //la finalul fiecarui test va fi un rezumat
                    //"\n"+ stackTrace => pt status= Error: pe o noua linie va aparea stackTrace; daca nu exista Error atunci nu ia in calcul "\n"+ stackTrace 
                    _test.Log(logstatus, "Test " + testName + " was " + logstatus + "\n" + stackTrace);
                    _driver.Quit();
                }
       





    //dupa toate testele
        [OneTimeTearDown]
        public void AllTearDown()
        {
            //save the test report on disk
            _extent.Flush();
        }


    }
}
