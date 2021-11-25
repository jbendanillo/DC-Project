using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using static Jeniza_DC_Project.HelperFunctions;

namespace Jeniza_DC_Project
{
    public class Tests
    {
        class User: Base
        {
            private IWebElement menuButton => webDriver.FindElement(By.XPath("//*[@id='left']/div[1]/div/button"));
            private IWebElement payeeButton => webDriver.FindElement(By.XPath("//*[@id='left']/div[1]/div/div[3]/section/div[2]/nav[1]/ul/li[3]"));
            private IWebElement addButton => webDriver.FindElement(By.XPath("//*[@id='YouMoney']/div/div/div[3]/section/section/div/div[2]/header[2]/div/div[3]/button"));
            private IWebElement paymentsButton => webDriver.FindElement(By.XPath("//*[@id='left']/div[1]/div/div[3]/section/div[2]/nav[1]/ul/li[1]"));
            private IWebElement payeeNameInput => webDriver.FindElement(By.XPath("//*[@id='ComboboxInput-apm-name']"));
            private IWebElement payeeBankInput => webDriver.FindElement(By.XPath("//*[@id='apm-bank']"));
            private IWebElement payeeBranchInput => webDriver.FindElement(By.XPath("//*[@id='apm-branch']"));
            private IWebElement payeeAccountInput => webDriver.FindElement(By.XPath("//*[@id='apm-account']"));
            private IWebElement payeeSuffixInput => webDriver.FindElement(By.XPath("//*[@id='apm-suffix']"));
            private IWebElement payeeAddButton => webDriver.FindElement(By.XPath("//*[@id='apm-form']/div[2]/button[3]"));
            private IWebElement sortButton =>  webDriver.FindElement(By.XPath("//*[@id='YouMoney']/div/div/div[3]/section/section/div/div[2]/header[2]/div/div[1]/h3"));

            //TC1: Verify you can navigate to Payees page using the navigation menu
            [Test]
            public void TC1()
            {
                // Click Menu button
                menuButton.Click();
                Thread.Sleep(3000);

                // Click payee button
                payeeButton.Click();
                Thread.Sleep(3000);

                // Pass if url contains "payees"
                String pageUrl = webDriver.Url;
                if (pageUrl.Contains("payees"))
                    Assert.Pass("Passed");
                else
                    Assert.Fail("Failed");
            }

            // TC2: Verify you can add new payee in the Payees page
            [Test]
            public void TC2()
            {
                // Click Menu button
                menuButton.Click();
                Thread.Sleep(3000);

                // Click payee button
                payeeButton.Click();
                Thread.Sleep(3000);

                // Click add button
                addButton.Click();
                Thread.Sleep(2000);

                // Enter new payee name
                payeeNameInput.SendKeys("test");
                payeeNameInput.SendKeys(Keys.Enter);
                Thread.Sleep(3000);

                // Enter account number
                payeeBankInput.SendKeys("21");
                payeeBranchInput.SendKeys("3131");
                payeeAccountInput.SendKeys("3131111");
                payeeSuffixInput.SendKeys("000");
                payeeNameInput.SendKeys(Keys.Enter);
                Thread.Sleep(3000);

                // Click add payee
                payeeAddButton.Click();
                Thread.Sleep(3000);

                // Check if added payee exists
                var newPayeeExists = CheckIfPayeeExist("test");

                // Pass if new payee exists in the list
                if (newPayeeExists)
                    Assert.Pass("Passed");
                else
                    Assert.Fail("Failed");
            }

            // TC3: Verify payee name is a required field
            [Test]
            public void TC3()
            {
                // Click Menu button
                menuButton.Click();
                Thread.Sleep(3000);

                // Click payeeButton
                payeeButton.Click();
                Thread.Sleep(3000);

                // Click add button
                addButton.Click();
                Thread.Sleep(2000);

                // Click add payee
                payeeAddButton.Click();
                Thread.Sleep(3000);

                // Validate payee name field if it has error attribute/message
                var hasError = payeeNameInput.GetAttribute("class").Split(" ").Contains("field-error");

                // if payee name has error populate mandatory fields
                if (hasError)
                {
                    payeeNameInput.SendKeys("test");
                    payeeNameInput.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);

                    payeeBankInput.SendKeys("21");
                    payeeBranchInput.SendKeys("3131");
                    payeeAccountInput.SendKeys("3131111");
                    payeeSuffixInput.SendKeys("000");
                    payeeNameInput.SendKeys(Keys.Enter);
                    Thread.Sleep(3000);

                    // validate mandatory fields again
                    hasError = payeeNameInput.GetAttribute("class").Split(" ").Contains("field-error");
                }

                // Pass if there are no error messages
                if (hasError == false)
                    Assert.Pass("Passed");
                else
                    Assert.Fail("Failed");
            }

            // TC4: Verify that payees can be sorted by name
            [Test]
            public void TC4()
            {
                // Click Menu button
                menuButton.Click();
                Thread.Sleep(3000);

                // Click Payee button
                payeeButton.Click();
                Thread.Sleep(3000);

                // Check if ascending by default
                var isAscending = CheckIfListAscending();

                // Click sort button to sort by descending
                sortButton.Click();
                Thread.Sleep(3000);

                // Check if descending
                var isDescending = CheckIfListDescending();

                // Pass if both ascending and descending checks are ok
                if (isAscending && isDescending)
                    Assert.Pass("Passed");
                else
                    Assert.Fail("Failed");
            }

            // TC5 - Navigate to Payments page - Verify the current balance of Everyday account and Bills account are correct
            [Test]
            public void TC5()
            {
                // Click Menu button
                menuButton.Click();
                Thread.Sleep(3000);

                // Click Payments button
                paymentsButton.Click();
                Thread.Sleep(2000);

                //Click "Payments from" button
                IWebElement paymentsFromButton = webDriver.FindElement(By.XPath("//button[contains(@data-testid, 'from-account-chooser')]"));
                paymentsFromButton.Click();
                Thread.Sleep(3000);

                // Loop through payees and select Everyday Account
                IList<IWebElement> payees = webDriver.FindElements(By.TagName("li"));
                for (int i = 0; i < payees.Count; i++)
                {
                    var text = payees.ElementAt(i).FindElement(By.ClassName("name-1-1-65")).Text;
                    if (text == "Everyday")
                    {
                        payees.ElementAt(i).Click();
                    }
                }
                Thread.Sleep(2000);

                // Click "Payments to" button
                IWebElement paymentsToButton = webDriver.FindElement(By.XPath("//button[contains(@data-testid, 'to-account-chooser')]"));
                paymentsToButton.Click();
                Thread.Sleep(2000);

                // Click "Accounts" tab
                IWebElement paymentsAccountsTab = webDriver.FindElement(By.XPath("//*[@id='react-tabs-2']"));
                paymentsAccountsTab.Click();
                Thread.Sleep(2000);

                // Loop through accounts and select Bills Account
                IWebElement accountsDiv = webDriver.FindElement(By.XPath("//div[contains(@data-testid, 'to-accounts-list-results')]"));
                var accountsList = accountsDiv.FindElements(By.TagName("li"));
                for (int i = 0; i < accountsList.Count; i++)
                {
                    var text = accountsList.ElementAt(i).FindElement(By.ClassName("name-1-1-65")).Text;
                    if (text == "Bills")
                    {
                        accountsList.ElementAt(i).Click();
                    }
                }
                Thread.Sleep(2000);

                // Enter 500 to amount
                IWebElement amountInput = webDriver.FindElement(By.XPath("//*[@id='field-bnz-web-ui-toolkit-12']"));
                amountInput.SendKeys("500");
                Thread.Sleep(2000);


                // Click transfer money button
                IWebElement transferButton = webDriver.FindElement(By.XPath("//*[@id='paymentForm']/div[4]/div/button[1]"));
                transferButton.Click();
                Thread.Sleep(4000);

                // Get "Everyday Account" balance
                IWebElement everydayAccount = webDriver.FindElement(By.Id("account-ACC-1"));
                var everydayAccountBalance = everydayAccount.FindElement(By.ClassName("account-balance")).Text;
                
                // Get "Bills" balance
                IWebElement billsAccount = webDriver.FindElement(By.Id("account-ACC-5"));
                var billsAccountBalance = billsAccount.FindElement(By.ClassName("account-balance")).Text;

                // Pass if account balances are correct
                if (everydayAccountBalance == "2,000.00" && billsAccountBalance == "920.00")
                    Assert.Pass("Passed");
                else
                    Assert.Fail("Failed");
            }

        }

    }
}