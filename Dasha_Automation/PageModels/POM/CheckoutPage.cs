using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.PageModels.POM
{
    public class CheckoutPage : BasePage
    {

        const string veziDetaliiCosSelector = "btn-pink"; //class
        const string produseComandateSelector = "#cartDetails > thead > tr.hidden-xs > th.col-xs-6.col-sm-5";//css

        //constructorul mosteneste constructorul din clasa BasePage (clasa parinte)
        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }

    //metoda prin care dau click pe VEZI DETALII COS ca sa ajung in CheckoutPage
        public void ClickOnVeziDetaliiCos()
        {
            var veziDetaliiCosElement = driver.FindElement(By.ClassName(veziDetaliiCosSelector));
            veziDetaliiCosElement.Click();
        }



    //metoda prin care verific ca am ajuns in Checkout Page (unde scrie "Produse comandate")
        public string CheckCheckoutDetails()
        {
            var produseComandateElement = driver.FindElement(By.CssSelector(produseComandateSelector));
            return produseComandateElement.Text;
        }


    }
}
