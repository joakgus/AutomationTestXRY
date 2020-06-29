using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace AutomationTestXRY
{
    public class XRYSession
    {
        // Note: append /wd/hub to the URL if you're directing the test at Appium
        protected static WindowsDriver<WindowsElement> session;

        public static void Setup(TestContext context)
        {
            AppiumOptions opt = new AppiumOptions();
            opt.AddAdditionalCapability("platformName", "Windows");
            opt.AddAdditionalCapability("app", "Root");
            opt.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(CommonTestSettings.WindowsApplicationDriverUrl), opt);

            WindowsElement applicationWindow = null;
            var openWindows = session.FindElementsByClassName("Window");
            foreach (var window in openWindows)
            {
                if (window.GetAttribute("Name").Equals("XRY"))
                {
                    applicationWindow = window;
                    break;
                }
            }

            // Attaching to existing Application Window
            var topLevelWindowHandle = applicationWindow.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("X");

            AppiumOptions opts = new AppiumOptions();
            opts.AddAdditionalCapability("deviceName", "WindowsPC");
            opts.AddAdditionalCapability("appTopLevelWindow", topLevelWindowHandle);
            session = new WindowsDriver<WindowsElement>(new Uri(CommonTestSettings.WindowsApplicationDriverUrl), opts);
            session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}
