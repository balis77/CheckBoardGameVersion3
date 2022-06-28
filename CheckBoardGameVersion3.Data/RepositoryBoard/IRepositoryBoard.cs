using CheckBoardGameVersion3.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.RepositoryBoard
{
    public interface IRepositoryBoard
    {
        Dictionary<string, Cell> CreateDesk();
    }
}
