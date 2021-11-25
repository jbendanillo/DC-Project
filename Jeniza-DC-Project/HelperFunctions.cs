using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using static Jeniza_DC_Project.Base;

namespace Jeniza_DC_Project
{
    public static class HelperFunctions
    {
        public static Boolean CheckIfPayeeExist(string name)
        {
            // Get all payees from the list and find the newly added payee
            IList<IWebElement> payees = webDriver.FindElements(By.TagName("li"));
            for (int i = 0; i < payees.Count; i++)
            {
                var text = payees.ElementAt(i).FindElement(By.ClassName("js-payee-name")).Text;
                if (text == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static Boolean CheckIfListAscending()
        {
            // Get list of all payee names
            IList<IWebElement> payees = webDriver.FindElements(By.TagName("li"));
            var listPayees = new List<string>();
            for (int i = 0; i < payees.Count; i++)
            {
                var payee = payees.ElementAt(i).FindElement(By.ClassName("js-payee-name")).Text;
                listPayees.Add(payee);
            }

            // Create list of original ascending list
            var unsortedListPayees = new List<string>(listPayees);

            // Sort list ascending
            var sortedListPayees = new List<string>(listPayees);
            sortedListPayees.Sort((a, b) => a.CompareTo(b));

            // Check if list ascending by comparing the original and the new sorted list
            var isAscending = listPayees.SequenceEqual(sortedListPayees);
            if (isAscending) { return true; } else { return false; };

        }

        public static Boolean CheckIfListDescending()
        {
            // Get list of payee names
            IList<IWebElement> payees = webDriver.FindElements(By.TagName("li"));
            var listPayees = new List<string>();
            for (int i = 0; i < payees.Count; i++)
            {
                var payee = payees.ElementAt(i).FindElement(By.ClassName("js-payee-name")).Text;
                listPayees.Add(payee);
            }

            // create list of original ascending list
            var unsortedListPayees = new List<string>(listPayees);
            var sortedListPayees = new List<string>(listPayees);

            // sort list descending
            sortedListPayees.Sort((a, b) => b.CompareTo(a));

            // check if list descending by comparing the original and the new sorted list
            var isDescending = sortedListPayees.SequenceEqual(unsortedListPayees);
            if (isDescending) { return true; } else { return false; };

        }
    }
}
