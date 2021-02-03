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

## Good Practices

It is important to put some standards in place to reduce the maintenance of the overall test classes.

### Naming Convention

Prefix name of web elements with something which describes type of web elements whether it is input or button or something else. In addition, web element name should be given as it is shown on UI. E.g. `btnNext`, `lnkFooter`, etc.

Add postfix "Page" with all pages you develop. It will segregate page classes from other available classes. E.g. `LoginPage`, `HomePage`, etc.

Use PascalCasing for naming classes and methods, follow common capitalization conventions appropriate to developing in C#: [Capitalization Conventions](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/capitalization-conventions)

```
// Good
public class HomePage

// Bad
public class homepage
public class homePage
```

Use camelCasing for local variables and parameters (the first word starts with lower case, following words with upper case).

Don't use abbreviations – there is no limit on the number of characters a class, method, or variable name can have, so avoid creating confusion or inconsistencies by using abbreviations.

Use nouns for class names, and verbs for methods.

Avoid using the underscore character (the only exception are private variables):

```
// Good
private WebDriverWait _wait;
var homePage = new HomePage(Driver);

// Bad
var another_page = homePage.GoToAnotherPage();
```

Include the word "Tests" at the end of your test classes (e.g. `FacultyTests`).

### Independent Tests

This is a very important aspect of a good and stable automation framework. If you want your tests to be reliable, avoid making one test depend on another i.e. create the test data for TestCase2 in TestCase1. This may cause unexpected test failures, which can consume valuable time spent debugging and fixing, without actually uncovering any issues in the application under test. Also, if you want to consider test parallelization, you can't control the order in which the tests run, which means that the dependent test might run before the test it depends on.

### Locator Strategy

An important part of UI automation is using the right locators. The locator strategies available in Selenium C# are:
- Id
- Name
- LinkText
- PartialLinkText
- TagName
- ClassName
- CssSelector
- XPath

When available and unique, the most recommended locator strategy is the `ID`. It's the fastest locator, and it's also easy to use. `ClassName` is also a good locator strategy, but it also has to be unique, which you probably noticed is not always the case.

When `ID` and `ClassName` are not available, the following preferred locator is the `CssSelector`, which is a pattern that uses attributes and their values.

After `CssSelector`, comes the `XPath`, which locates the elements in the DOM structure. The `LinkText` and `PartialLinkText` are available only when the element you need to locate is a link, otherwise they cannot be used. `Name` and `TagName` should be used only if they are unique, or when you need to find multiple elements with the same name, or tag name, using the `FindElements` method.

### Use Waits

#### Avoid using Thread.Sleep

`Thread.Sleep` will make the test wait the amount of time passed as a parameter, statically. For example, if you set the sleep time to 5 seconds, and then try to find an element, you have to wait the whole 5 seconds before the test continues, even if it takes only 3 seconds to load the element. This adds unnecessary delays to your code. Two seconds may not seem like a lot, but if you use this in hundreds of tests, they amount to a few minutes.

A good practice is to use Selenium `WebDriver`'s wait methods instead.

#### Wait methods

The explicit wait delays the execution for the specified amount of time until a certain condition is met. However, unlike the Sleep, if, for instance, the element is found before the time elapses, the test will continue normally. It applies only to a specific element.

Here's a code sample of an explicit wait that waits for an element to be clickable:

```
_wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("List View"))).Click();
```

### Limitations

Keep assertions out of the page object classes. If you want to include a validation inside the test class, make it return a boolean value. This way, you can use it to validate both positive and negative scenarios.

Include Selenium methods in your page object classes, not your test methods. Each web element can be declared as a private variable inside its class, and the methods should be public so they can be accessed from the test classes.

## Tools

This is a list of tools being used:
- NUnit
- Selenium.WebDriver
- Selenium.WebDriver.ChromeDriver
- Selenium.WebDriver.GeckoDriver
- Selenium.WebDriver.IEDriver
- Microsoft.Edge.SeleniumTools (adding coverage for Microsoft Edge Chromium)
- DotNetSeleniumExtras.PageObjects.Core (the replacement of the `PageFactory` class, which is now deprecated in the Selenium binaries)