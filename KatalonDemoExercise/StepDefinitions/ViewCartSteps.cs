using System;
using TechTalk.SpecFlow;
using KatalonDemoExercise.Hooks;
using KatalonDemoExercise.Pages;
using NUnit.Framework;

namespace KatalonDemoExercise.StepDefinitions
{
    [Binding]
    public class ViewCartSteps
    {
        Context context;
        Homepage homepage;
        CartPage cartpage;
        public ViewCartSteps(Context _context, Homepage _homepage, CartPage _cartpage)
        {
            context = _context; homepage = _homepage; cartpage = _cartpage;
        }
        [Given(@"that katalon Ecommerce website is loaded")]
        public void GivenThatKatalonEcommerceWebsiteIsLoaded()
        {
            context.LoadKatalonDemoApplication();
        }
        
        [When(@"a user adds (.*) items to cart")]
        public void WhenAUserAddsItemsToCart(int itemQuantity)
        {
            homepage.AddFourItemsToCart();
        }
        
        [When(@"a user clicks on view cart")]
        public void WhenAUserClicksOnViewCart()
        {
            homepage.ClickCartLink();
        }
        
        [When(@"a user clicks on the remove item button for the lowest price item")]
        public void WhenAUserClicksOnTheRemoveItemButtonForTheLowestPriceItem()
        {
            cartpage.RemoveLowestPriceItem();
        }
        
        [Then(@"a user is able to get a total of (.*) items")]
        public void ThenAUserIsAbleToGetATotalOfItems(int expectedNumberOfItemsInCart)
        {
            int actualNumberOfItemsInCart = cartpage.CountItemsInCart();
            Assert.IsTrue(expectedNumberOfItemsInCart.Equals(actualNumberOfItemsInCart));
        }

        [AfterScenario]
        public void DiscardBrowser()
        {
            context.CloseKatalonDemoApplication();
        }
    }
}
