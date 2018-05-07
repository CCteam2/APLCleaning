using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cleaningsupplies.Web;
using Cleaningsupplies.Web.Controllers;
using Cleaningsupplies.Web.Models;
using Cleaningsupplies.Business.Classes;

namespace Cleaningsupplies.Web.Tests.Controllers
{
    
    [TestClass]
    public class UsageControllerTest
    {

        [TestMethod]
        public void Index()
        {
            // Arrange
            UsageController controller = new UsageController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void JsonUpdate()
        {
            // Arrange
            UsageController controller = new UsageController();

            UsageVM vm = new UsageVM
            {
                ID = 2,
                Description = "Hand Sanitizer",
                QuantityInStock = QueryMethods.GetProdInvSum(2),
                Quantity_modified = 0
            };


            // Act
            JsonResult result = controller.JsonUpdate(vm) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }

   
}
