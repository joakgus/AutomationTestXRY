using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace AutomationTestXRY
{
    [TestClass]
    public class Tests:XRYSession
    {
        private static WindowsDriver<WindowsElement> secondarySession;

        [TestMethod]
        public void LicenseTest()
        {
            // Test if the license doesn't exist.
            session.FindElementByName("Extract").Click();
            Assert.IsTrue(session.FindElementByName("No valid license found").Enabled);
            session.FindElementByName("BACK").Click();
            session.FindElementByName("MENU").Click();
            Assert.IsTrue(session.FindElementByName("PROCESS OPTIONS").Enabled);
            session.FindElementByName("MANAGE LICENSES").Click();
            Thread.Sleep(TimeSpan.FromSeconds(3));

            secondarySession = Utility.CreateNewSession(CommonTestSettings.Licesnse);
            var existingApplicationTopLevelWindow = secondarySession.CurrentWindowHandle;

            // Create a new session by attaching to an existing application top level window
            AppiumOptions opt = new AppiumOptions();
            opt.AddAdditionalCapability("appTopLevelWindow", existingApplicationTopLevelWindow);
            secondarySession = new WindowsDriver<WindowsElement>(new Uri(CommonTestSettings.WindowsApplicationDriverUrl), opt);

            secondarySession.FindElementByName("Add license").Click();

            var currentWindowHandle = secondarySession.CurrentWindowHandle;
            Thread.Sleep(TimeSpan.FromSeconds(3));
            var allWindowHandles2 = secondarySession.WindowHandles;
            secondarySession.SwitchTo().Window(allWindowHandles2[0]);

            secondarySession.Keyboard.SendKeys(License.p1);
            secondarySession.Keyboard.SendKeys(Keys.Tab);
            secondarySession.Keyboard.SendKeys(License.p2);
            secondarySession.Keyboard.SendKeys(Keys.Tab);
            secondarySession.Keyboard.SendKeys(License.p3);
            secondarySession.Keyboard.SendKeys(Keys.Tab);
            secondarySession.Keyboard.SendKeys(License.p4);
            secondarySession.FindElementByName("Activate online").Click();
            secondarySession.FindElementByName("Activate").Click();
            secondarySession.FindElementByName("OK").Click();
            secondarySession.SwitchTo().Window(allWindowHandles2[1]);
            secondarySession.FindElementByName("Close").Click();
            session.FindElementByName("HOME").Click();
        }

        [TestMethod]
        public void OpenFileTest()
        {
            session.FindElementByName("Open...").Click();
            session.FindElementByName("2019-05-07_15.49.xrycase").Click();
            session.FindElementByName("Open").Click();
            Assert.IsTrue(session.FindElementByName("2019-05-07_15.49").Displayed);
            session.FindElementByClassName("MenuItem").Click();
            session.FindElementByName("Edit file data").Click();
            session.FindElementByName("CLOSE").Click();
            session.FindElementByName("HOME").Click();
        }

        [TestMethod]
        public void OptionsTest()
        {
            session.FindElementByName("MENU").Click();
            session.FindElementByName("OPTIONS").Click();
            session.FindElementByName("Organization").Click();
            session.Keyboard.SendKeys("Full Auto");
            session.FindElementByName("Save").Click();
            session.FindElementByName("OPTIONS").Click();
            session.FindElementByName("Organization").Click();
            session.FindElementByName("Save").Click();
            session.FindElementByName("HOME").Click();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch a Calculator window
            Setup(context);

        }

        /*[ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }*/

        /* [TestInitialize]
         public void Clear()
         {
             session.FindElementByName("Clear").Click();
             Assert.AreEqual("0", GetCalculatorResultText());
         }
         /*
         private string GetCalculatorResultText()
         {
             return calculatorResult.Text.Replace("Display is", string.Empty).Trim();
         }*/
    }
}