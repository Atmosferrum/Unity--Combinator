using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Структура ПРОВЕРКИ комбинаций
/// </summary>
public struct Combinator
{
    static List<Card> newCards = new List<Card>(); // Лист Карт в виде СО

    private static string result; // Результат вычислений

    /// <summary>
    /// Список комбинаций
    /// </summary>
    enum Comb
    {
        NOTHING,
        PAIR,
        PAIRS,
        SET,
        STRAIGHT,
        HIGHSTRAIGHT,
        FLUSH,
        FULLHOUSE,
        SQUARE,
        STRAIGHTFLUSH,
        FLASHROYAL
    }

    /// <summary>
    /// ПРОВЕРКА наличия комбинаций
    /// </summary>
    public static void Check()
    {
        newCards.Clear(); // Очищаем лист Карт

        Comb combination = Comb.NOTHING; // Выставляем дефолтную комбинацию

        // Получаем данные Карт на столе
        foreach (GameObject card in GameManager.Instance.cardsOnTable)
            newCards.Add(card.GetComponent<CardDisplay>().card);


        #region Pairs & Sets     

        // Ищем пары и сеты
        var pairs = newCards.GroupBy(x => x.number)
                            .Where(g => g.Count() > 1)
                            .SelectMany(y => y)
                            .ToList();

        // Вычисляем количество пар и сетов
        var fullHouse = from dupl in pairs
                        group dupl by dupl.name into g
                        select new { name = g.Key, number = g.Count() };


        // Получаем имена и количество пар
        byte c = 0;
        string[] pairName = { "", "" };
        int[] pairQwty = { 0, 0 };

        foreach (var card in fullHouse)
        {
            pairName[c] = card.name;
            pairQwty[c] = card.number;
            c++;
        }

        // Определяем какая именно комбинация пар выпала
        if (fullHouse.Count() == 1)
        {
            if (pairQwty[0] == 2)
                combination = Comb.PAIR;
            else if (pairQwty[0] == 3)
                combination = Comb.SET;
            else if (pairQwty[0] == 4)
                combination = Comb.SQUARE;
        }
        else if (fullHouse.Count() == 2)
        {
            for (int i = 0; i < pairQwty.Length; i++)
            {
                if (pairQwty[i] > 2)
                    combination = Comb.FULLHOUSE;
                else
                    combination = Comb.PAIRS;
            }
        }

        #endregion Pairs & Sets

        #region Flush     

        // Ищем флэш
        var flush = newCards.GroupBy(x => x.suit)
                      .Where(g => g.Count() == 5)
                      .SelectMany(y => y)
                      .ToList();
        
        bool itsFlush = flush.Count == 5; // Булевая для упрощения чтения получения флэша

        // Выставляем комбинацию в случае флэша
        if (itsFlush)
            combination = Comb.STRAIGHT;

        #endregion Flush

        #region Straight     

        // Ищем стрит
        var straight = newCards.OrderByDescending(i => i.number).ToList();
        byte counter = 0;
        for (int i = 0; i < straight.Count - 1; i++)
            if (straight[i].number - straight[i + 1].number == 1)
                counter++;
        
        bool itsStraight = counter == 4; // Булевая для упрощения чтения получения стрита

        bool highStraight; // Булевая для упрощения чтения получения высокго стрита
        highStraight = straight.Any(p => p.number == 1)
                    && straight.Any(p => p.number == 10)
                    && straight.Any(p => p.number == 11)
                    && straight.Any(p => p.number == 12)
                    && straight.Any(p => p.number == 13);

        //Определяем какой флэш или стрит получен
        if (highStraight)
            if (itsFlush)
                combination = Comb.FLASHROYAL;
            else
                combination = Comb.HIGHSTRAIGHT;
        else if (itsStraight)
            if (itsFlush)
                combination = Comb.STRAIGHTFLUSH;
            else
                combination = Comb.STRAIGHT;

        #endregion Straight

        // Определяем данные результата для выведения в UI
        switch (combination)
        {
            case Comb.NOTHING:
                result = $"nothing !";
                break;
            case Comb.PAIR:
                if (pairName[0] == "Six")
                    result = $"Pair of {pairName[0]}es !";
                else
                    result = $"Pair of {pairName[0]}s !";
                break;
            case Comb.PAIRS:
                if (pairName[0] == "Six")
                    result = $"Pair of {pairName[0]}es & Pair of {pairName[1]}s !";
                else if (pairName[1] == "Six")
                    result = $"Pair of {pairName[0]}es & Pair of {pairName[1]}s !";
                else
                    result = $"Pair of {pairName[0]}s & Pair of {pairName[1]}s !";
                break;
            case Comb.SET:
                if (pairName[0] == "Six")
                    result = $"Set of {pairName[0]}es !";
                else
                    result = $"Set of {pairName[0]}s !";
                break;
            case Comb.STRAIGHT:
                result = $"Staight!";
                break;
            case Comb.HIGHSTRAIGHT:
                result = $"High Straight !";
                break;
            case Comb.FLUSH:
                result = $"Flush!";
                break;
            case Comb.FULLHOUSE:
                result = $"high Fullhouse !";
                break;
            case Comb.SQUARE:
                if (pairName[0] == "Six")
                    result = $"square of {pairName[0]}es !";
                else
                    result = $"square of {pairName[0]}s !";
                break;
            case Comb.STRAIGHTFLUSH:
                result = $"Staight Flush !";
                break;
            case Comb.FLASHROYAL:
                result = $"Flush Royal !!!";
                break;
            default:
                combination = Comb.NOTHING;
                break;
        }

        // Отправляем данные результата для выведения в UI
        GameManager.Instance.SetResult(result);
    }
}
