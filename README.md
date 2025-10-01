# Advantage Shopping Automation Tests

This project contains automated UI tests for [Advantage Online Shopping](https://www.advantageonlineshopping.com) using:

- **C#**
- **Selenium WebDriver**
- **NUnit**
- **Page Object Model (POM)**
- **Explicit Waits (WebDriverWait)**
- **WebDriverManager** for driver management

---

## 📂 Project Structure

AdvantageShoppingTests/
│
├── Driver/                # Browser Driver
│   ├── DriverFactory.cs
├── Pages/                # Page Object classes
│   ├── HomePage.cs
│   └── RegisterPage.cs
├── TestData/                # Testdata
│   ├── TestData.json
│
├── Tests/                # NUnit test classes
│   └── CreateAccountTests.cs
│   └── BaseTest.cs
│
├── Utils/                # Helpers
│   ├── TestDataReader.cs
│   └── WaitHelper.cs
│   └── ConfigReader.cs
│   ├── TestReportManager.cs
│
└── AdvantageShoppingTests.csproj
└── appsettings.json
└── Dockerfile
└── docker-compose.yml

---

## ⚙️ Setup

1. Git Clone: git clone https://github.com/itpro21/C_NUnit_Selenium_Automation.git
2. Restore dependencies:
   ```bash
   dotnet restore
   ```

---

## ▶️ Running Tests

Run all tests:

```bash
dotnet test
```

Run Reports:

```bash
dotnet test --logger "trx;LogFileName=TestResults.trx"
```

---

## 🧪 Test Scenario Covered

**LOG001 – Mandatory Field Error Messages Display and Clear**

Steps:

1. Navigate to `https://www.advantageonlineshopping.com`
2. Open login popup
3. Click **Create New Account**
4. Focus in and out of required fields (Username, Email, Password, Confirm Password)
5. Verify Mandatory error messages display
6. Enter valid data
7. Verify errors are cleared

**LOG002 - Error Message displayed for Invalid data**

Steps:

1. Navigate to `https://www.advantageonlineshopping.com`
2. Open login popup
3. Click **Create New Account**
4. Enter invalid data
5. Verify errors are displayed

---

✅ This project is **scalable** with POM, Docker, Parallel execution with Selenium grid, supports **explicit waits**, cross browser testing using Chrome, firefox and can be extended to include other browsers and can be extended easily for new test cases
