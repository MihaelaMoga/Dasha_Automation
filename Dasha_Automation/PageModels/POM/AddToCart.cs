using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.PageModels.POM
{
    class AddToCart : BasePage
    {


        const string adaugaButtonSelector = "//*[@id='bodyBody']/div[4]/div/div[4]/div[2]/div[7]/div[2]/button"; //xpath                                                     
        const string continutulCosuluiSelector = "#dHF > div.header-cart > div > span > div > div.products > div > p";//css
        const string pretTotalCosSelector = "#dHF > div.header-cart > div > span > div > div.totals.total.row > div.xs-col-50.sm-col-50.md-col-50.lg-col-50.value > span > span"; //css


        //constructorul mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina
        public AddToCart(IWebDriver driver) : base(driver)
        {
        }


        //metoda prin care adaug produsul in cos
        public void ClickOnAdauga()
        {
            var adaugaButtonElement = Utilities.Utils.WaitForExplicitElement(driver, 5, By.XPath(adaugaButtonSelector));
            adaugaButtonElement.Click();
        }




        //metoda prin care verific ca am adaugat produse in cos
        public string CheckContinutulCosului()
        {
            driver.SwitchTo().ActiveElement();
            var continutulCosuluiElement = Utilities.Utils.WaitForExplicitElement(driver, 5, By.CssSelector(pretTotalCosSelector));
            return continutulCosuluiElement.Text;

        }




        //DE FACUT METODA DE MAI JOS PT ASSERT DUPA CLICK PE BUTONUL ADAUGA (SIGUR TB WAIT)
        //    public string CheckAdauga()
        //     {

        //     }





    }
}
