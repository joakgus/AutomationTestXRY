using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Configuration;
using System.Text;

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
            Assert.IsTrue(session.FindElementByClassName("LicenseCheck").Enabled);
            session.FindElementByName("BACK").Click();
            session.FindElementByName("MENU").Click();
            session.FindElementByName("MANAGE LICENSES").Click();

            secondarySession = Utility.CreateNewSession(CommonTestSettings.Licesnse);
            var existingApplicationTopLevelWindow = secondarySession.CurrentWindowHandle;

            // Create a new session by attaching to an existing application top level window
            AppiumOptions opt = new AppiumOptions();
            opt.AddAdditionalCapability("appTopLevelWindow", existingApplicationTopLevelWindow);
            secondarySession = new WindowsDriver<WindowsElement>(new Uri(CommonTestSettings.WindowsApplicationDriverUrl), opt);

            secondarySession.FindElementByName("Add license").Click();
            var currentWindowHandle = secondarySession.CurrentWindowHandle;
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
            session.FindElementByClassName("UIProperty").Click();
            session.FindElementByName("Open").Click();
            session.FindElementByClassName("MenuItem").Click();
            session.FindElementByName("Edit file data").Click();
            var currentWindowHandle = session.CurrentWindowHandle;
            var allWindowHandles2 = session.WindowHandles;
            session.SwitchTo().Window(allWindowHandles2[0]);
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

        [TestMethod]
        public void Extraction()
        {
            session.FindElementByName("Extract").Click();
            session.FindElementByAccessibilityId("ToggleConnectedDevices").Click();
            session.FindElementByName("Huawei Y5 Lite CRO-L03 2017 LTE AM").Click();
            session.FindElementByName("Logical (No files)").Click();
            session.FindElementByName("Next").Click();
            session.FindElementByName("Next").Click();
            session.FindElementByAccessibilityId("OperatorTextBox").Click();
            session.Keyboard.SendKeys("Joakim");
            session.FindElementByName("Next").Click();

            Thread.Sleep(TimeSpan.FromSeconds(15));
            var currentWindowHandle = session.CurrentWindowHandle;
            var allWindowHandles2 = session.WindowHandles;
            session.SwitchTo().Window(allWindowHandles2[0]);
            session.FindElementByAccessibilityId("SaveLogButton").Click();
            session.FindElementByName("Save").Click();
            session.FindElementByName("Finish").Click();
            
            currentWindowHandle = session.CurrentWindowHandle;
            allWindowHandles2 = session.WindowHandles;
            session.SwitchTo().Window(allWindowHandles2[0]);
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