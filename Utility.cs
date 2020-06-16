using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace AutomationTestXRY
{
    internal class Utility
    {
        private static WindowsDriver<WindowsElement> orphanedSession;
        private static WindowsElement orphanedElement;
        private static string orphanedWindowHandle;

        Utility()
        {
            CleanupOrphanedSession();
        }

        public static WindowsDriver<WindowsElement> CreateNewSession(string appId, string argument = null)
        {
            /*DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", appId);

            if (argument != null)
            {
                appCapabilities.SetCapability("appArguments", argument);
            }

            return new WindowsDriver<WindowsElement>(new Uri(CommonTestSettings.WindowsApplicationDriverUrl), appCapabilities);*/

            AppiumOptions opt = new AppiumOptions();
            opt.AddAdditionalCapability("app", appId);

            if (argument != null)
            {
                opt.AddAdditionalCapability("appArguments", argument);
            }
            return new WindowsDriver<WindowsElement>(new Uri(CommonTestSettings.WindowsApplicationDriverUrl), opt);
        }

        public static bool CurrentWindowIsAlive(WindowsDriver<WindowsElement> remoteSession)
        {
            bool windowIsAlive = false;

            if (remoteSession != null)
            {
                try
                {
                    windowIsAlive = !string.IsNullOrEmpty(remoteSession.CurrentWindowHandle) && remoteSession.CurrentWindowHandle != "0";
                    windowIsAlive = true;
                }
                catch { }
            }

            return windowIsAlive;
        }


        public static WindowsElement GetOrphanedElement()
        {
            // Re-initialize orphaned session and element if they are compromised
            if (orphanedSession == null || orphanedElement == null)
            {
                InitializeOrphanedSession();
            }

            return orphanedElement;
        }

        public static WindowsDriver<WindowsElement> GetOrphanedSession()
        {
            // Re-initialize orphaned session and element if they are compromised
            if (orphanedSession == null || orphanedElement == null || string.IsNullOrEmpty(orphanedWindowHandle))
            {
                InitializeOrphanedSession();
            }

            return orphanedSession;
        }

        public static string GetOrphanedWindowHandle()
        {
            // Re-initialize orphaned session and element if they are compromised
            if (orphanedSession == null || orphanedElement == null || string.IsNullOrEmpty(orphanedWindowHandle))
            {
                InitializeOrphanedSession();
            }

            return orphanedWindowHandle;
        }

        private static void CleanupOrphanedSession()
        {
            orphanedWindowHandle = null;
            orphanedElement = null;

            // Cleanup after the session if exists
            if (orphanedSession != null)
            {
                orphanedSession.Quit();
                orphanedSession = null;
            }
        }

        private static void InitializeOrphanedSession()
        {
            // Create new calculator session and close the window to get an orphaned element
            CleanupOrphanedSession();
            orphanedSession = CreateNewSession(CommonTestSettings.CalculatorAppId);
            orphanedElement = orphanedSession.FindElementByAccessibilityId("AppNameTitle");
            orphanedWindowHandle = orphanedSession.CurrentWindowHandle;
            orphanedSession.Close();
        }
    }
}
