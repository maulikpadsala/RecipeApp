using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RecipeAppTest
{
    public class RecipeUITest: IDisposable
    {
        private readonly IWebDriver driver= new ChromeDriver();
        public RecipeUITest()
        {

        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void ReturnIndexRecipeView()
        {
            driver.Navigate().GoToUrl("https://localhost:44387/Home/Index");
            Assert.Equals("All-Recipe", driver.Title);            
        }

        [Fact]
        public void CrateNewRecipe()
        {
            driver.Navigate()
                .GoToUrl("https://localhost:5001/Employees/Create");

            driver.FindElement(By.Id("RecipeName")).SendKeys("Test RecipeName");

            driver.FindElement(By.Id("Ingredients")).SendKeys("Test Ingredients");

            driver.FindElement(By.Id("Description")).SendKeys("Test Description");

            driver.FindElement(By.Id("CreateRecipe")).Click();

            Assert.Equals("Create New Recipe", driver.Title);

            Assert.IsTrue(true,"Create New Recipe");
        }

        [Fact]
        public void CreateRecipeErrorMessage()
        {
            driver.Navigate()
                .GoToUrl("https://localhost:44387/Home/CreateRecipe");

            driver.FindElement(By.Id("RecipeName")).SendKeys("Test RecipeName");

            driver.FindElement(By.Id("Ingredients")).SendKeys("Test Ingredients");

            var errorMessage = driver.FindElement(By.Id("Description-error")).Text;

            Assert.Equals("Description Error", errorMessage);
        }
    }

}
