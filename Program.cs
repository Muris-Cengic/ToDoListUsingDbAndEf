using System;
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
                Console.WriteLine("3. Add a task. ");
                Console.WriteLine("4. Show Task. ");
                Console.WriteLine("5. Remove Task. ");
                Console.WriteLine("Enter 0 to exit.");
                string option = Console.ReadLine();
                Console.Clear();
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

                        ShowCategories();
                        break;

                    case "3":
                        {
                            Console.WriteLine("Enter task title: ");
                            string taskTitle = Console.ReadLine();

                            ShowCategories();

                            Console.WriteLine("\nChoose Catetgory ID: ");
                            int categoryID = int.Parse(Console.ReadLine());

                            Task task = new Task();
                            task.Title = taskTitle;
                            task.DateCreated = DateTime.Now;
                            task.IsCompleted = false;
                            task.CategoryID = categoryID;

                            using (var db = new todolistEntities())
                            {
                                db.AddToTasks(task);
                                db.SaveChanges();
                            }
                        }
                        break;

                    case "0":
                        {
                            continueProgram = false;
                        }
                        break;
                    case "4":
                        {
                            ShowCategories();
                            Console.WriteLine("\nChoose Catetgory ID: ");
                            int cID = int.Parse(Console.ReadLine());

                            using (var db = new todolistEntities())
                            {
                                var tasks = db.Tasks.Where(x=>x.CategoryID == cID);

                                foreach (Task t in tasks)
                                {
                                        Console.WriteLine($"{t.Title}");
                                }
                            }
                        }
                        break;

                    case "5":
                        {
                            ShowCategories();
                            Console.WriteLine("\nChoose a category ID for the task you want to delete: ");
                            int cID = int.Parse(Console.ReadLine());

                            using (var db = new todolistEntities())
                            {
                                /* var tasks = 
                                    from t in db.Tasks
                                    where t.CategoryID == cID
                                    select t; */
                                var tasks = db.Tasks.Where(t => t.CategoryID == cID).ToList();

                                Console.Clear();

                                foreach (Task t in tasks)
                                {
                                    Console.WriteLine($"{t.TaskID}  {t.Title}");
                                }
                                
                                Console.WriteLine("\nEnter task ID to delete: ");
                                int tID = int.Parse(Console.ReadLine());
                                Console.Clear();

                                Task task = db.Tasks.First(t => t.TaskID == tID);
                                db.Tasks.DeleteObject(task);
                                Console.WriteLine("Task deleted. ");
                                db.SaveChanges();
                            }
                        }
                        break;
                }
            } while (continueProgram);
        }
        static void ShowCategories()
        {
            using (var db = new todolistEntities())
            {
                var categories = db.Categories;
                foreach (Category category in categories)
                {
                    Console.WriteLine($"{category.CategoryID}. \t{category.CategoryName}");
                }
            }
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
