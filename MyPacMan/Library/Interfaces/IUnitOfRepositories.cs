using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces
{
    /// <summary>
    /// Unit of Repositories
    /// </summary>
    public interface IUnitOfRepository : IDisposable
    {
        
        IPlayerRepository PlayerRep { get; }
        void Save();
    }
}
