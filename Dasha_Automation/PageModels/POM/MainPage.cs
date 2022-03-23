using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.PageModels.POM
{
    class MainPage : BasePage
    {

    //definim selectori specifici paginii MainPage
        

        //css selectori ca sa ajung la "CONT NOU"                                                                                                                                                                                                            //selector pt iconita "Contul meu"
        const string contulMeuIconSelector = "dasha-user-outline"; //class
        //selector pt optiunea Contul meu/INTRA IN CONT
        const string contNouSelector = "btn-grey"; //class


        //css ca sa verific ca sunt pe pagina in care apare sintagma "Client nou"
        const string contNouPageTextSelector = "#bodyBody > div.container.customerfrontend-accountnew > div > div.row > div.col-xs-12.account-new-left > h3 > span"; //CSS



        //constructorul mosteneste constructorul din BasePage => avem nevoie de constructor pt a crea ulterior obiecte de tip MainPage in clasa MainPageTest.cs
        public MainPage(IWebDriver driver) : base(driver)
        {
        }



    //metoda prin care sa returnez titlul paginii web
        public string GetDocumentTitle()
        {
          return Utilities.Utils.ExecuteJsScript(driver, "return document.title");
        }


   

     //metoda pt a merge din MainPage catre optiunea ContulMeu/ContNou  
        public void GoToContNouPage()
        {
            //dupa ce am intrat pe Home (cum: in test voi apela url-ul de Home), click pe optiunea "Contul meu" din site + Inspect + Copy/Copy CssSelector
            var contulMeuIconEl = driver.FindElement(By.ClassName(contulMeuIconSelector));
            //dau click pe "Contul meu"
            contulMeuIconEl.Click();

            //dau click pe "CONT NOU"
            var contNouEl = driver.FindElement(By.ClassName(contNouSelector));
            contNouEl.Click();

        }




    //metoda pt a verifica ca am ajuns pe pagina ContNou
        public string CheckContNouPage()
        {
            var contNouPageTextElement = driver.FindElement(By.CssSelector(contNouPageTextSelector));
            //returnez textul care apare ca titlul in pagina web (si anume in pagina web apare "Client nou")
            return contNouPageTextElement.Text;   //returneaza "Client nou"
        }



     //metoda pt afisarea cookie-urilor
     public string GetACookieValue()
        {
            //cookies stocheaza lista cu toate cookie-urile site-ului
            var cookies = driver.Manage().Cookies;
          // Utilities.Utils.PrintCookies(cookies);
            //pt assert voi return valoarea cookie-ului care imi arata ca nu sunt un user logat
            var ckForAssert = cookies.GetCookieNamed("rgisanonymous");
            return ckForAssert.Value;
        }


    }
}
