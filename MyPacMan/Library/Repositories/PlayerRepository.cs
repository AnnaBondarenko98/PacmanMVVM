using Library.Interfaces;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{/// <summary>
/// Implementing of base and Customer repository
/// </summary>
    class PlayerRepository : MajorRepository<Player>, IPlayerRepository
    {
        MyContext db;
        public PlayerRepository(MyContext _context) : base(_context)
        {
            db = _context;
        }
       

    }
}
