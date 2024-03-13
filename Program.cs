﻿using System;
using System.Linq;

namespace ToDoListUsingDbAndEf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            initializeDatabaseData();
            bool continueProgram = true;
            do
            {
                Console.WriteLine("1: Add a cateogry. ");
                Console.WriteLine("2. Show exisiting categories. ");
                Console.WriteLine("Enter 0 to exit.");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        {
                            Console.WriteLine("Enter category name: ");
                            string categoryName = Console.ReadLine();
                            Category category = new Category();
                            category.CategoryName = categoryName;
                            using (var datab = new todolistEntities())
                            {
                                datab.Categories.AddObject(category);
                                datab.SaveChanges();
                            }
                        }
                        break;

                    case "2":
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("\nList of Catetgories: ");
                        using (var db = new todolistEntities())
                        {
                            var categories = db.Categories;
                            foreach (Category category in categories)
                            {
                                Console.WriteLine($"{category.CategoryID}. \t{category.CategoryName}");
                            }
                        }
                        break;

                    case "0":
                        {
                            continueProgram = false;
                        }
                        break;
                }
            } while (continueProgram);
        }

        private static void initializeDatabaseData()
        {
            // Category Midterm
            Category c1 = new Category()
            {
                CategoryID = 1,
                CategoryName = "Midterm",
            };
            Task t1 = new Task()
            {
                TaskID = 1,
                Category = c1,
                Title = "Prepare notes for OOP",
                DateCreated = DateTime.Now,
            };

            // Category E Skills
            Category c2 = new Category()
            {
                CategoryID = 2,
                CategoryName = "E Skills 24"
            };

            Task t2 = new Task()
            {
                TaskID = 2,
                Category = c2,
                Title = "Wath a video about Entity Framework",
                DateCreated = DateTime.Now,
            };
            Task t3 = new Task()
            {
                TaskID = 3,
                Category = c2,
                Title = "Wath a video about LINQ",
                DateCreated = DateTime.Now,
            };

            // Add records to database if database is empty
            using (var db = new todolistEntities())
            {
                if (!db.Categories.Any())
                {
                    db.Categories.AddObject(c1);
                    db.Categories.AddObject(c2);
                }

                if (!db.Tasks.Any())
                {
                    db.Tasks.AddObject(t1);
                    db.Tasks.AddObject(t2);
                    db.Tasks.AddObject(t3);
                }

                db.SaveChanges();
            }
        }
    }
}
