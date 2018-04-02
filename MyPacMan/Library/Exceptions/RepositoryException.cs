using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    /// <summary>
    /// Class of repository exception
    /// </summary>
    public class RepositoryException : Exception
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Path of the exception
        /// </summary>
        public string ExceptionPlace { get; protected set; }
        /// <summary>
        /// Initializing exception object
        /// </summary>
        /// <param name="message">message of exception</param>
        /// <param name="place">place of exception</param>
        public RepositoryException(string message, string place) : base(message)
        {
            ExceptionPlace = place;
           logger.Error($"Exception in the place {ExceptionPlace}");
        }
    }
}
