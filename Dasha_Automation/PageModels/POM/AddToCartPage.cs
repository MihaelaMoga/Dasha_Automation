using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.PageModels.POM
{
    class AddToCartPage : BasePage
    {


     
         const string adaugaInCosButtonSelector = "//*[@id='bodyBody']/div[4]/div/div[4]/div[2]/div/div[2]/button"; //parte din full xpath


        const string expectedItemCodeOnContinutulCosului = "#dHF > div.header-cart > div > span > div > div.products > div > p";//css
        const string cartTotal = "price-formatted"; //class
        
        const string veziSiAlteProduseButton = "#dHF > div.header-cart > div > div > a.btn.btn-white";//css


        //constructorul mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina
        public AddToCartPage(IWebDriver driver) : base(driver)
        {
        }




    //metoda prin care adaug produsul in cos
        public void ClickOnAdauga()
        {
            var adaugaButtonElement = Utilities.Utils.WaitForElementClickable(driver, 20, By.XPath(adaugaInCosButtonSelector));
            adaugaButtonElement.Click();  
        }





    //metoda care returneaza codul produsului
        public string CheckCodeItemOnContinutulCosului()
        {
            var itemCodeInContinutulCosului = Utilities.Utils.WaitForExplicitElement(driver,15,By.CssSelector(expectedItemCodeOnContinutulCosului));
            return itemCodeInContinutulCosului.Text;
        }



    //metoda care returneaza valoarea finala a comenzii
        public string CheckCartTotalOnContinutulCosului()
        {
            driver.SwitchTo().ActiveElement();
            var cartTotalElement = Utilities.Utils.WaitForExplicitElement(driver, 7, By.ClassName(cartTotal));
            return cartTotalElement.Text;
        }





    //metoda care returneaza textul de pe butonul "VEZI SI ALTE PRODUSE"
        public string CheckContinutulCosuluiFinal()
        {
            driver.SwitchTo().ActiveElement();
            var veziSiAlteProduseButtonEl = Utilities.Utils.WaitForExplicitElement(driver, 5, By.CssSelector(veziSiAlteProduseButton));
            return veziSiAlteProduseButtonEl.Text;
        }



    //metoda cu care care userul da click pe butonul "VEZI SI ALTE PRODUSE"
        public void ClickOnVeziSiAlteProduse()
        {
            var veziSiAlteProduseButtonEl = Utilities.Utils.WaitForExplicitElement(driver, 5, By.CssSelector(veziSiAlteProduseButton));
            veziSiAlteProduseButtonEl.Click();

        }
     

    

    }
}
