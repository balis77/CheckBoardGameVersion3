﻿using CheckBoardGameVersion3.Data.Models;
using CheckBoardGameVersion3.Data.Models.Enums;
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
    public sealed class RepositoryBoard : IRepositoryBoard
    {

        public Dictionary<string, Cell>? Board { get; set; } = new Dictionary<string, Cell>();

        public Dictionary<string, Cell> CreateDesk()
        {
            Board = new Dictionary<string, Cell>();

            for (int i = 0; i < 8; i++)
            {
                string _nameMarkup = markup.MarkupName[i];
                for (int j = 0; j < 8; j++)
                {
                    int _numberMarkup = markup.MarkupNumber[j];
                    Board.Add(_nameMarkup + _numberMarkup, new Cell 
                    {
                       
                        X = i,
                        Y = j,
                    });
                }
            }
            CreateCheckers();
            return Board;
        }
        public Cell GetCell(string Id)
        {
            Cell? cell = null;
            if (Board != null)
            {
                cell = Board[Id];
            }
              
            
            
            return cell;
        }
        #region PrivateMethod
        private void CreateCheckers()
        {
            BlackChecker();
            WhiteChecker();
        }

        private void BlackChecker()
        {
            for (int i = 0; i < 3; i++)
            {
                string _nameMarkup = markup.MarkupName[i];
                for (int j = (i + 1) % 2; j < 8; j += 2)
                {
                    int _numberMarkup = markup.MarkupNumber[j];
                    string cordinateInTheDesk = _nameMarkup + _numberMarkup;

                    Board[cordinateInTheDesk].Checker = new Checker
                    {
                        InCellId = cordinateInTheDesk,
                        Color = CheckerColor.Black,
                        Team = SetTeam.Black
                        
                    };
                }
            }
        }

        private void WhiteChecker()
        {
            for (int i = 5; i < 8; i++)
            {
                string _nameMarkup = markup.MarkupName[i];
                for (int j = (i + 1) % 2; j < 8; j += 2)
                {
                    int _numberMarkup = markup.MarkupNumber[j];
                    string cordinateInTheDesk = _nameMarkup + _numberMarkup;

                    Board[cordinateInTheDesk].Checker = new Checker
                    {
                        InCellId = cordinateInTheDesk,
                        Color = CheckerColor.White,
                        Team = SetTeam.White

                    };
                }
            } 
        }
        #endregion
    }
}