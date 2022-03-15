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
        public const string cookieSelector = "body > div.optanon-alert-box-wrapper > div.optanon-alert-box-bg > div.optanon-alert-box-button-container > div.optanon-alert-box-button.optanon-button-allow > div > a"; //css
        public const string cookieSelectorFinal = "optanon-alert-box-close";



 //metoda e necesara pt SCROLL DOWN + a da click pe ACCEPT COOKIES pe mai multe pagini (de ex pe paginile: ContNouPage, IntraInContPage, ModificaParolaPage)
        public void ClickOnAcceptCookies()
        {
            Utilities.Utils.ScrollDownAndAcceptCookiesFinal(driver, cookieSelector);
        }


     


        //creez constructorul cu parametru driver
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
