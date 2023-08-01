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
        By listCartItems = By.CssSelector("tr[class='woocommerce-cart-form__cart-item cart_item']");
        By rowsintable = By.XPath("//table[@class='shop_table shop_table_responsive cart woocommerce-cart-form__contents']/tbody/tr[@class]");
        
        double smallestPrice = double.MaxValue;
        IWebElement itemWithSmallestPrice = null;
        public int CountItemsInCart()
        {
            int numberOfItemsInCart = context.driver.FindElements(listCartItems).Count;
            return numberOfItemsInCart;
        }

        public void SearchLowestPriceItem()
        {
            IList<IWebElement> tableRows = context.driver.FindElements(rowsintable);
            foreach (IWebElement row in tableRows)
            {
                string itemData = row.FindElement(By.ClassName("woocommerce-Price-amount")).Text.Trim();
                string[] itemSplittedData = itemData.Split('$');
                double itemPrice = double.Parse(itemSplittedData[1]);
                if (itemPrice < smallestPrice)
                {
                    smallestPrice = itemPrice;
                    itemWithSmallestPrice = row;
                }
            }
        }

        public void RemoveLowestPriceItem()
        {
            if (itemWithSmallestPrice != null)
            {
                itemWithSmallestPrice.FindElement(By.ClassName("remove")).Click();
            }
            Thread.Sleep(3000);
        }
    }
}
