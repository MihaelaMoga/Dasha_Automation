using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dasha_Automation.PageModels.POM
{
    class FilterFunctionality : BasePage
    {

     //selectori specifici pt filtrare categorii din meniul principal: INCALTAMINTE, IMBRACAMINTE, GENTI, COSMETICE, NOUTATI, OUTLET
        const string incaltaminteMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(1) > a"; //css
        const string imbracaminteMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(2) > a"; //css
        const string gentiMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(3) > a"; //css
        const string cosmeticeMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(4) > a"; //css
        const string noutatiMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(5) > a"; //css
        const string outletMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(6) > a"; //css
        const string outletLabelSelector = "#bodyBody > div.container.frontend-productslist > div > div.breadcrumbs > span > span > a > span"; //css
   
        
     //selector specifici tuturor paginilor 
        const string allMainCategoriesLabelSelector = "category-title";//class
        const string allSubmenuLabelSelector = "#bodyBody > div.container.frontend-productslist > div > div.category_primary_description > h1";//css



        //selector pt submeniul INGRIJIREA TENULUI din meniul COSMETICE
        const string ingrijireaTenuluiSelector = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(4) > div > div.subcategories > ul:nth-child(1) > li:nth-child(1) > a"; //css
        const string ingrijireaParuluiSelector = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(4) > div > div.subcategories > ul:nth-child(1) > li:nth-child(2) > a";//css



//constructorul mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina 
        public FilterFunctionality(IWebDriver driver) : base(driver)
        {
        }




        //metoda pentru a ajunge in pagina de INCALTAMINTE
        public void ClickOnIncaltaminte()
        {
            var incaltaminteElement = driver.FindElement(By.CssSelector(incaltaminteMainMenu));
            incaltaminteElement.Click();
        }


    


        //metoda pentru a ajunge in pagina de IMBRACAMINTE
        public void ClickOnImbracaminte()
        {
            var imbracaminteElement = driver.FindElement(By.CssSelector(imbracaminteMainMenu));
            imbracaminteElement.Click();
        }




        //metoda pt a ajunge in pagina de GENTI
        public void ClickOnGenti()
        {
            var gentiElement = driver.FindElement(By.CssSelector(gentiMainMenu));
            gentiElement.Click();
        }





//metoda pt a ajunge in pagina de COSMETICE
        public void ClickOnCosmetice()
        {
            var cosmeticeElement = driver.FindElement(By.CssSelector(cosmeticeMainMenu));
            cosmeticeElement.Click();
        }






//metoda pt a ajunge in pagina COSMETICE/INGRIJIREA TENULUI (INGRIJIREA PARULUI apare DOAR DUPA ce pun mouse-ul pe meniul COSMETICE) 

        public void ClickOnIngrijireaTenului()
        {      
        //interactionez cu site-ul folosind clasa Actions (parametru = driver)
            Actions actions = new Actions(driver);

        //mut mouse-ul si dau click pe meniul principal COSMETICE => se afiseaza submeniul vertical format din: INGRIJIREA TENULUI, INGRIJIREA PARULUI
            var cosmeticeElement = driver.FindElement(By.CssSelector(cosmeticeMainMenu));
            actions.MoveToElement(cosmeticeElement).Build().Perform();

        //citesc si apoi dau click pe submmeniul INGRIJIREA TENULUI
            var ingrijireaTenuluiSubmenu = driver.FindElement(By.CssSelector(ingrijireaTenuluiSelector));
            ingrijireaTenuluiSubmenu.Click();
        }


        public void ClickOnIngrijireaParului()
        {
            //interactionez cu site-ul folosind clasa Actions (parametru = driver)
            Actions actions = new Actions(driver);

            //mut mouse-ul si dau click pe meniul principal COSMETICE => se afiseaza submeniul vertical format din: INGRIJIREA TENULUI, INGRIJIREA PARULUI
            var cosmeticeElement = driver.FindElement(By.CssSelector(cosmeticeMainMenu));
            actions.MoveToElement(cosmeticeElement).Build().Perform();

            //citesc si apoi dau click pe submmeniul INGRIJIREA PARULUI
            var ingrijireaParuluiSubmenu = driver.FindElement(By.CssSelector(ingrijireaParuluiSelector));
            ingrijireaParuluiSubmenu.Click();
        }



        //metoda pt a ajunge in pagina de NOUTATI
        public void ClickOnNoutati()
        {
            var noutatiElement = driver.FindElement(By.CssSelector(noutatiMainMenu));
            noutatiElement.Click();
        }



//metoda pe care o vom folosi in teste pt a returna textul specific paginilor INCALTAMINTE, IMBRACAMINTE, GENTI, COSMETICE, NOUTATI
        public string CheckMainMenuCategories()
        {
            var allMainCategoriesMessageElement = driver.FindElement(By.ClassName(allMainCategoriesLabelSelector));
            return allMainCategoriesMessageElement.Text;
        }



//metoda pt a returna textul specific paginii de submeniu COSMETICE/INGRIJIREA TENULUI
        public string CheckSubmenuCategories()
        {
            var submenuMessageElement = driver.FindElement(By.CssSelector(allSubmenuLabelSelector));
            Console.WriteLine(submenuMessageElement.Text);
            return submenuMessageElement.Text;    
        }




//metoda pe care o vom folosi in teste pt a returna textul specific paginii OUTLET
        public string CheckOutletPage()
        {
            var outletMessageElement = driver.FindElement(By.CssSelector(outletLabelSelector));
            return outletMessageElement.Text;
        }





//metoda pt a ajunge in pagina de OUTLET
        public void ClickOnOutlet()
        {
            var outletElement = driver.FindElement(By.CssSelector(outletMainMenu));
            outletElement.Click();
        }







    }
}
