using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ReactWebApp.Logic;
using ReactWebApp.Models;

namespace OrderAlgorithmTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem(@"orders.json", "optionalOutFolder")]
        [DeploymentItem(@"restocks.json", "optionalOutFolder")]
        public void CanSerializeData()
        {
            string orderTestData = System.IO.File.ReadAllText(@"optionalOutFolder\orders.json");
            string restocksTestData = System.IO.File.ReadAllText(@"optionalOutFolder\restocks.json");

            List<Order> deserializedOrders = JsonConvert.DeserializeObject<List<Order>>(orderTestData);
            List<Restock> deserializedResotcks = JsonConvert.DeserializeObject<List<Restock>>(restocksTestData);

            Assert.IsTrue(deserializedOrders.Any());
            Assert.IsTrue(deserializedResotcks.Any());
        }

        [TestMethod]
        [DeploymentItem(@"orders.json", "optionalOutFolder")]
        [DeploymentItem(@"restocks.json", "optionalOutFolder")]
        public void AssertTestCaseProvided()
        {
            string orderTestData = System.IO.File.ReadAllText(@"optionalOutFolder\orders.json");
            string restocksTestData = System.IO.File.ReadAllText(@"optionalOutFolder\restocks.json");

            List<Order> deserializedOrders = JsonConvert.DeserializeObject<List<Order>>(orderTestData);
            List<Restock> deserializedResotcks = JsonConvert.DeserializeObject<List<Restock>>(restocksTestData);

            ResultValue result = AlgorithmTester.VerifyOrdersAreStocked(deserializedOrders, deserializedResotcks);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Inventory["shovel"] == 4);
            Assert.IsTrue(result.Inventory["snowblower"] == 4);
            Assert.IsTrue(result.Inventory["tires"] == 2);
            Assert.IsTrue(result.Inventory["sled"] == 1);
            Assert.IsTrue(result.Inventory["skis"] == 0); // not provided in example result
        }

        [TestMethod]
        [DeploymentItem(@"orders.json", "optionalOutFolder")]
        [DeploymentItem(@"restocks.json", "optionalOutFolder")]
        public void AssertEmptyInventoryIsSuccess()
        {
            string orderTestData = System.IO.File.ReadAllText(@"optionalOutFolder\orders.json");
            string restocksTestData = System.IO.File.ReadAllText(@"optionalOutFolder\restocks.json");

            List<Order> deserializedOrders = JsonConvert.DeserializeObject<List<Order>>(orderTestData);
            List<Restock> deserializedResotcks = JsonConvert.DeserializeObject<List<Restock>>(restocksTestData);

            deserializedOrders.AddRange(new List<Order>()
            {
                new Order()
                {
                    ItemOrdered = "shovel",
                    ItemQuantity = 4,
                    OrderDate = DateTime.Now
                },
                new Order()
                {
                    ItemOrdered = "snowblower",
                    ItemQuantity = 4,
                    OrderDate = DateTime.Now
                },
                new Order()
                {
                    ItemOrdered = "tires",
                    ItemQuantity = 2,
                    OrderDate = DateTime.Now
                },
                new Order()
                {
                    ItemOrdered = "sled",
                    ItemQuantity = 1,
                    OrderDate = DateTime.Now
                }
            });

            ResultValue result = AlgorithmTester.VerifyOrdersAreStocked(deserializedOrders, deserializedResotcks);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Inventory["shovel"] == 0);
            Assert.IsTrue(result.Inventory["snowblower"] == 0);
            Assert.IsTrue(result.Inventory["tires"] == 0);
            Assert.IsTrue(result.Inventory["sled"] == 0);
            Assert.IsTrue(result.Inventory["skis"] == 0);
        }

        [TestMethod]
        public void AssertLastOrderFailed()
        {
            Order previousOrderThatExaustedInventory = new Order
            {
                ItemOrdered = "testItem",
                ItemQuantity = 1,
                OrderDate = DateTime.Now.AddDays(-1)
            };

            Order latestOrder = new Order
            {
                ItemOrdered = "testItem",
                ItemQuantity = 1,
                OrderDate = DateTime.Now
            };

            List<Order> orders = new List<Order>
            {
                previousOrderThatExaustedInventory,
                latestOrder
            };

            List<Restock> restock = new List<Restock>
            {
                new Restock
                {
                    ItemStocked = "testItem",
                    ItemQuantity = 1,
                    DateOfRestock = DateTime.Now.AddDays(-2)
                }
            };

            ResultValue result = AlgorithmTester.VerifyOrdersAreStocked(orders, restock);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(result.FailingOrder, previousOrderThatExaustedInventory);

        }

        [TestMethod]
        public void AssertFailiingOrderIsOverStock()
        {
            int itemQuantityInStock = 1;
            Order failingOrder = new Order
            {
                ItemOrdered = "testItem",
                ItemQuantity = itemQuantityInStock + 1,
                OrderDate = DateTime.Now
            };

            List<Order> orders = new List<Order>
            {
                failingOrder
            };

            List<Restock> restock = new List<Restock>
            {
                new Restock
                {
                    ItemStocked = "testItem",
                    ItemQuantity = itemQuantityInStock,
                    DateOfRestock = DateTime.Now.AddDays(-1)
                }
            };

            ResultValue result = AlgorithmTester.VerifyOrdersAreStocked(orders, restock);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(result.FailingOrder, failingOrder);

        }

        [TestMethod]
        public void AssertFailiingOrderWithEmptyInventory()
        {
            int itemQuantityInStock = 1;
            Order failingOrder = new Order
            {
                ItemOrdered = "testItem",
                ItemQuantity = itemQuantityInStock + 1,
                OrderDate = DateTime.Now
            };

            List<Order> orders = new List<Order>
            {
                failingOrder
            };

            List<Restock> restock = new List<Restock>();

            ResultValue result = AlgorithmTester.VerifyOrdersAreStocked(orders, restock);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(result.FailingOrder, failingOrder);

        }
    }
}
