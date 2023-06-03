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
    public class CartPage
    {
        Context context;
        public CartPage(Context _context)
        {
            context = _context;
        }
        By removeItem = By.XPath("//a[@class='remove'] [@data-product_id='54']");
        By listCartItems = By.CssSelector("tr[class='woocommerce-cart-form__cart-item cart_item']");
        //Chose to user Css Selector since it's shorter
        //By listCartItems = By.XPath("//table[@class='shop_table shop_table_responsive cart woocommerce-cart-form__contents']/tbody/tr[@class]");
        public void RemoveItem()
        {
            context.driver.FindElement(removeItem).Click();
        }

        public int CountItemsInCart()
        {
            int numberOfItemsInCart = context.driver.FindElements(listCartItems).Count;
            return numberOfItemsInCart;
        }

        public void RemoveLowestPriceItem()
        {
            context.driver.FindElement(removeItem).Click();
            Thread.Sleep(1000);
        }
    }
}
