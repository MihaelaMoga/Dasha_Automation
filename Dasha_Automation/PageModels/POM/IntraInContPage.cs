using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.PageModels.POM
{
   public class IntraInContPage : BasePage
    {


//definesc CSS selectorii specifici pt pagina INTRA IN CONT
        const string contulMeuIconSelector = "dasha-user-outline"; //class

        const string intraInContSelector = "btn-pink"; //class
        const string intraInContTextSelector = "#login_form > div.fb-login-wrapper > p"; //css 

        const string emailLabelSelector = "#login_form > div:nth-child(2) > label"; //css
        const string parolaLabelSelector = "#login_form > div:nth-child(3) > label";//css
        const string otherInfoLabelSelector = "#login_form > div.fb-login-wrapper > p";//css
        const string aiUitatParolaLabelSelector = "#login_form > div.form-group.login_utils > div > a:nth-child(1)"; //css
        const string nuAiContLabelSelector = "#login_form > div.form-group.login_utils > div > a:nth-child(2)"; //css


        const string emailInputSelector = "email"; //id
        const string parolaInputSelector = "parola";  //id
        const string submitButtonSelector = "btn-default"; //class

       // const string invalidLoginErrMessageSelector = "#bodyBody > div.container.customerfrontend-login > div > div.xs-hidden.sm-hidden > div:nth-child(3) > span"; //css
        const string invalidLoginErrMessageSelector = "#bodyBody > div.container.customerfrontend-login > div > div:nth-child(1) > div:nth-child(3) > span"; //css

       
        const string afterLoginTextSelector = "#user_options > ul > li.first.on > a"; //css 






        //constructorul din aceasta prezenta mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina INTRA IN CONT
        public IntraInContPage(IWebDriver driver) : base(driver)
        {
        }



        public void ClickOnContulMeu()
        {
            //dau click pe iconita "Contul meu"
            var contulMeuIconElement = driver.FindElement(By.ClassName(contulMeuIconSelector));
            contulMeuIconElement.Click();

        
        }




        //metoda pentru a da click pe optiunea INTRA IN CONT
        public void ClickOnIntraInCont()
        {
            //click dreapta pe optiunea "INTRA IN CONT" din site + Inspect + Copy/Copy CssSelector
            var intraInContElement = driver.FindElement(By.ClassName(intraInContSelector));
            //dau click pe "Login"
            intraInContElement.Click();
        }


    
        public string ActualIntraInContText()
        {
            //dupa ce am intrat pe pagina INTRA IN CONT, selectez cuvintele "SAU INTRA IN CONT FOLOSIND:" din pagina de INTRA IN CONT + Inspect + Copy/Copy CssSelector
            var intraInContText = driver.FindElement(By.CssSelector(intraInContTextSelector));
            return intraInContText.Text;
        }




//metoda pe baza caruia voi verifica daca labels sunt conforme cu specificatiile
        public bool IntraInContLabels(string emailLabel, string parolaLabel, string otherInfoLabel, string aiUitatParolaLabel, string nuAiContLabel)
        {
            var emailLabelElement = driver.FindElement(By.CssSelector(emailLabelSelector));
            var parolaLabelElement = driver.FindElement(By.CssSelector(parolaLabelSelector));
            var otherInfoLabelElement = driver.FindElement(By.CssSelector(otherInfoLabelSelector));
            var aiUitatParolaLabelElement = driver.FindElement(By.CssSelector(aiUitatParolaLabelSelector));
            var nuAiContLabelElement = driver.FindElement(By.CssSelector(nuAiContLabelSelector));
           

            if (emailLabelElement.Text == emailLabel & parolaLabelElement.Text == parolaLabel & otherInfoLabelElement.Text == otherInfoLabel & aiUitatParolaLabelElement.Text == aiUitatParolaLabel & nuAiContLabelElement.Text == nuAiContLabel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }






//metoda pt a verifica ca logarea se poate face doar daca completez ambele campuri: username si password

        public void Login(string expectedEmail, string expectedParola, string expectedInvalidLoginErr)
        {
            //inainte de a ma loga trebuie sa dau CLICK PE ACCEPT COOKIES (metoda e definita in BasePage)
    //  ClickOnAcceptCookies();

            //din pagina INTRA IN CONT: gasesc selectorul pt input de username, password + butonul Submit (selectare element din pagina + click dreapta + Inspect) 
            var emailInputElement = driver.FindElement(By.Id(emailInputSelector));
            //la campurile cu input de preferat inainte sa completam inputurile sa dam stergem valorile care pot exista deja in acea casuta de input
            emailInputElement.Clear();
            ////in campul username completez valoarea user din parametrii prezentei functii 
            emailInputElement.SendKeys(expectedEmail);


            var parolaInputElement = driver.FindElement(By.Id(parolaInputSelector));
            parolaInputElement.Clear();
            //parametru pass din metoda prezenta
            parolaInputElement.SendKeys(expectedParola);


            var submitButtonElement = driver.FindElement(By.ClassName(submitButtonSelector));
            submitButtonElement.Submit();


            //la orice apelare a metodei Login: vreau sa afiseze actual error message pt email, atunci cand e cazul
            if (expectedEmail == "" || expectedParola == "")
            {
                Console.WriteLine("ACTUAL error message displayed for Username field is: {0}", ActualInvalidLoginErrMessage());
            }

        }






    //metoda prin care verific ca m-am logat cu succes
        public string TextAfterValidLogin()
        {
            var afterLoginTextElement = driver.FindElement(By.CssSelector(afterLoginTextSelector));
            return afterLoginTextElement.Text;
        }



     //metoda prin care returnez mesajul de eroare in caz de Login INVALID
        public string ActualInvalidLoginErrMessage()
        {
            var emailErrorElement = driver.FindElement(By.CssSelector(invalidLoginErrMessageSelector));
            return emailErrorElement.Text;
        }



    



}
}
