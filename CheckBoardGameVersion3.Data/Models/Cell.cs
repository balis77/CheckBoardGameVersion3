using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Models
{
    public class Cell
    {
       
        public byte X { get; set; }
        public byte Y { get; set; }
        public Checker? Checker { get; set; }
        public bool CanMove { get; set; } = false;
        public bool CanAttack { get; set; } = false;
        public bool ClickChecker { get; set; } = false;
        public Cell(byte x,byte y,Checker checker,bool canMove,bool canAttack,bool clickChecker)
        {
            X = x;
            Y = y;
            Checker = checker;
            CanMove = canMove;
            CanAttack = canAttack;
            ClickChecker = clickChecker;
        }
        public Cell()
        {

        }
        
    }
}
