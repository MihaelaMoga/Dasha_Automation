using AventStack.ExtentReports;
using Microsoft.VisualBasic.FileIO;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Dasha_Automation.Utilities
{
    //in clasa Utils vom pune toate metodele statice din proiect
   public class Utils
    {


        //click pe becul galben si selectez "using OpenQA.Selenium"
        public static IWebElement WaitForExplicitElement(IWebDriver driver,int seconds, By locator)
        {
            //definim wait-ul explicit
            //click pe bec galben 
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));

        }

        public static IWebElement WaitForElementClickable(IWebDriver driver, int seconds, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }


        public static IWebElement WaitForFluentElement(IWebDriver driver, int seconds, By locator)
        {
            //definim un fluentWait in driverul nostru
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver)

            //initializam proprietatile obiectului fluentWait
            {
                //setam timeout-ul de 20 secunde
                Timeout = TimeSpan.FromSeconds(seconds),
                //la fiecare 100 milisecunde verifica daca a aparut elementul in pagina; daca nu a aparut: mai verifica peste alte 100 milisecunde
                PollingInterval = TimeSpan.FromMilliseconds(100),
                //mesajul customizat va inlocui mesajul standard
                Message = "Sorry! the element cannot be found in page"
                };


            //mesajul standard e de tipul NoSuchElementException => vrem sa fie ignorat mesajul standard
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
           

            
            //in paranteze rotunde apare ca se asteapta pana cand gasim elementul cu locator (ID btn2) in pagina
            //dupa ce gaseste elementul in pagina, returneaza fluentWait
            return fluentWait.Until(x => x.FindElement(locator));
        }





      

        //metoda pt inchidere banner cookies
        public static void CloseTheCookiesBanner(IWebDriver driver, int sec, string locator)
        {
            var closeBanner = Utilities.Utils.WaitForExplicitElement(driver, sec, By.CssSelector(locator));
            closeBanner.SendKeys(Keys.Enter);
        
        }



    


    public static void PrintCookies(ICookieJar cookies)
        {
            Console.WriteLine("The site contains {0} cookies", cookies.AllCookies.Count);
            //vrem sa afisam fiecare cookie (numele si valoarea fiecarui cookie)
            //cookies.AllCookies este o lista cu toate cookie-urile site-ului
            foreach (Cookie c in cookies.AllCookies)
            {
                Console.WriteLine("Cookie name {0} - cookie value {1}", c.Name, c.Value);
            }
           
        }




        public static string GetDateFromDateTime(DateTime datevalue)
        {
            return datevalue.ToShortDateString();
        }




        /// <summary>
        /// the method creates a screenshot based on current date & saves it into a folder defined by tester
        /// </summary>
        /// <param name="driver">the WebDriver instance/browser from which the screenshot will be made</param>
        /// <param name="path">the path where screenshot will be saved</param>
        /// <param name="fileName">specify file name that will have appended the date to have unique files</param>
        /// <param name="format">specify the image format for screenshot</param>

        public static void TakeScreenshotWithDate(IWebDriver driver, string path, string fileName, ScreenshotImageFormat format)
        {
            //cream un obiect numit validation de tipul clasei DirectoryInfo (adica de tip folder) cu parametru path
            DirectoryInfo validation = new DirectoryInfo(path);
            //daca NU exista un folder pe calea path pt a salva print screenu-urile, definim mai jos sa creeze automat un folder
            if (!validation.Exists)
            {
                validation.Create();
            }
            string currentDate = DateTime.Now.ToString();
            StringBuilder sb = new StringBuilder(currentDate);
            sb.Replace(":", "_");
            sb.Replace("/", "_");
            sb.Replace(" ", "_");
            string finalFilePath = String.Format("{0}\\{1}_{2}.{3}", path, fileName, sb.ToString(), format);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(finalFilePath, format);
        }



//metoda pt a face screenshot pt test report
        //pt linia de mai jos, click pe beculetul galben si selectez "using AventStack.ExtentReports;"
        public static MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string name)
        {
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, name).Build();
        }





        //passez driverului un script de Javascript si driverul va executa acel script
        public static string ExecuteJsScript(IWebDriver driver, string script)
        {
            
            var jsExecutor = (IJavaScriptExecutor)driver;
            //result returneaza un obiect
           var result = jsExecutor.ExecuteScript(script);
            if(result != null)
            {
               Console.WriteLine(result.ToString());
            }
            var stringForAssert = result.ToString();
            return stringForAssert;
        }






        /// <summary>
        /// the method converts a config file that has lines as key = value into a Dictionary with key and value
        /// </summary>
        /// <param name="configFilePath">the path of config file</param>
        /// <returns>returns a dictionary with a key value pair of type string and string representing the lines in the config file</returns>



//METODA 1 folosita pt citirea datelor de test din fisier csv
        //ReadConfig este metoda pt citirea unui fisier extern, de tip text, DE PROPRIETATI (denumit config.properties)
        //string configFilePath e calea catre fisierul config.properties
        //eu ii dau fisierul de configurare, de tip PROPERTIES
        //metoda va citi config.properties + va returna un Dictionar de tip cheie valoare cu informatiile din config.properties

        public static Dictionary<string,string> ReadConfig(string configFilePath)
        {
            //cream Dictionarul de tip string,string
            var configData = new Dictionary<string, string>();

            //citeste fiecare linie din fisierul text 
            foreach(var line in File.ReadAllLines(configFilePath))
            {
                //pt fiecare linie din fisier, adaugam in Dictionar: valoarea dinainte de =, valoarea de dupa =
                //am folosit Trim() pt a elimina cazurile cand in config.properties se pune spatiu inainte sau dupa egal
                string[] values = line.Split('=');
                configData.Add(values[0].Trim(), values[1].Trim());
            }

           //metoda va citi fisierul de configurare si va returna un Dictionar de tip cheie valoare cu informatiile din acel fisier
            return configData;
        }




        public static string[][] GetGenericData(string path)
        {

            //pt fiecare din linie face split dupa virgula & sare prima linie
            var lines = File.ReadAllLines(path).Select(a => a.Split(',')).Skip(1);
            return lines.ToArray();
        }






//METODA 2 pt citire date de test: dintr-un DataTable - FRECVENT folosita de DEV
        public static DataTable GetDataTableFromCsv(string csv)
        {
            //cream un obiect dataTable (care e returnat la finalul acestei metode)
            DataTable dataTable = new DataTable();
            try
            {
                  //TextFieldParser este o clasa generica dotnet => cream obiectul csvReader
                using (TextFieldParser csvReader = new TextFieldParser(csv))
                {
                    //cod pt a indica cum sunt separate valorile in cadrul csv-ului (in cazul prezent prin virgula)
                    csvReader.SetDelimiters(new string[] { "," });
                    //daca in tabel am avea 2 delimitatori, atunci scriem codul de mai jos in locul liniei de mai sus
                    //csvReader.SetDelimiters(new string[] { ",",";"});
                    //codul de mai jos il scriem ca sa acoperim si cazurile in care datele dintr-o celula sunt intre ghilimele=> vrem sa eliminam ghilimelele
                    csvReader.HasFieldsEnclosedInQuotes = true;
            //citim capul de tabel
                    string[] columnNames = csvReader.ReadFields();
                    foreach (string column in columnNames)
                    {
                        //DataColumn este o clasa generica dotnet pt coloane
                        DataColumn dataColumn = new DataColumn();
                        //permitem valori nule in coloane
                        dataColumn.AllowDBNull = true;
                        dataTable.Columns.Add(dataColumn);
                    }
             //citim liniile din tabel
                    //cat nu am ajuns pe ultima linie din DataTable
                    while (!csvReader.EndOfData)
                    {
                        //cream un array numit rowValues
                        string[] rowValues = csvReader.ReadFields();

                        // this for can be skipped (it is used for sanitisation purposes)
                    //    for (int i = 0; i<rowValues.Length; i++)
                    //    {
                    //        if (rowValues[i] == "")
                     //       {
                      //          rowValues[i] = null;
                      //      }
                     //   }

                        dataTable.Rows.Add(rowValues);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Could not read from csv file {0}", csv));
            }
            //se returneaza obiectul dataTable
            return dataTable;
        }





       

//METODA 3 pt citire date de test din primul sheet dintr-un excel 
                //valorile din excel le vom varsa intr-un DataTable
                public static DataTable GetDataTableFromExcel(string excelPath)
               //daca excelul are mai multe sheeturi si de ex vrem sa citim doar sheetul cu denumirea X, voi folosi linia de mai jos in locul celei anterioare
               // public static DataTable GetDataTableFromExcel(string excelPath, string sheetName)
                {
                    DataTable dataTable = new DataTable();
                    //creez variabila wb de tip Workbook (in excel workbook e tot fisierul excel)
                    XSSFWorkbook wb;
                    //creez variabila sh de tip sheet (workbookul e o colectie de sheeturi)
                    XSSFSheet sh;
                    //creez variabila sheetName (intrucat fiecare sheet are un nume)
                    string sheetName;
                    //variabila fs merge pe path-ul indicat, deschide excelul cu drepturi de Read
                    using (var fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                    {
                        //incarc excelul deschis in variabila wb
                        wb = new XSSFWorkbook(fs);
                        //numele primului sheet (adica 0) e incarcat in variabila sheetName
                        sheetName = wb.GetSheetAt(0).SheetName;
                    }

                    dataTable.Columns.Clear();
                    dataTable.Rows.Clear();

                    //din workbook ia sheetul numit sheetName si il incarca in variabila sh
                    sh = (XSSFSheet)wb.GetSheet(sheetName);
                    //merge pe prima linie din excel
                    int i = 0;
                    //cat timp mai exista linii de citit in excel
                    while (sh.GetRow(i) != null)
                    {
                        //cat timp mai sunt coloane de citit
                        if (dataTable.Columns.Count < sh.GetRow(i).Cells.Count)
                        {
                            for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                            {
                                //in toate celulele de pe linie adaugam toate valorile ca nule (adica "") si de tip string
                                dataTable.Columns.Add("", typeof(string));
                            }
                        }

                        dataTable.Rows.Add();
                        //din sheetul curent merge pe linia i si numara cate valori sunt pe linia i
                        for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                        {
                            //de pe linia i iau valoarea din coloana j; apoi valoarea j e salvata in variabila cell
                            var cell = sh.GetRow(i).GetCell(j);
                            //daca valoarea din celula nu este nula
                            if (cell != null)
                            {
                                //verificam ce tip de date e in acea celula
                                switch (cell.CellType)
                                {
                                    //daca in celula avem valoare de tip numeric
                                    case NPOI.SS.UserModel.CellType.Numeric:
                                        {
                                            dataTable.Rows[i][j] = sh.GetRow(i).GetCell(j).NumericCellValue;
                                            break;
                                        }
                                    //daca in celula avem valoare de tip string
                                    case NPOI.SS.UserModel.CellType.String:
                                        {
                                            dataTable.Rows[i][j] = sh.GetRow(i).GetCell(j).StringCellValue;
                                            break;
                                        }
                                    default: // if the cell type is not numeric or string
                                        {
                                            dataTable.Rows[i][j] = "";
                                            break;
                                        }
                                }
                            }
                        }
                        //parcurgem toate liniile din sheet
                        i++;
                    }

                    return dataTable;
                }

        




//METODA 4 pt citire date de test dintr-un json
    //mapeaza fisierul jsonFile la clasa T
        public static T JsonRead<T>(string jsonFile)
        {
            string text = File.ReadAllText(jsonFile);
            return JsonSerializer.Deserialize<T>(text);
        }





//METODA 5 de citire a datelor de test dintr-un fisier xml
        //am facut metoda direct in AuthTest.cs

        //Alex recomanda ca intr-un fisier xml sa fie o singura serie de date de test
        //=> de exemplu la AuthTest avem 4 serii de date de test=> vom crea 4 xml-uri separate 
        //=> Cum CITIM MAI MULTE fisiere xml?

        public static List<string> GetAllFilesInFolderExt(string path, string extension)
        {
            //creez o lista de fisiere
            List<string> files = new List<string>();
            //luam toate folderele de pe calea path
            DirectoryInfo di = new DirectoryInfo(path);
            //pt fiecare fisier din folderul respectiv, cu o anumita extensie, dar numai din directorul curent
            foreach (FileInfo fi in di.GetFiles(extension, System.IO.SearchOption.TopDirectoryOnly))
            {
                //
                files.Add(fi.FullName);
            }
            //returnez toate fisierele de tip xml din folderul indo=icat mai sus
            return files;
        }






//metoda de CRIPTARE pe care o vom folosi pt a ascunde datele cu care ma conectez la server
//deci metoda ia un sir de caractere (adica datele de conectare la server) si pe baza unei chei de criptare + metode de criptare gen tripleDESCryptoService => returneaza un Base64
        //Base64 e folosit pt formatarea datelor - la ce e util?
        //pas 1: dechis fisierul binar (poza/pdf, etc) cu un Notepad
        //pas 2: dau copy paste la ce apare in Notepad pe site-ul base64encode.org +click pe Encode=> aplic o transformare intr-un Base64 => rezulta un fisier text

        //key este cheia de criptare
        public static string Encrypt(string source, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    byte[] data = Encoding.UTF8.GetBytes(source);

                    //fisierele binare sunt trecute prin Base64 pt a incerca sa transformam fisierul binar in fisier text  
                    return Convert.ToBase64String(tripleDESCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }





//metoda de DECRIPTARE primeste un fisier Base64 si il 
        public static string Decrypt(string encrypt, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    byte[] data = Convert.FromBase64String(encrypt);
                    return Encoding.UTF8.GetString(tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }

    




//metoda pentru CONVERTIRE dictionar queryParams in Querry (pt a testa API-ul folosind Selenium)
        public static string ConvertDictionaryToQuerry(Dictionary<string,string> queryParams)
        {
            StringBuilder sb = new StringBuilder();
            //pt fiecare key (adica parametrii API-ului: to, from) din queryParams
            foreach(string key in queryParams.Keys)
            {
                //adaug perechile cheie=valoare cheie
                sb.Append(String.Format("&{0}={1}",key,queryParams[key]));
            }
            //returnam un Querry de tip string
            return sb.ToString();
        }




//metoda pt a CONVERTI un fisier CSV intr-o lista de dictionare
        //de ce lista de dictionare? pt ca pt fiecare linie din fisierul csv se va crea un dictionar
        public static List<Dictionary<string, string>> ConvertCsvToDictionary(string filePath)
        {
            var lines = File.ReadAllLines(filePath).Select(a => a.Split(','));
            List<Dictionary<string, string>> dictionaryList = new List<Dictionary<string, string>>();
            string[] header = lines.ElementAt(0).ToArray();
            //incepand cu linia 2 a fisierului csv
            for (int i = 1; i < lines.Count(); i++)
            {
                var currentValues = lines.ElementAt(i).ToArray();
             //pt fiecare linie in parte vom avea un dictionar => vom avea o lista de dictionare 
                //queryParams sunt from,to,outFormat,unit
                Dictionary<string, string> queryParams = new Dictionary<string, string>();
                for (int j = 0; j < currentValues.Count(); j++)
                {
                    queryParams.Add(header[j], currentValues[j]);
                }
                //adaugam queryParams in dictionaryList
                dictionaryList.Add(queryParams);
            }

            //metoda curenta returneaza o lista de dictionare
            return dictionaryList;
        }






//metoda GENERALA pt randomizare parola la creare cont nou

        public static string GenerateRandomPassStringCount(int count)
        {
            const string chars = "abcdefghijklmnoprstuvxyz1234567890";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                Random r = new Random();
                //adaugam chars de la 0 la nr maxim de caractere
                sb.Append(chars[r.Next(chars.Length)]);
            }
            return sb.ToString();
        }



//metoda pt randomizare Nume si Prenume valid: minim 3 LITERE oriunde in Prenume 
        
 //count e nr de caractere pt Nume si Prenume - de ex vrem un Nume din 3 litere
        public static string GenerateRandomNumePrenumeStringCount(int count)
        {
            //in char stocam toate caracterele 
            const string chars = "abcdefghijklmnoprstuwvxyz";
           
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                Random r = new Random();
                //random adaugam chars (chars.Length este nr maxim de caractere
                sb.Append(chars[r.Next(chars.Length)]);
            }
            return sb.ToString();
        }




//metoda pt randomizare Parola valida: inainte de @: minim 3 caractere (LITERE/NUMERE/ca si caractere speciale DOAR .-_ ) 

        //count e nr de caractere pt parola
        public static string GenerateRandomParolaAndEmailStringCount(int count)
        {
            //in char stocam toate caracterele 
            const string chars = "abcdefghijklmnoprstuwvxyz1234567890._-";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                Random r = new Random();
                //random adaugam chars (chars.Length este nr maxim de caractere
                sb.Append(chars[r.Next(chars.Length)]);
            }
            return sb.ToString();
        }



//metoda pt randomizare Parola valida: imediat dupa @ minim 2 caractere (LITERE/NUMERE/caractere speciale doar - )

        //count e nr de caractere pt parola
        public static string GenerateRandomEmailPart22StringCount(int count)
        {
            //in char stocam toate caracterele 
            const string chars = "abcdefghijklmnoprstuwvxyz1234567890-";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                Random r = new Random();
                //random adaugam chars (chars.Length este nr maxim de caractere
                sb.Append(chars[r.Next(chars.Length)]);
            }
            return sb.ToString();
        }



//metoda pt randomizare Parola valida: imediat dupa puntc: minim 2 caractere (LITERE/NUMERE) 

        //count e nr de caractere pt parola
        public static string GenerateRandomEmailPart32StringCount(int count)
        {
            //in char stocam toate caracterele 
            const string chars = "abcdefghijklmnoprstuwvxyz1234567890";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                Random r = new Random();
                //random adaugam chars (chars.Length este nr maxim de caractere
                sb.Append(chars[r.Next(chars.Length)]);
            }
            return sb.ToString();
        }


    }


}

    

