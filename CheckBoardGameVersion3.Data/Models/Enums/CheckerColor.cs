﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBoardGameVersion3.Data.Models.Enums
{
    /// <summary>
    /// нач так пищу тут потом зітру
    /// примерна логіка дошки шашка в якій находиться 
    /// 1.колір шашки enum black,white,whiteQueen,BlackQueen
    /// 2.хто ходить чорні/білі
    /// 3.булова хто ходить
    /// 4.булова хто б'є
    /// 5.булова чи може ще раз бити
    /// 6.ID яка буде реалізована через кординати доски тоїсть A1,A2,A3,A4
    /// сама шашка находиться в клетці в якої
    /// 1.ID так же як в шашки
    /// 2. кординати
    /// 
    /// 2.Шашка
    /// 
    /// Перше що ми можем зліпити це репозіторій через діктіонарі з стрігою Id,второй варіант заключається через enum 
    /// 
    /// Дальше доска варіант з циклами литить в канаву,так як,це доска яка в нас статична,щоб вона була адекватно построїна будем брати варіант з хардкодом,щоб ця срань не зломалась
    /// хмм як? да хуй знає,примерні предположенія вальнути все в дівах 
    /// <div class = "row" .. це просто отступ для як блок рядка>
    /// <div class = "cell" .. тут будем прописувати саму клітинку там 80 на 80 пікселів і т.д >
    /// </div>
    /// </div>
    /// вопрос залишається в шашці 64 іфа не адекватно,тому лібо булову переменну на contains() яку треба самому ще забацати , або ж через (@cell?.checker)
    /// 
    /// 
    /// 
    /// 
    /// 
    /// крч більшість зліплено осталась дамка і лок дошки 
    /// лок дошки приймає в себе доску Dictionary<string,Cell> вертає що?
    /// вертати боард назад з булами,то я хуй знає як тоді впаяти при кліку треба щоб шашка лочилась мб добавити бул який буде лочити шашку?
    /// окей якщо ми пишем тру лок, то воно тупо мене викидує в канаву при кліку не на ту шашку правильно? логіка вже підключення на удалення мувів,так що мб може спрацювати
    /// </summary>
    /// 

    public enum CheckerColor
    {
        Black,
        White,
        BlackKing,
        WhiteKing,
        Empty,
    }
}
