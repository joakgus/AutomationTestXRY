# AutomationTestXRY
Automation Test For XRY!

So this is the C# code, project and solution for XRY test automation.
The solution uses Appium in conjuction with WinAppDriver from Microsoft to drive the XRY ui and manipulate it.

To run the use need:
* Visual studio community 2015 or later.
* Install WINAPPDRIVER from here https://github.com/microsoft/WinAppDriver/releases.
* Create you own License.cs like this:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTestXRY
{
    public class License
    {
        public static string p1 = "first 6";
        public static string p2 = "second 6";
        public static string p3 = "third 6";
        public static string p4 = "fourth 6";
    }
}

* XRY must be running before starting the script and the same goes for winappdriver.exe. 

* You must be in developer mode in windows.
