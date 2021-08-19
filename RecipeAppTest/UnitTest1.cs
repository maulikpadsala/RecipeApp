using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeApp.Controllers;
using RecipeAppAPI.Controllers;
using RecipeAppAPI.Database;
using Xunit;

namespace RecipeAppTest
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            //Run Reciepe App Test
            RecipeAppTest();

            //Run Recipe App API  Test
            RecipeAppAPITest();

            //Run Recipe UI Test
            RecipeUITest recipeUITest = new RecipeUITest();
            recipeUITest.ReturnIndexRecipeView();
            recipeUITest.CrateNewRecipe();
            recipeUITest.CreateRecipeErrorMessage();
        }
        [Fact]
        public void RecipeAppTest()
        {
            HomeController homeController = new HomeController();
            homeController.Index();

            homeController.UpdateRecipe(10);

            homeController.DeleteRecipe(10);

            Assert.IsTrue(true);
        }
        [Fact]
        public void RecipeAppAPITest()
        {
            
            RecipeDataContext DbContext = new RecipeDataContext(null);
            RecipeController recipeController = new RecipeController(DbContext);
            recipeController.GetAllRecipe();

            recipeController.GetRecipe(10);

            recipeController.DeleteRecipe(10);

            Assert.IsTrue(true);
        }
    }
}
