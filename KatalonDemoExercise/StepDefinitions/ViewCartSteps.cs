using System;
using TechTalk.SpecFlow;
using KatalonDemoExercise.Hooks;
using KatalonDemoExercise.Pages;
using NUnit.Framework;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using System.IO;

namespace KatalonDemoExercise.StepDefinitions
{
    [Binding]
    public class ViewCartSteps
    {
        Context context;
        Homepage homepage;
        CartPage cartpage;
        ScenarioContext scenarioContext;
        static ExtentTest feature;
        static ExtentTest scenario;
        static ExtentReports report;

        public ViewCartSteps(Context _context, Homepage _homepage, CartPage _cartpage, ScenarioContext _scenarioContext)
        {
            context = _context; homepage = _homepage; cartpage = _cartpage; scenarioContext = _scenarioContext;
        }
        [Given(@"that katalon Ecommerce website is loaded")]
        public void GivenThatKatalonEcommerceWebsiteIsLoaded()
        {
            context.LoadKatalonDemoApplication();
            scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [Given(@"I add (.*) random items to my cart")]
        public void GivenIAddRandomItemsToMyCart(int itemQuantity)
        {
            homepage.AddFourItemsToCart();
        }

        [When(@"I search for lowest price item")]
        public void WhenISearchForLowestPriceItem()
        {
            cartpage.SearchLowestPriceItem();
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            homepage.ClickCartLink();
        }

        [When(@"I am able to remove the lowest price item from my cart")]
        public void WhenIAmAbleToRemoveTheLowestPriceItemFromMyCart()
        {
            cartpage.RemoveLowestPriceItem();
        }

        [Then(@"I find total (.*) items listed in my cart")]
        [Then(@"I am able to verify (.*) items in my cart")]
        public void ThenIFindTotalItemsListedInMyCart(int expectedNumberOfItemsInCart)
        {
            int actualNumberOfItemsInCart = cartpage.CountItemsInCart();
            Assert.IsTrue(expectedNumberOfItemsInCart.Equals(actualNumberOfItemsInCart));
        }

        [BeforeTestRun]
        public static void ReportGenerator()
        {
            var testResultReport = new ExtentV3HtmlReporter(AppDomain.CurrentDomain.BaseDirectory + @"\TestResult.html");
            testResultReport.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            report = new ExtentReports();
            report.AttachReporter(testResultReport);
        }

        [AfterTestRun]
        public static void ReportCleaner()
        {
            report.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            feature = report.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterStep]
        public void StepsInTheReport()
        {
            var typeOfStep = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (scenarioContext.TestError == null)
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }

            if (scenarioContext.TestError != null)
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                }
            }
        }

        [AfterScenario]
        public void CloseTFLApplication()
        {
            try
            {
                if (scenarioContext.TestError != null)
                {
                    
                    string scenarioName = scenarioContext.ScenarioInfo.Title;
                    Directory.CreateDirectory("ReportScreenshots");
                    string directory = AppDomain.CurrentDomain.BaseDirectory + @"\ReportScreenshots\";
                    context.TakeScreenshotAtThePointOfTestFailure(directory, scenarioName);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                context.CloseKatalonDemoApplication();
            }
        }
    }
}
