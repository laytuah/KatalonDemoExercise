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
        //string column1 = "//table[@class='shop_table shop_table_responsive cart woocommerce-cart-form__contents']/tbody/tr[";
        //string column2 = "]";
        //string column3 = "/td[@class='product-remove']/a";
        //private string getCartItemLocator(int index)
        //{
        //    return $"//table[@class='shop_table shop_table_responsive cart woocommerce-cart-form__contents']/tbody/tr[{index}]";
        //}
        public int CountItemsInCart()
        {
            int numberOfItemsInCart = context.driver.FindElements(listCartItems).Count;
            return numberOfItemsInCart;
        }

        public void RemoveLowestPriceItem()
        {
            IList<IWebElement> tableRows = context.driver.FindElements(rowsintable);
            double smallestPrice = double.MaxValue;
            IWebElement itemWithSmallestPrice = null;
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
            if (itemWithSmallestPrice != null)
            {
                itemWithSmallestPrice.FindElement(By.ClassName("remove")).Click();
            }
            Thread.Sleep(3000);


            //IList<IWebElement> tableRows = context.driver.FindElements(rowsintable);
            //double smallestPrice = double.MaxValue;
            //int rowIndex = -1;
            //for (int i = 1; i <= tableRows.Count; i++)
            //{
            //    string tableRowLocator = getCartItemLocator(i);
            //    string itemData = context.driver.FindElement(By.XPath(tableRowLocator)).Text.Trim();
            //    string[] itemSplittedData = itemData.Split('$');
            //    double itemPrice = double.Parse(itemSplittedData[2]);
            //    if (itemPrice < smallestPrice)
            //    {
            //        smallestPrice = itemPrice;
            //        rowIndex = i;
            //    }
            //}
            //string removeLowestPriceItem = $"{getCartItemLocator(rowIndex)}{column3}";
            //context.driver.FindElement(By.XPath(removeLowestPriceItem)).Click();
            //Thread.Sleep(3000);
        }
    }
}
