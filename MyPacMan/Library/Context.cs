using Library.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MyContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
       


        public MyContext()
        {
        }

        public MyContext(string connection) : base(connection)
        {
            Database.SetInitializer(new DbInitializer());
        }
        static MyContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
    }
    /// <summary>
    /// Database initializer
    /// </summary>
    class DbInitializer : CreateDatabaseIfNotExists<MyContext>
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="db">Database Context</param>
        protected override void Seed(MyContext db)
        {

            db.Players.Add(new Player {  Name="Anna",  Record=100});
           
            db.SaveChanges();
            base.Seed(db);
        }
    }
}
