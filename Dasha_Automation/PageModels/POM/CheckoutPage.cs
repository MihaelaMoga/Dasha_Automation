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

        //selector pt codul oricarui produs
        const string codeItemOnCheckoutPage = "//tr/td[2]/p[2]"; //fragment de xpath

        const string pasulUrmatorCheckoutSelector = "next-step-checkout"; //id
        const string adresaLivrareLabel = "predefined-shipping-addresses-head"; //class
       


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
        public string SeeCheckoutDetails()
        {
            var produseComandateElement = driver.FindElement(By.CssSelector(produseComandateSelector));
            return produseComandateElement.Text;
        }



    //metoda care va returna codul produsului aflat in cos
    public string CheckItemCode()
        {
            var itemCodeOnCheckoutPage = driver.FindElement(By.XPath(codeItemOnCheckoutPage));
            return itemCodeOnCheckoutPage.Text;
        }



    //metoda pt a da click pe butonul PASUL URMATOR din pagian de checkout 
        public void ClickOnPasulUrmatorCheckout()
        {
            var pasulUrmatorCheckout = Utilities.Utils.WaitForElementClickable(driver,7,By.Id(pasulUrmatorCheckoutSelector));
            pasulUrmatorCheckout.Click();
        }



    //metoda pt a returna textul "Date livrare" din pagina de Checkout
       public string CheckDateLivrareLabel()
         {
            var dateLivrareLabelElement = Utilities.Utils.WaitForExplicitElement(driver,7,By.ClassName(adresaLivrareLabel));
            return dateLivrareLabelElement.Text;
         }



    }
}
