using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SalesRepDAL.Constants;
using SalesRepDAL.Entities;
using System;
using System.Collections.Generic;

namespace SalesRepDAL.Seeders
{
    public class SeedDataToDB
    {
        #region First part - Seed Users with full info to Database 
        public static void SeedUsersWithRoles(UserManager<DbUser> userManager,
                            RoleManager<DbRole> roleManager)
        {
            var roleResult = roleManager.FindByNameAsync(Roles.Admin).Result;
            if (roleResult == null || string.IsNullOrEmpty(roleResult.Name))
            {
                var roleresult = roleManager.CreateAsync(new DbRole
                {
                    Name = Roles.Admin
                }).Result;
            }
            roleResult = roleManager.FindByNameAsync(Roles.User).Result;
            if (roleResult == null || string.IsNullOrEmpty(roleResult.Name))
            {
                var roleresult = roleManager.CreateAsync(new DbRole
                {
                    Name = Roles.User
                }).Result;
            }

            //add Users
            var emailAdmin = "andrewletskalyuk@gmail.com";
            var findAdmin = userManager.FindByEmailAsync(emailAdmin).Result;
            if (findAdmin == null)
            {
                var admin = new DbUser
                {
                    Email = emailAdmin,
                    UserName = "Admin",
                    Age = 30,
                    PhoneNumber = "+380673840953",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(admin, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(admin, Roles.Admin).Result;
            }
            var emailUser = "andrewgopanchuk@gmail.com";
            var findUser = userManager.FindByEmailAsync(emailUser).Result;
            if (findUser == null)
            {
                var user = new DbUser
                {
                    Email = emailUser,
                    UserName = "User",
                    Age = 27,
                    PhoneNumber = "+380932291709",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, "Qwerty-1").Result;
                result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
            }
        }
        #endregion

        #region SecondPart - Seed data about Trading Company with Sales Rep 
        public static void SeedDataTradingCompany(EFContext efcontext)
        {
            TradeCompany tradeCompany = new TradeCompany
            {
                //TradeCompanyID = 1,
                Address = "Rivne city",
                Email = "rivnecity@gmail.com",
                Owner = "Oleksa Dovbush",
                Phone = "+380671112233",
                TaxSystem = "Simplified System",
                Title = "Barvinok",
                //SaleReps = saleReps
            };
            efcontext.Trades.Add(tradeCompany);
            efcontext.SaveChanges();

            SaleRep saleRep1 = new SaleRep()
            {
                //SaleRepID = 1,
                FullName = "Andrii Letskaliuk",
                Email = "andrewletskalyuk@gmail.com",
                HomeAddress = "Rivne",
                IsActive = true,
                Itinerary = "first",
                Phone = "+380673840953",
                Salary = 6500,
                TradeCompanyID = tradeCompany.TradeCompanyID
            };
            SaleRep saleRep2 = new SaleRep()
            {
                //SaleRepID = 2,
                FullName = "Sergiy Kravchuk",
                Email = "kravchuk@gmail.com",
                HomeAddress = "Lviv",
                IsActive = true,
                Itinerary = "second",
                Phone = "+380672223344",
                Salary = 4354,
                TradeCompanyID = tradeCompany.TradeCompanyID
            };
            SaleRep saleRep3 = new SaleRep()
            {
                //SaleRepID = 3,
                FullName = "Iruna Volhovec",
                Email = "volhovec@gmail.com",
                HomeAddress = "Kyiv",
                IsActive = true,
                Itinerary = "third",
                Phone = "+380679998877",
                Salary = 25352,
                TradeCompanyID = tradeCompany.TradeCompanyID
            };
            List<SaleRep> saleReps = new List<SaleRep>();
            saleReps.Add(saleRep1);
            saleReps.Add(saleRep2);
            saleReps.Add(saleRep3);

            efcontext.SaleRep.Add(saleRep1);
            efcontext.SaleRep.Add(saleRep2);
            efcontext.SaleRep.Add(saleRep3);
            efcontext.SaveChanges();

            Supplier supplier = new Supplier() { Address = "Dubno", Email = "dubno@gmail.com", IsActive = true, Phone = "+380631112233" };
            Supplier supplier1 = new Supplier() { Address = "Lviv", Email = "lviv@gmail.com", IsActive = true, Phone = "+380931112233" };
            Supplier supplier2 = new Supplier() { Address = "Kyiv", Email = "kyiv@gmail.com", IsActive = true, Phone = "+380678765321" };
            efcontext.Suppliers.Add(supplier);
            efcontext.Suppliers.Add(supplier1);
            efcontext.Suppliers.Add(supplier2);
            efcontext.SaveChanges();

            Product product1 = new Product
            {
                Title = "Oyster",
                Description = "seafood",
                Price = 10,
                QuantityInWarehouse = 100,
                TotalSum = 1000,
                SupplierID = supplier.SupplierID
            };
            Product product2 = new Product
            {
                Title = "Sturgeon",
                Description = "black caviar",
                Price = 875,
                QuantityInWarehouse = 25,
                TotalSum = 21875,
                SupplierID = supplier1.SupplierID
            };
            Product product3 = new Product
            {
                Title = "Salmon",
                Description = "fish",
                Price = 8,
                QuantityInWarehouse = 100,
                TotalSum = 800,
                SupplierID = supplier2.SupplierID
            };
            Product product4 = new Product
            {
                Title = "Apple",
                Description = "fruit",
                Price = 2,
                QuantityInWarehouse = 1000,
                TotalSum = 2000,
                SupplierID = supplier1.SupplierID
            };
            Product product5 = new Product
            {
                Title = "Blueberry",
                Description = "Berries",
                Price = 18,
                QuantityInWarehouse = 1,
                TotalSum = 18,
                SupplierID = supplier.SupplierID
            };
            efcontext.Products.Add(product1);
            efcontext.Products.Add(product2);
            efcontext.Products.Add(product3);
            efcontext.Products.Add(product4);
            efcontext.Products.Add(product5);
            efcontext.SaveChanges();
            List<Product> listProducts = new List<Product>();
            listProducts.Add(product1);
            listProducts.Add(product2);
            listProducts.Add(product3);
            listProducts.Add(product4);
            listProducts.Add(product5);
            supplier.Products = listProducts;
            supplier1.Products = listProducts;
            supplier2.Products = listProducts;
            efcontext.SaveChanges();

            Customer customer = new Customer
            {
                Title = "York",
                Address = "Gogol 1 st",
                IsActive = true,
                Phone = "+380972233555"
            };
            Customer customer1 = new Customer
            {
                Title = "Bubluk",
                Address = "Shevchenka 98 st",
                IsActive = true,
                Phone = "+380978812345"
            };
            Customer customer2 = new Customer
            {
                Title = "Morkva",
                Address = "Konovalcya 12 st",
                IsActive = true,
                Phone = "+38(097)224-25-63"
            };
            efcontext.Customers.Add(customer);
            efcontext.Customers.Add(customer2);
            efcontext.Customers.Add(customer1);
            efcontext.SaveChanges();

            TradeOrder tradeOrder = new TradeOrder
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "Ivana Franka 9 st.",
                Products = listProducts,
                SumOfOrder = 7854,
                SalesRepID = saleRep2.SaleRepID,
                CustomerID = customer.CusomerID
            };
            TradeOrder tradeOrder1 = new TradeOrder
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "Volunskoi Duvisii 92 st.",
                Products = listProducts,
                SumOfOrder = 2512,
                SalesRepID = saleRep1.SaleRepID,
                CustomerID = customer1.CusomerID
            };
            TradeOrder tradeOrder2 = new TradeOrder
            {
                CreatedDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "Gagarina 1 st.",
                Products = listProducts,
                SumOfOrder = 856,
                SalesRepID = saleRep3.SaleRepID,
                CustomerID = customer2.CusomerID
            };
            efcontext.TradeOrders.Add(tradeOrder1);
            efcontext.TradeOrders.Add(tradeOrder2);
            efcontext.TradeOrders.Add(tradeOrder);
            efcontext.SaveChanges();



            TradeCompany_Supplier tradeCompany_Supplier =
               new TradeCompany_Supplier() { SupplierID = supplier.SupplierID, TradeCompanyID = tradeCompany.TradeCompanyID };
            TradeCompany_Supplier tradeCompany_Supplier1 =
               new TradeCompany_Supplier() { SupplierID = supplier1.SupplierID, TradeCompanyID = tradeCompany.TradeCompanyID };
            efcontext.Trades_Supplier.Add(tradeCompany_Supplier1);
            efcontext.Trades_Supplier.Add(tradeCompany_Supplier);
            efcontext.SaveChanges();
        }
        #endregion

        public static void SeedData(IServiceProvider services)
        {
            //use service provider and write data to DB
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                SeedDataToDB.SeedUsersWithRoles(userManager, roleManager);
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                SeedDataToDB.SeedDataTradingCompany(context);
            }
        }
    }
}
