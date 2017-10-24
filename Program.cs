using LiteDB;
using System;
using System.Linq;

namespace ConsoleApp
{
    public class Program
    {
        private const string DATABASE = "MyData.db";
        private const string COLLECTION = "sites";

        public static void Main(string[] args)
        {
            try
            {
                using(var db = new LiteDatabase(DATABASE))
                {
                    var sites = db.GetCollection<Site>(COLLECTION);

                    InitializeData();

                    PrintAll();

                    var result = sites.Find(x => x.Name == ("Microsoft")).SingleOrDefault();
                    sites.Update(new Site() { Id = 2, Name = "Google" });

                    PrintAll();
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void InitializeData()
        {
            try
            {
                using(var db = new LiteDatabase(DATABASE))
                {
                    if (db.CollectionExists(COLLECTION)) db.DropCollection(COLLECTION);
                
                    var sites = db.GetCollection<Site>(COLLECTION);

                    sites.Insert(new Site() { Id = 1, Name = "Geeks.ms"});
                    sites.Insert(new Site() { Id = 2, Name = "Microsoft"});
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PrintAll()
        {
            using(var db = new LiteDatabase(DATABASE))
            {
                var sites = db.GetCollection<Site>(COLLECTION);

                var results = sites.FindAll().ToList();

                foreach(var element in results)
                {
                    Console.WriteLine($"Id ({element.Id}) with Name {element.Name}");
                }

                Console.WriteLine();
            }
        }

    }

    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}