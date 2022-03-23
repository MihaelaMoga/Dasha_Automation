using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.PageModels.POM
{
    class AddToCartPage : BasePage
    {


     //   const string adaugaButtonSelector = "//*[@id='bodyBody']/div[4]/div/div[4]/div[2]/div[7]/div[2]/button"; //xpath relativ
        const string adaugaInCosButtonSelector = "//*[@id='bodyBody']/div[4]/div/div[4]/div[2]"; //parte din full xpath    
        const string veziSiAlteProduseButton = "#dHF > div.header-cart > div > div > a.btn.btn-white";//css



        //constructorul mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina
        public AddToCartPage(IWebDriver driver) : base(driver)
        {
        }




        //metoda prin care adaug produsul in cos
        public void ClickOnAdauga()
        {
            var adaugaButtonElement = Utilities.Utils.WaitForExplicitElement(driver, 5, By.XPath(adaugaInCosButtonSelector));
            adaugaButtonElement.Click();
        }


 


        public string CheckContinutulCosuluiFinal()
        {
            driver.SwitchTo().ActiveElement();
            var veziSiAlteProduseButtonEl = Utilities.Utils.WaitForExplicitElement(driver, 5, By.CssSelector(veziSiAlteProduseButton));
            return veziSiAlteProduseButtonEl.Text;
        }


     

    }
}
