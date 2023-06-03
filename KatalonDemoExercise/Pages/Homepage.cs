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
        By itemOne = By.XPath("//a[@data-product_id='54']");
        By itemTwo = By.XPath("//a[@data-product_id='26']");
        By itemThree = By.XPath("//a[@data-product_id='66']");
        By itemFour = By.XPath("//a[@data-product_id='57']");
        By cartLink = By.XPath("//a[@href='https://cms.demo.katalon.com/cart/']");
        public void AddFourItemsToCart()
        {
            context.driver.FindElement(itemOne).Click(); Thread.Sleep(200);
            context.driver.FindElement(itemTwo).Click(); Thread.Sleep(200);
            context.driver.FindElement(itemThree).Click(); Thread.Sleep(200);
            context.driver.FindElement(itemFour).Click(); Thread.Sleep(200);
        }

        public CartPage ClickCartLink()
        {
            context.driver.FindElement(cartLink).Click();
            return new CartPage(context);
        }
    }
}
