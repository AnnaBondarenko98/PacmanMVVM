using Library.Exceptions;
using Library.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    /// <summary>
    /// Inmlementing of common repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
  class MajorRepository<T> : IRepository<T>
        where T : class
    {
       
        MyContext db;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger updateLogger = LogManager.GetLogger("UpdateLogger");

        /// <summary>
        /// Initialize a new instance of <see cref="T:Library.Repositories.MajorRepository" /> class
        /// </summary>
        /// <param name="_context"></param>
        public MajorRepository(MyContext _context)
        {
            this.db = _context;
            
        }
        /// <summary>
        /// Creates new Player in Database
        /// </summary>
        /// <param name="item"></param>
        public void Create(T item)
        {
            try
            {

                db.Set<T>().Add(item);
                db.SaveChanges();
               logger.Info("New Player was created");

            }
            catch
            {
                throw new RepositoryException("Cannot create new db-object", "MajorRepository.Create");
            }
        }
        /// <summary>
        /// Deletes Player from database by id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            try
            {
                T item = db.Set<T>().Find(id);
                if (item != null)
                {
                    db.Set<T>().Remove(item);
                    db.SaveChanges();
                }
                logger.Info("The Player was deleted");
            }
            catch
            {
                throw new RepositoryException("Cannot delete a record", "MajorRepository.Delete");

            }

        }
        /// <summary>
        /// Gets player from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            try
            {
                logger.Info("Try find the Player by id");
                return db.Set<T>().Find(id);
               
            }
            catch
            {
                throw new RepositoryException("Cannot get a record", "MajorRepository.Get");
            }
            

        }
        /// <summary>
        /// Gets all players from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            try
            {
               logger.Info("Try find all Players by id");
                return db.Set<T>().ToList();
                
            }
            catch
            {
                throw new RepositoryException("Cannot get a records", "MajorRepository.GetAll");

            }
        }
        /// <summary>
        /// Updates the Player by instance
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item)
        {
            try
            {
                 updateLogger.Info($"Try to update the Player");
                db.Entry(item).State = EntityState.Modified;

                db.SaveChanges();
                updateLogger.Info($"Player was updated");

            }
            catch
            {
                throw new RepositoryException("Cannot update a record", "MajorRepository.Update");

            }
        }
        /// <summary>
        /// Finds Player by some predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> FindByPred(Func<T, bool> predicate)
        {
            try
            {
               logger.Info($"Try to find player  by predicate.");
                return db.Set<T>().Where(predicate).ToList();
                
            }
            catch
            {
                throw new RepositoryException("Cannot find a record by predicate", "MajorRepository.FindByName");

            }
        }


    }
}
