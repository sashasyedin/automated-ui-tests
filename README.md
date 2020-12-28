# Automated UI Tests

This describes the approach on how to perform UI testing in C# using Selenium framework.

## Structuring UI Tests

Though the choice of the directory structure purely depends on the test requirements, it is important that a uniform structure is followed so that there are no ambiguities in development and enhancements.

The Automated UI Tests project has the following structure:

```
Project
│    appsettings.json
│
└─── PageObjects
│   
└─── TestCases
│   
└─── Shared
│   │   Configuration.cs
│   │   Constants.cs
│   │   WebDriverFactory.cs
│   │   ...
│   │
│   └─── Contracts
│       │   ITests.cs
│       │   ...
│   │
│   └─── Models
│       │   BrowserType.cs
│       │   ...
```

As shown in the structure above, the directory Project/PageObjects contains the Page Classes/Page Objects for the different web pages. The directory Project/TestCases contains the actual test code implementation. The files present in this directory will use a test framework for automated browser testing.

`WebDriverFactory` is used as a static factory object for creating WebDriver instances. Being a base abstract class, it should be inherited by a test related class.

`ITests` defines a contract which the test script will comply with.

### Page Object Model Approach

Page Object Model is an object design pattern in Selenium. In this design pattern web pages are represented as classes, and the various elements on the page are defined as variables on the class. The tests then use the methods of this page object class whenever they need to interact with the UI of that page. The benefit is that if the UI changes for the page, the tests themselves don't need to change, only the code within the page object needs to change. Subsequently all changes to support that new UI are located in one place.

Summarizing the above, we can note the following advantages of Page Object Model:
- **Helps with easy maintenance**: POM is useful when there is a change in a UI element or there is a change in an action. An example would be if a drop down menu is changed to a radio button. In this case, POM helps to identify the page or screen to be modified. As every screen will have different C# files, this identification is necessary to make the required changes in the right files. This makes test cases easy to maintain and reduces errors.
- **Helps with reusing code**: As already mentioned, all screens are independent. By using POM, one can use the test code for any one screen, and reuse it in another test case. There is no need to rewrite code, thus saving time and effort.
- **Readability and Reliability of scripts**: When all screens have independent C# files, one can easily identify actions that will be performed on a particular screen by navigating through the C# file. If a change must be made to a certain section of code, it can be efficiently done without affecting other files.

## Naming Convention

It is important to put some standards in place to reduce the maintenance of the overall test classes.

Prefix name of web elements with something which describes type of web elements whether it is textbox or button or something else. For example:

| Category |      UI/Control type       | Prefix |     Example     |
|----------|----------------------------|--------|-----------------|
| Basic    | Button                     | btn    | btnExit         |
| Basic    | Check box                  | chk    | chkReadOnly     |
| Basic    | Combo box                  | cbo    | cboEnglish      |
| Basic    | Common dialog              | dlg    | dlgFileOpen     |
| Basic    | Date picker                | dtp    | dtpPublished    |
| Basic    | Dropdown List / Select tag | ddl    | ddlCountry      |
| Basic    | Form                       | frm    | frmEntry        |
| Basic    | Frame                      | fra    | fraLanguage     |
| Basic    | Image                      | img    | imgIcon         |
| Basic    | Label                      | lbl    | lblHelpMessage  |
| Basic    | Links/Anchor Tags          | lnk    | lnkForgotPwd    |
| Basic    | List box                   | lst    | lstPolicyCodes  |
| Basic    | Menu                       | mnu    | mnuFileOpen     |
| Basic    | Radio button / group       | rdo    | rdoGender       |
| Basic    | RichTextBox                | rtf    | rtfReport       |
| Basic    | Table                      | tbl    | tblCustomer     |
| Basic    | TabStrip                   | tab    | tabOptions      |
| Basic    | Text Area                  | txa    | txaDescription  |
| Basic    | Text box                   | txt    | txtLastName     |
| Complex  | Chevron                    | chv    | chvProtocol     |
| Complex  | Data grid                  | dgd    | dgdTitles       |
| Complex  | Data list                  | dbl    | dblPublisher    |
| Complex  | Directory list box         | dir    | dirSource       |
| Complex  | Drive list box             | drv    | drvTarget       |
| Complex  | File list box              | fil    | filSource       |
| Complex  | Panel/Fieldset             | pnl    | pnlGroup        |
| Complex  | ProgressBar                | prg    | prgLoadFile     |
| Complex  | Slider                     | sld    | sldScale        |
| Complex  | Spinner                    | spn    | spnPages        |
| Complex  | StatusBar                  | sta    | staDateTime     |
| Complex  | Timer                      | tmr    | tmrAlarm        |
| Complex  | Toolbar                    | tlb    | tlbActions      |
| Complex  | TreeView                   | tre    | treOrganization |

Add postfix "Page" with all pages you develop. It will segregate page classes from other available classes. E.g. `LoginPage`, `HomePage`, etc.

Web element name should be given as it is shown on UI. For example, if email has label as "Email Address", name this web element as `txtEmailAddress`.

Prefix action name with method which performs on web element. For example:

|            Action          |  Prefix  |        Example          |
|----------------------------|----------|-------------------------|
| Click                      | clk      | clkSigin, clkRegister   |
| Type                       | set      | setEmail, setPassword   |
| Check a check box          | chk      | chkGender               |
| Select value from drop down| select   | selectYear, selectMonth |

Use PascalCasing for naming methods, follow common capitalization conventions appropriate to developing in C#: [Capitalization Conventions](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/capitalization-conventions)

## Tools

This is a list of tools being used:
- NUnit
- Selenium.WebDriver
- Selenium.WebDriver.ChromeDriver
- Selenium.WebDriver.GeckoDriver
- Selenium.WebDriver.IEDriver
- Microsoft.Edge.SeleniumTools (adding coverage for Microsoft Edge Chromium)
- DotNetSeleniumExtras.PageObjects.Core (the replacement of the `PageFactory` class, which is now deprecated in the Selenium binaries)