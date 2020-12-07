namespace Orders.Migrations
{
    using Orders.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Orders.DAL.OrdersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Orders.DAL.OrdersContext context)
        {
            var providers = new List<Provider>
            {
                new Provider{ Name = "ООО ЗМК \"Элемент\"" },
                new Provider{ Name = "ООО «Металлопрокатный завод»" },
                new Provider{ Name = "ООО “КоксоХимМонтаж-Волга”" },
                new Provider{ Name = "ОА “РусТехноГрупп”" },
                new Provider{ Name = "ПАО “Аэропортстрой”" }
            };
            providers.ForEach(s => context.Providers.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var orders = new List<Order>
            {
                new Order
                {
                    Number = "931240613",
                    Date = DateTime.Parse("2019-10-04"),
                    ProviderId = providers.Single(s => s.Name == "ООО ЗМК \"Элемент\"").Id
                },
                new Order
                {
                    Number = "951467835",
                    Date = DateTime.Parse("2020-04-25"),
                    ProviderId = providers.Single(s => s.Name == "ООО ЗМК \"Элемент\"").Id
                },
                new Order
                {
                    Number = "953741689",
                    Date = DateTime.Parse("2020-05-01"),
                    ProviderId = providers.Single(s => s.Name == "ОА “РусТехноГрупп”").Id
                },
                new Order
                {
                    Number = "953741765",
                    Date = DateTime.Parse("2020-05-20"),
                    ProviderId = providers.Single(s => s.Name == "ПАО “Аэропортстрой”").Id
                }
            };
            orders.ForEach(s => context.Orders.AddOrUpdate(p => p.Number, s));
            context.SaveChanges();

            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "931240613").Id,
                    Name = "Трубы",
                    Quantity = 10,
                    Unit = "тн."
                },
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "931240613").Id,
                    Name = "Фермы",
                    Quantity = 20,
                    Unit = "тн."
                },
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "931240613").Id,
                    Name = "Колонны",
                    Quantity = 15,
                    Unit = "тн."
                },
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "951467835").Id,
                    Name = "Сварные балки",
                    Quantity = 1
                },
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "951467835").Id,
                    Name = "Лестницы",
                    Quantity = 4
                },
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "953741689").Id,
                    Name = "Фермы",
                    Quantity = 20,
                    Unit = "тн."
                },
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "953741689").Id,
                    Name = "Колонны",
                    Quantity = 5,
                    Unit = "тн."
                },
                new OrderItem
                {
                    OrderId = orders.Single(s => s.Number ==  "953741765").Id,
                    Name = "Caterpillar D6 2012",
                    Quantity = 2
                }
            };
            orderItems.ForEach(s => context.OrderItems.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }
    }
}
