using CheckBoardGameVersion3.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Models
{
    public class Checker
    {
        //можна і бацнути енамом як вийде в ініцилізації
        public string InCellId { get; set; }
        public CheckerColor Color { get; set; } = CheckerColor.Empty;
        
        //public bool MostBeAttaking { get; set; }
        public Checker()
        {

        }
        public Checker(string id,CheckerColor color)
        {
            Color = color;
            InCellId = id;
        }
        
    }
}
