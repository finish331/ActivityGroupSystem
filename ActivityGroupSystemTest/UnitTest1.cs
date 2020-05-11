using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityGroupSystem.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActivityGroupSystemTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            
            var controller = new HallController();

            //var result = await controller.GetAllProductsAsync() as List<Product>;
            //Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}
