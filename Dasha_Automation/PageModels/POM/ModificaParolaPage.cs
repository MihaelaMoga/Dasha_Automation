using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.PageModels.POM
{
    class ModificaParolaPage : BasePage
    {



//definesc CSS selectorii specifici pt pagina Modifica parola

    //selectori pt labels
        const string vecheaParolaLabel = "#changePassword > div:nth-child(3) > label"; //css
        const string nouaParolaLabel = "#changePassword > div:nth-child(4) > label"; //css
        const string retypeNewPassLabel = "#changePassword > div:nth-child(5) > label"; //css
        const string campuriObligatoriiLabel= "#changePassword > div:nth-child(6) > div > div"; //CSS
        const string submitButton = "btn-default";  //class

        const string linkModificaParolaPage = "#dHF > header > div.header-buttons > ul > li.customer-actions.head-customer > div > a:nth-child(4)"; //css 
        

    //selectori pt input
        const string vecheaParolaInput = "customer_old_password"; //id
        const string nouaParolaInput = "customer_password"; //id
        const string retypePassInput = "customer_confirm_password"; //id
        const string afterValidModificaParola = "#bodyBody > div.container.customerfrontend-myaccount > div > div:nth-child(1) > div.alert-bar.status";//css


    //selectori pt mesaje de eroare 
        const string vecheaParolaErrMessage = "#changePassword > div:nth-child(3) > div > span:nth-child(3)"; //css
        const string nouaParolaErrMessage = "#changePassword > div:nth-child(4) > div > span:nth-child(3)";  //css
        const string retypePasswordMessage = "#changePassword > div:nth-child(5) > div > span:nth-child(3)"; //css
        

        const string parolaVecheInvalidaErrMessage = "#bodyBody > div.container.customerfrontend-changepassword > div > div:nth-child(1) > div:nth-child(3) > span";//css


       



        //constructorul din aceasta prezenta mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina INTRA IN CONT
        public ModificaParolaPage(IWebDriver driver) : base(driver)
        {
        }




//metoda pt a da click pe optiunea "Modifica parola"
        public void ClickOnModificaParola()
        {
            var modificaParolaElement = Utilities.Utils.WaitForExplicitElement(driver,3,(By.CssSelector(linkModificaParolaPage)));
            modificaParolaElement.Click();
        }






//metoda pe baza caruia voi verifica daca labels sunt conforme cu specificatiile
        public bool ModificaParolaLabels(string expectedVecheaParola, string expectedNouaParola, string expectedRetypeNewPass, string expectedCampuriObligatorii)
        {
            var vecheaParolaLabelElement = driver.FindElement(By.CssSelector(vecheaParolaLabel));
            var nouaParolaLabelElement = driver.FindElement(By.CssSelector(nouaParolaLabel));
            var retypeNewPassLabelElement = driver.FindElement(By.CssSelector(retypeNewPassLabel));
            var campuriObligatoriiLabelElement = driver.FindElement(By.CssSelector(campuriObligatoriiLabel));
          

            if (vecheaParolaLabelElement.Text == expectedVecheaParola & nouaParolaLabelElement.Text == expectedNouaParola & retypeNewPassLabelElement.Text == expectedRetypeNewPass & campuriObligatoriiLabelElement.Text == expectedCampuriObligatorii)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }






//metoda prin care voi verifica ulterior ca modificarea parolei se poate face dupa completarea corecta a campurilor
        public void ModificaParola(string expectedVecheaParola, string expectedNouaParola, string expectedRetypePassword)
        {


            //in pagina Modifica parola: gasesc selectorul pt input de vechea parola, noua parola, retype password + butonul Submit (selectare element din pagina + click dreapta + Inspect) 
            var vecheaParolaInputElement = driver.FindElement(By.Id(vecheaParolaInput));
            //la campurile cu input de preferat inainte sa completam inputurile sa dam stergem valorile care pot exista deja in acea casuta de input
            vecheaParolaInputElement.Clear();
            //in campul vechea parola completez valoarea expectedVecheaParola din parametrii prezentei functii 
            vecheaParolaInputElement.SendKeys(expectedVecheaParola);


            var nouaParolaInputElement = driver.FindElement(By.Id(nouaParolaInput));
            nouaParolaInputElement.Clear();
            //parametru expectedNouaParola din metoda prezenta
            nouaParolaInputElement.SendKeys(expectedNouaParola);


            var retypePasswordInputElement = driver.FindElement(By.Id(retypePassInput));
            retypePasswordInputElement.Clear();
            //parametru expectedNouaParola din metoda prezenta
            retypePasswordInputElement.SendKeys(expectedRetypePassword);


            var submitButtonElement = driver.FindElement(By.ClassName(submitButton));
            submitButtonElement.Submit();



            //la orice apelare a metodei ModificaParola: vreau sa afiseze actual error message, atunci cand e cazul
            if (expectedVecheaParola == "" || expectedNouaParola == "" || expectedRetypePassword == "" || expectedNouaParola != expectedRetypePassword)
            {
               

                //daca nu completez corect cele 3 campuri mandatory, apare un popup => browserul tb sa mute focusul pe popup
                IAlert alert2 = driver.SwitchTo().Alert();

                //afisez mesajul alertei in test
                Console.WriteLine(alert2.Text);

                //dau accept la popup
                alert2.Accept();

             
            }


        }







//metoda prin care verific ca am schimbat parola cu SUCCES
        public string TextAfterValidChangeOfPass()
        {
            var afterValidModificaParolatElement = driver.FindElement(By.CssSelector(afterValidModificaParola));
            return afterValidModificaParolatElement.Text;
        }





//metoda prin care verific mesajul de eroare in caz de eroare la vechea parola
        public string ActualErrMessageInvalidPassChange()
        {
            var errorMessageInvalidVecheaParolaElement = driver.FindElement(By.CssSelector(vecheaParolaErrMessage));
            return errorMessageInvalidVecheaParolaElement.Text;
        }
     
        


//metoda prin care verific mesajul de eroare in caz de eroare la noua parola
        public string ActualErrMessageInvalidNouaParolaChange()
        {   
            var errorMessageInvalidNouaParolaElement = driver.FindElement(By.CssSelector(nouaParolaErrMessage));
            return errorMessageInvalidNouaParolaElement.Text;
        }


//metoda prin care verific mesajul de eroare in cazul in care noua parola e diferita de retypePassword
        public string ActualErrMessageInvalidRetypePass()
        {
            var errMessageInvalidRetypePassElement = driver.FindElement(By.CssSelector(retypePasswordMessage));
            return errMessageInvalidRetypePassElement.Text;
        }
        

//metoda prin care verific mesajul de eroare 
          public string ActualErrMessageInvalidVecheaParola()
        {
            var errMessageInvalidVecheaParola = driver.FindElement(By.CssSelector(parolaVecheInvalidaErrMessage));
            return errMessageInvalidVecheaParola.Text;
        }




    }
}
