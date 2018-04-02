using Library.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    /// <summary>
    /// Implementing of Unit of repositories
    /// </summary>
    public class UnitOfRepository : IUnitOfRepository
    {
        MyContext db;
        private PlayerRepository playerRepos;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger updateLogger = LogManager.GetLogger("UpdateLogger");
        /// <summary>
        /// Initialize a new instance of <see cref="UnitOfRepository" /> class
        /// </summary>
        /// <param name="connectionstring"></param>
        public UnitOfRepository(string connectionstring)
        {
            db = new MyContext(connectionstring);

        }

        public IPlayerRepository PlayerRep => playerRepos ?? new PlayerRepository(db);

        
        /// <summary>
        /// Saves all changes
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
            logger.Info("Save database changes ");
        }
       
        private bool disposed = false;
        /// <summary>
        /// Disposing resources
        /// </summary>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {

                db.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
