using System;
using System.Linq;

namespace ToDoListUsingDbAndEf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            initializeDatabaseData();

            using (var db = new todolistEntities())
            {
                foreach (var category in db.Categories)
                {
                    Console.WriteLine("========================================================");
                    Console.WriteLine($"Category: {category.CategoryName}");
                    Console.WriteLine("Tasks:");

                    foreach (var task in category.Tasks)
                    {
                        Console.WriteLine($"      {task.Title} - {task.DateCreated}");
                    }
                }    
            }
            Console.ReadKey();
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
