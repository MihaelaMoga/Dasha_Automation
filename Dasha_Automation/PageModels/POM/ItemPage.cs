using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.PageModels.POM
{
    class ItemPage : BasePage
    {
       

//selector ca sa selectez produsul din TestData
   //    const string codeOfSelectedItem2 = "//*[contains(@data-product-id,'248758')]";//xpath
   //    const string codeOfSelectedItem3 = "//*[contains(@data-product-id,'535882')]";//xpath

   //!!! in loc de xpath-ul prea specific al celor 2 produse, am sa aplic encapsulation
        string code;
        string itemSelectedFromSearchPagePart1 = "//*[contains(@data-product-id,'";//xpath
        string itemSelectedFromSearchPagePart3 = "')]";//xpath



//selector dupa ce intru pe pagina produsului
        const string codeDisplayedOnItemPage = "product-code"; //class
        const string ratingSelector = "rating-average";//class  
        const string priceSelector = "product-price";//class                    
        const string discountPriceSelector = "product-price-pink"; //class
        const string telefonSelector = "google_forwarding_number"; //class
        const string producatorSelector = "#tabs > div:nth-child(2) > p > a";//css

       






        //constructorul mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina
        public ItemPage(IWebDriver driver) : base(driver)
        {
        }



        //constructor pt encapsulation cu 2 parametrii: code (ulterior cand voi crea obiectul ItemPage, prin metoda GetCode() de la encapsulation: this.code = expectedCodProdus din TestData) si driver
        public ItemPage(IWebDriver driver, string code) : base(driver)
        {
            this.code = code;
        }


        //metoda Getter pt encapsulation; GetCode() va citi expectedCodProdus din TestData
        public string GetCode()
        {
        return this.code;    
        }




        
//metoda pt a intra pe pagina oricarui produs avand in vedere ca xpath este de genul "//*[contains(@data-product-id,'535882')]" 
        public void GoToItemPageGeneral()
        {
            var itemSelectedFromSearchPageSelector = itemSelectedFromSearchPagePart1 + GetCode() + itemSelectedFromSearchPagePart3;//xpath
            var produsGeneralElement = Utilities.Utils.WaitForElementClickable(driver, 12, By.XPath(itemSelectedFromSearchPageSelector));
            produsGeneralElement.Click();
        }






        //metoda returneaza codul produsului
        public string CheckCodeOfItem()
        {
            var samponElement = Utilities.Utils.WaitForExplicitElement(driver, 4, By.ClassName(codeDisplayedOnItemPage));
            return samponElement.Text;
        }



//metoda care returneaza ratingul produsului

        public string CheckRating()
        { 
            var ratingElement = Utilities.Utils.WaitForExplicitElement(driver, 1, By.ClassName(ratingSelector));
            return ratingElement.Text;
        }



//metoda care returneaza pretul produsului
        public string CheckPrice()
        {  
            var priceElement = Utilities.Utils.WaitForExplicitElement(driver, 1, By.ClassName(priceSelector));
            return priceElement.Text;
        }


//metoda pt a returna pretul produsului dupa discount
        public string CheckDiscountPrice()
        {
          var discountPrice  = Utilities.Utils.WaitForExplicitElement(driver, 10, By.ClassName(discountPriceSelector));
            
            return discountPrice.Text;
        }




//metoda care returneaza nr de telefon la care se poate face comanda telefonica
        public string CheckTelephone()
        {
            var telephoneElement = Utilities.Utils.WaitForExplicitElement(driver, 1, By.ClassName(telefonSelector));
            return telephoneElement.Text;
        }



//metoda care returneaza numele producatorului
        public string CheckNumeProducator()
        {
            var producatorElement = Utilities.Utils.WaitForExplicitElement(driver, 1, By.CssSelector(producatorSelector));
            return producatorElement.Text;
            
        }



//metoda care duce userul pe site-ul producatorului si apoi il aduce inapoi pe site-ul Dasha
    public void ClickOnNumeProducator()
        {    
        //merg pe site-ul producatorului
            var producatorElement = Utilities.Utils.WaitForExplicitElement(driver, 6, By.CssSelector(producatorSelector));
            producatorElement.Click();
        //revin la site-ul Dasha.com
            driver.Navigate().Back();
        }






        //la imbracaminte si incaltaminte de facut metode pt a verifica:
        //daca produsul nu e disponibil, cand dau click pe marime apare textul "anunta-ma cand produsul e disponibil" + NU ma lasa sa adaug produsul in cos
        //daca produsul e disponibil, e mandatory sa aleg marimea si apoi sa adaug in cos

    }
}
