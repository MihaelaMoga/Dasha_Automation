using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.PageModels.POM
{
    class LogoutPage : BasePage
    {

//definesc CSS selectorii specifici pt butonul Logout
        const string logoutButtonSelector = "#dHF > header > div.header-buttons > ul > li.customer-actions.head-customer > div > a:nth-child(6)"; //css
        const string afterLogoutMessageSelector = "status"; //class



//constructorul din aceasta prezenta mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica butonul Logout
        public LogoutPage(IWebDriver driver) : base(driver)
        {
        }




//metoda pt a da click pe butonul "Log out"
        public void ClickOnLogoutButton()
        {
            var logoutElement = driver.FindElement(By.CssSelector(logoutButtonSelector));
            logoutElement.Click();
        }



                                    

//metoda prin care verific ca m-am delogat cu SUCCES
        public string TextAfterLogout()
        {
            var afterLogoutElement = driver.FindElement(By.ClassName(afterLogoutMessageSelector));
            return afterLogoutElement.Text;
        }




    }
}
