using System;
using System.Collections.Generic;
using System.Linq;
using ReactWebApp.Models;

namespace ReactWebApp.Logic
{
    public static class AlgorithmTester
    {
        /// The Ask: Create an application that evaluates a restocking algorithm against
        /// Refrostly’s actual order history from the past year.By keeping track of when Refrostly sells its
        /// inventory (customer order events), and when it would restock its warehouse with new inventory
        /// (inventory restocking events), this application will track a running inventory of all of Refrostly’s
        /// products.
        public static ResultValue VerifyOrdersAreStocked(IReadOnlyCollection<Order> orders,
            IReadOnlyCollection<Restock> restocks)
        {
            Dictionary<string, Order> lastOrderedItems = new Dictionary<string, Order>();

            // TODO: 
            Dictionary<string, int> inventory = new Dictionary<string, int>();

            List<Order> orderedOrders = orders.OrderBy(x => x.OrderDate).ToList();
            List<Restock> orderedRestocks = restocks.OrderBy(x => x.DateOfRestock).ToList();

            // Walk the orders and get restock events that occurred before the order to fill the inventory
            foreach (Order order in orderedOrders)
            {
                List<Restock> restockEvents =
                    orderedRestocks.TakeWhile(x => x.DateOfRestock < order.OrderDate).ToList();
                foreach (Restock restockEvent in restockEvents)
                {
                    RestockItem(inventory, restockEvent);
                    orderedRestocks.Remove(restockEvent);
                }

                if (!OrderItem(inventory, order, lastOrderedItems))
                {
                    return new ResultValue()
                    {
                        IsSuccess = false,
                        Inventory = inventory,
                        FailingOrder =
                            inventory.Any(x => x.Key == order.ItemOrdered) && inventory[order.ItemOrdered] == 0
                                ? lastOrderedItems[order.ItemOrdered]
                                : order
                    };
                }
            }

            // complete the inventory if any remains. 
            if (orderedRestocks.Any())
            {
                foreach (Restock restockEvent in orderedRestocks)
                {
                    RestockItem(inventory, restockEvent);
                }
            }

            return new ResultValue()
            {
                IsSuccess = true,
                Inventory = inventory,
                FailingOrder = null
            };
        }

        private static void RestockItem(Dictionary<string, int> inventory, Restock restockEvent)
        {
            if (inventory.ContainsKey(restockEvent.ItemStocked))
            {
                inventory[restockEvent.ItemStocked] += restockEvent.ItemQuantity;
            }
            else
            {
                Queue<Restock> tempStock = new Queue<Restock>();
                tempStock.Enqueue(restockEvent);
                inventory.Add(restockEvent.ItemStocked, restockEvent.ItemQuantity);
            }
        }

        private static bool OrderItem(Dictionary<string, int> inventory, Order orderEvent,
            Dictionary<string, Order> lastOrderedItems)
        {
            if (inventory.TryGetValue(orderEvent.ItemOrdered, out int quantity))
            {
                if (quantity >= orderEvent.ItemQuantity)
                {
                    inventory[orderEvent.ItemOrdered] -= orderEvent.ItemQuantity;

                    UpdateLastOrderedItem(lastOrderedItems, orderEvent);
                    return true;
                }
            }

            return false;
        }

        private static void UpdateLastOrderedItem(Dictionary<string, Order> lastOrderedItems, Order orderEvent)
        {
            if (lastOrderedItems.ContainsKey(orderEvent.ItemOrdered))
            {
                lastOrderedItems[orderEvent.ItemOrdered] = orderEvent;
            }
            else
            {
                lastOrderedItems.Add(orderEvent.ItemOrdered, orderEvent);
            }
        }
    }
}