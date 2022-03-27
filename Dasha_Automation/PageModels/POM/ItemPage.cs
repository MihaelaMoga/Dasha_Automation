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
       
        
        const string codeOfSelectedItem1 = "//*[contains(@data-product-id,'252833')]";//XPath
        const string codeOfSelectedItem2 = "//*[contains(@data-product-id,'248758')]";//xpath
        const string codeOfSelectedItem3 = "//*[contains(@data-product-id,'535882')]";//xpath
        const string nameOfSelectedItem3 = "//h2[@class='product-name']"; //xpath

       
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




//metoda care va returna codul produsului
    public string GetCodeOfItem1()
        {
                var codProdusElement = Utilities.Utils.WaitForExplicitElement(driver, 12, By.XPath(codeOfSelectedItem1));
                return codProdusElement.GetAttribute("data-product-id");   
        }



        //metoda care va returna codul produsului
        public string GetCodeOfItem2()
        {
            var codProdusElement = Utilities.Utils.WaitForExplicitElement(driver, 12, By.XPath(codeOfSelectedItem2));
            return codProdusElement.GetAttribute("data-product-id");
        }


        public string GetCodeOfItem3()
        {
            var codProdusElement = Utilities.Utils.WaitForExplicitElement(driver, 12, By.XPath(codeOfSelectedItem3));
            return codProdusElement.GetAttribute("data-product-id");
        }


        //metoda pt a intra pe pagina produsului: https://www.dasha.ro/Samponanti-pigmentgalben-OysterBlondyeAnti-YellowShampoo250ml.html
        public void GoToItem1Page()
        {
            var sampon = Utilities.Utils.WaitForElementClickable(driver, 12, By.XPath(codeOfSelectedItem1));
            sampon.Click();
        }


           
         public void GoToItem2Page()
            {
               var produs2 = Utilities.Utils.WaitForElementClickable(driver,12,(By.XPath(codeOfSelectedItem2)));
               produs2.Click();
             }


        //metoda pt produsele cu discount
        public void GoToItem3Page()
        {
            var produs3 = Utilities.Utils.WaitForElementClickable(driver, 12, By.XPath(codeOfSelectedItem3));
            produs3.Click();
        }


        //metoda returneaza codul produsului
        public string CheckCodeOfItem()
        {
            var samponElement = Utilities.Utils.WaitForExplicitElement(driver, 4, By.ClassName(codeDisplayedOnItemPage));
            return samponElement.Text;
        }

//metoda care retunreaza ratingul produsului

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
            var discountPrice  = Utilities.Utils.WaitForExplicitElement(driver, 4, By.ClassName(discountPriceSelector));
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
