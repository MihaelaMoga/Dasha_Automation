using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.PageModels.POM
{
  public class ContNouPage : BasePage
    {


    //labels din pagina CONT NOU:
        const string numeLabelSelector = "#newAccount > div:nth-child(2) > label"; //css
        const string prenumeLabelSelector = "#newAccount > div:nth-child(3) > label"; //css
        const string emailLabelSelector = "#newAccount > div:nth-child(5) > label"; //css
        const string parolaLabelSelector = "#newAccount > div.form-group.form-element.password > label";//css

    
    //css pt input in pagina CONT NOU
        const string numeInputSelector = "newAccount-customer_lastname"; //id
        const string prenumeInputSelector = "newAccount-customer_firstname"; //id
        const string emailInputSelector = "newAccount-customer_email";//id
        const string parolaInputSelector = "newAccount-customer_password";
        const string trimiteButtonSelector = "btn-default";//class 
        const string newsletterSelector = "signup-newsletter-agreed"; //id
        const string termSelector = "signup-terms-agreed"; //id



    //selector cand NU reusesc sa creez cont nou
        const string alertErrorMessageSelector = "dialogAlertContent"; //id
    //selector cand folosesc un email pt care deja exista un cont creat
        const string sameEmailErrMessageSelector = "#bodyBody > div.container.customerfrontend-accountnew > div > div:nth-child(1) > div:nth-child(3) > span"; //css
    //selector cand reusesc sa creez cont nou
        const string afterLoginTextSelector = "#user_options > ul > li.first.on > a";




    //definesc constructor cu parametru driver => driverul ne ajuta sa gasim elementele in pagina
        //constructorul pt aceasta clasa este mostenit din clasa parinte
        public ContNouPage(IWebDriver driver) : base(driver)
        {
        }




    //metoda pe baza caruia voi verifica daca labels sunt conforme cu specificatiile
        public bool ContNouLabels(string numeText, string prenumeText, string emailText, string parolaText)
        {
            var numeLabelEl = driver.FindElement(By.CssSelector(numeLabelSelector));
            var prenumeLabelEl = driver.FindElement(By.CssSelector(prenumeLabelSelector));
            var emailLabelEl = driver.FindElement(By.CssSelector(emailLabelSelector));
            var parolaLabelEl = driver.FindElement(By.CssSelector(parolaLabelSelector));


            if (numeLabelEl.Text == numeText & prenumeLabelEl.Text == prenumeText & emailLabelEl.Text == emailText & parolaLabelEl.Text == parolaText)
            {
                return true;
            }
            else
            {
                return false;
            }

        }





    //definesc actiunile din pagina web cu ajutorul functiei ContNou care are ca parametrii de mai jos
        public void ContNou(string nume, string prenume, string email, string parola, bool newsletter, bool terms)
        {

            var numeInputElement = driver.FindElement(By.Id(numeInputSelector));
            numeInputElement.Clear();
            numeInputElement.SendKeys(nume);


            var prenumeInputElement = driver.FindElement(By.Id(prenumeInputSelector));
            prenumeInputElement.Clear();
            prenumeInputElement.SendKeys(prenume);

            var emailInputElement = driver.FindElement(By.Id(emailInputSelector));
            emailInputElement.Clear();
            emailInputElement.SendKeys(email);


            var parolaInputElement = driver.FindElement(By.Id(parolaInputSelector));
            parolaInputElement.Clear();
            parolaInputElement.SendKeys(parola);


            //checkbox pt Newsletter NU e mandatory
            if (newsletter == true)
            {
              
                var newsletterElement = driver.FindElement(By.Id(newsletterSelector));
                newsletterElement.Click();
            }


            //checkbox pt "Terms and conditions"
            if (terms == true)
            {
  
                var termsCheckbox = driver.FindElement(By.Id(termSelector));
                termsCheckbox.Click();

            
                driver.FindElement(By.ClassName(trimiteButtonSelector)).Submit();

            }

          if(terms == false)
            {
            //daca nu bifez checkbox-ul de "Terms and conditions", dau submit pe butonul TRIMITE
                
                var trimiteButtonElement = Utilities.Utils.WaitForExplicitElement(driver, 1000, By.ClassName(trimiteButtonSelector));
                trimiteButtonElement.Submit();
            }
        }




        //metoda prin care verific ca am creat cont nou cu succes
        public string TextAfterContNouValid()
        {
            var afterLoginTextElement = driver.FindElement(By.CssSelector(afterLoginTextSelector));
            return afterLoginTextElement.Text;
        }



    //metoda care returneaza mesajul de eroare daca nu bifez checkboxul mandatory pt termeni: 
        public void ActualTermsAlertErr()
        {
          
        //daca nu bifez termenii si conditiile, apare un popup => browserul tb sa mute focusul pe popup
         IAlert alert2 = driver.SwitchTo().Alert();
         Console.WriteLine(alert2);
        
         alert2.Accept();
        }


        public string ActualTermsErrMessage()
        {
            //daca nu bifez termenii si conditiile, apare un popup => browserul tb sa mute focusul pe popup
            IAlert alert2 = driver.SwitchTo().Alert();
            return alert2.Text;

            
        }

        //metoda care returneaza mesajul de eroare daca nu completez campurile mandatory: Nume, Prenume, Email si Parola
        public string ActualContNouErrMessage()
        {
            return driver.FindElement(By.Id(alertErrorMessageSelector)).Text;
        }




        //metoda care returneaza mesajul de eroare daca vreau sa creez un cont nou cu o adresa de email folosita la crearea unui cont existent

        public string ActualSameEmailErrMessage()
        {
            var sameEmailErrMessage = driver.FindElement(By.CssSelector(sameEmailErrMessageSelector));
            return sameEmailErrMessage.Text;
        }


    }
}
