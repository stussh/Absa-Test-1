using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System.Threading.Tasks;
using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace Assessment
{
    [TestFixture]
    public class WebTablesTest
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void ValidateUserAddUserCheckInput()
        {
            //Setup the button variable
            driver.Url = "https://www.way2automation.com/angularjs-protractor/webtables/";
            var sutButton = driver.FindElement(By.CssSelector("button[type='add']"));

            //Click Add User
            sutButton.Click();

            //Setup firstName field variable and randomisation
            var sutNameField = driver.FindElement(By.Name("FirstName"));
            var firstName = Faker.Name.First();
            sutNameField.SendKeys(firstName);

            //Setup lastName field variable and randomisation
            var sutLNameField = driver.FindElement(By.Name("LastName"));//
            var lastName = Faker.Name.Last();
            sutLNameField.SendKeys(lastName);

            //Setup userName field variable and randomisation
            var sutUNameField = driver.FindElement(By.Name("UserName"));
            var userName = Faker.Internet.UserName();
            sutUNameField.SendKeys(userName);

            //Setup password field variable and randomisation
            var sutPasswordField = driver.FindElement(By.Name("Password"));
            var password = new Guid().ToString();
            sutPasswordField.SendKeys(password);

            //Setup customer radio button randomisation
            IList<IWebElement> sutRadioButton = driver.FindElements(By.Name("optionsRadios"));
            var random = new Random();
            int randomIndex = random.Next(sutRadioButton.Count);
            sutRadioButton.ElementAt(randomIndex).Click();

            //Setup role drop down listrandomisation
            var sutIdRole = driver.FindElement(By.Name("RoleId"));

            var selectElement = new SelectElement(sutIdRole);
            randomIndex = random.Next(1, selectElement.Options.Count);
            selectElement.SelectByIndex(randomIndex);

            //Setup email field variable and randomisation
            var sutEMailField = driver.FindElement(By.Name("Email"));
            //
            var email = Faker.Internet.Email();
            sutEMailField.SendKeys(email);

            //Setup phone field variable and randomisation
            var sutPhone = driver.FindElement(By.Name("Mobilephone"));
            //
            var phone = Faker.Phone.Number();
            sutPhone.SendKeys(phone);

            //click on the save button
            var sutSaveButton = driver.FindElement(By.XPath("//*[text()='Save']"));
            sutSaveButton.Click();

            bool valuesExist = false;

            //Check if the names have been added correctly
            IWebElement tableElement = driver.FindElement(By.CssSelector("table[table-title='Smart Table example']"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
            IList<IWebElement> rowTD;
            foreach (IWebElement row in tableRow)
            {
                rowTD = row.FindElements(By.TagName("td"));

                if (rowTD.Count > 6)
                {
                    valuesExist = rowTD[0].Text.Equals(firstName) &&
                        rowTD[1].Text.Equals(lastName);
                }

                if (valuesExist)
                    break;
                

            }

            Assert.True(valuesExist);
            
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }

    }
}


/*Task:
Your task is to automate the two test cases below. You are free to use any Open Source automation frameworks, but please do list the tools and resources used. Below is a list of patterns and practices that we are looking for in your solution:
 Hybrid approach with modularization
 Descriptive programming
 Regular expressions
 Parameterization
 At least two ways of storing and utilizing test data
 Report stores test evidence and results

Task 2 - Web:
Create the following test case:
• Navigate to - http://www.way2automation.com/angularjs-protractor/webtables/
• Validate that you are on the User List Table
• Click Add user
• Add users with the following details:
• Ensure that User Name (*) is unique on each run
• Ensure that your users are added to the list 
*/