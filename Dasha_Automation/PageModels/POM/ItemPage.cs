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
       
        const string samponSelectat = "//*[@id='bodyBody']/div[4]/div/div[4]/div[1]"; //xpath
        const string codSamponSelectat = "//*[contains(@data-product-id,'252833')]";//XPATH


        //selector pt a ajunge pe pagina produsului https://www.dasha.ro/rochie-eleganta-neagra-din-dantela-430879.html
        const string rochieSelector = "#bodyBody > div.container.frontend-productslist > div > div.products > div:nth-child(2) > a > h2"; //css
       
        const string codItem = "product-code"; //class
        const string ratingSelector = "rating-average";//class

        //selector pt moneda
        const string priceCurrency = "#bodyBody > div.container.frontend-productview > div > div.product-view.visible-sliders-a > div.col-right-small > div.product-main-price > div > span.currency"; //css
        const string priceSelector = "product-price";//class
        

        const string telefonSelector = "google_forwarding_number"; //class
        const string producatorSelector = "#tabs > div:nth-child(2) > p > a";//css
        const string cantitateMlCosmetice = "#productLongDescription > div > p:nth-child(10)";//css

       

      
     


        //constructorul mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina
        public ItemPage(IWebDriver driver) : base(driver)
        {
        }




//metoda care va returna codul produsului
    public string GetCodProdus()
        {
                var codProdusElement = Utilities.Utils.WaitForExplicitElement(driver, 7, By.XPath(codSamponSelectat));
                return codProdusElement.GetAttribute("data-product-id");   
        }



//metoda pt a intra pe pagina produsului: https://www.dasha.ro/Samponanti-pigmentgalben-OysterBlondyeAnti-YellowShampoo250ml.html
        public void ClickOnSelectedSampon()
        {
            var sampon = driver.FindElement(By.XPath(codSamponSelectat));
            Console.WriteLine(sampon.Text);
            sampon.Click();

        }

        //metoda returneaza codul samponului
        public string CheckCodSampon()
        {
            var samponElement = Utilities.Utils.WaitForExplicitElement(driver, 2, By.ClassName(codItem));
            return samponElement.Text;
        }

//metoda care retunreaza ratingul produsului

        public string CheckRating()
        { 
            var ratingElement = Utilities.Utils.WaitForExplicitElement(driver, 1, By.ClassName(ratingSelector));
            return ratingElement.Text;
        }

//metoda care returneaza un string cu moneda in care e exprimat pretul
        public string CheckMonedaPret()
        {
            var monedaElement = Utilities.Utils.WaitForExplicitElement(driver, 8, By.CssSelector(priceCurrency));
            return monedaElement.Text;
        }


//metoda care returneaza pretul produsului
        public string CheckPrice()
        {  
            var priceElement = Utilities.Utils.WaitForExplicitElement(driver, 1, By.ClassName(priceSelector));
            return priceElement.Text;
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



//metoda care returneaza cantitatea produselor cosmetice
        public string CheckCantitateCosmetice()
        {
            var cantitateElement = Utilities.Utils.WaitForExplicitElement(driver, 2, By.CssSelector(cantitateMlCosmetice));
            return cantitateElement.Text;       
        }








        /*

                //metoda pentru a intra pe pagina produsului https://www.dasha.ro/rochie-eleganta-neagra-din-dantela-430879.html
                public void ClickOnRochie()
                {
                    var rochie = driver.FindElement(By.CssSelector(rochieSelector));
                    Console.WriteLine(rochie.Text);
                    rochie.Click();
                }



        //metoda pt a verifica ca am ajuns pe pagina rochiei https://www.dasha.ro/rochie-eleganta-neagra-din-dantela-430879.html
                public string CheckRochiePage()
                {
                    var codRochieElement = driver.FindElement(By.ClassName(codItem));
                    return codRochieElement.Text;
                }

        */



        //la imbracaminte si incaltaminte de facut metode pt a verifica:
        //daca produsul nu e disponibil, cand dau click pe marime apare textul "anunta-ma cand produsul e disponibil" + NU ma lasa sa adaug produsul in cos
        //daca produsul e disponibil, e mandatory sa aleg marimea si apoi sa adaug in cos







    }
}
