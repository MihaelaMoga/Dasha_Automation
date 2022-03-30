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

//selectori specifici pt filtrare categorii din meniul principal: INCALTAMINTE, IMBRACAMINTE, GENTI, COSMETICE, NOUTATI i-am inlocuit cu cele 3 stringuri de mai jos si am facut encapsulation
        const string incaltaminteMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(1) > a"; //css
        const string imbracaminteMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(2) > a"; //css
        const string gentiMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(3) > a"; //css
        const string noutatiMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(5) > a"; //css
        const string outletMainMenu = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(6) > a"; //css

        string anyMainCategoryPart1 = "#dHF > nav.menubar.xs-hidden.sm-hidden > ul > li:nth-child(";//css
        string anyMainCategoryPart3 = ") > a";//css
        //parametru pt encapsulation
        string mainCategory;
        
        
       


        
//selector pt labelul tuturor paginilor din meniul principal, mai putin OUTLET
        const string allMainCategoriesLabelSelector = "category-title";//class
//selector pt labelul paginii de OUTLET
        const string outletLabelSelector = "#bodyBody > div.container.frontend-productslist > div > div.breadcrumbs > span > span > a > span"; //css






        //constructorul mosteneste constructorul din clasa parinte -> avem nevoie de driver pt a identifica elementele din pagina 
        public FilterFunctionality(IWebDriver driver) : base(driver)
        {
        }


//constructor pt encapsulation in legatura cu mainCategory
        public FilterFunctionality(IWebDriver driver,string mainCategory) : base(driver)
        {
            this.mainCategory = mainCategory;
        }


//metoda Getter pt encapsulation in legatura cu mainCatgeory
    public string GetItemMainCategory()
        {
            return this.mainCategory;
        }


//metoda cu encapsulation
        public void GoToItemMainCategory()
        {
            var anyMainCategory = driver.FindElement(By.CssSelector(anyMainCategoryPart1 + mainCategory + anyMainCategoryPart3));
            anyMainCategory.SendKeys(Keys.Enter);

        }



 //metoda care returneaza textul specific paginilor INCALTAMINTE, IMBRACAMINTE, GENTI, COSMETICE, NOUTATI
        public string CheckMainMenuCategories()
        {
            var allMainCategoriesMessageElement = driver.FindElement(By.ClassName(allMainCategoriesLabelSelector));
            return allMainCategoriesMessageElement.Text;
        }



        //metoda care returneaza textul specific paginii OUTLET
        public string CheckOutletPage()
        {
            var outletMessageElement = driver.FindElement(By.CssSelector(outletLabelSelector));
            return outletMessageElement.Text;
        }


    }
}
