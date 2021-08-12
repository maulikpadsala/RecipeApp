using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecipeAppTest
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestMethod1()
        {

            RecipeApp.Controllers.HomeController homeController = new RecipeApp.Controllers.HomeController();
            homeController.Index();

            homeController.UpdateRecipe(10);

            homeController.DeleteRecipe(10);
        }
    }
}
