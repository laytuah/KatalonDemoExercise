using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatalonDemoExercise.Hooks;
using System.Threading;

namespace KatalonDemoExercise.Pages
{
    public class Homepage
    {
        Context context;
        public Homepage(Context _context)
        {
            context = _context;
        }
        By cartLink = By.XPath("//a[@href='https://cms.demo.katalon.com/cart/']");

        By itemsPath = By.XPath("//a[text()='Add to cart']");
            public void AddFourItemsToCart()
        {
            IList<IWebElement> allItems = context.driver.FindElements(itemsPath);
            Random random = new Random();
            List<int> randomIndices = new List<int>();

            while (randomIndices.Count < 4)
            {
                int index = random.Next(0, allItems.Count);
                if (!randomIndices.Contains(index))
                    randomIndices.Add(index);
            }

            foreach (int index in randomIndices)
            {
                IWebElement element = allItems[index];
                element.SendKeys(Keys.Enter); /*This should have been '.click' but 'interception error' was 
                                               * encountered for specific items*/ Thread.Sleep(200);
            }
        }

        public CartPage ClickCartLink()
        {
            context.driver.FindElement(cartLink).Click();
            return new CartPage(context);
        }
    }
}
