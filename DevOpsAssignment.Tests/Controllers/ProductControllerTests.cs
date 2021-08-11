using DevOpsAssignment.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DevOpsAssignment.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void Get_Message()
        {
            var controller = new ProductController(new Mock<ILogger<ProductController>>().Object);
            var result = controller.GetMessage();
            Assert.IsInstanceOfType(result, typeof(string));
        }

        [TestMethod]
        public void Get_ListOfProduct()
        {
            var controller = new ProductController(new Mock<ILogger<ProductController>>().Object);
            var result = controller.GetListOfProduct();
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Product>));
        }
    }
}
