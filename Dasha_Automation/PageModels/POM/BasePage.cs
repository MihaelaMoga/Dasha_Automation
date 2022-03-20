using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dasha_Automation.PageModels.POM
{
    public class BasePage
    {
        public IWebDriver driver;

     //selectorul pt Accept Cookies este acelasi pentru mai multe pagini (de ex ContNouPage, IntraInContPage, ModificaParolaPage)
        public const string theCookieSelector = "body > div.optanon-alert-box-wrapper > div.optanon-alert-box-bottom-top > div > a"; //css
        


        //metoda pt accept cookies
        public void CloseTheCookies()
        {
            Utilities.Utils.CloseTheCookiesBanner(driver, 4, theCookieSelector);
        }


        //creez constructorul cu parametru driver
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
