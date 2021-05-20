using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ПЕРЕМЕШИВАНИЕ Карт
/// </summary>
public class Shuffler : MonoBehaviour
{
    [SerializeField]
    Card[] allCards; // Все Карты
    List<Card> defaultCards = new List<Card>(); // Карты на вылет
    Card currentCard; // Выбраная карта
    CardDisplay cardDisplay;

    public void Deal()
    {
        defaultCards.Clear(); //Отчистка списка выпавших Карт

        foreach (GameObject card in GameManager.Instance.cardsOnTable) // Присвоение данных Картам на столе
        {
            MakeRandomCard();             

            cardDisplay = card.GetComponent<CardDisplay>(); // Получение Скрипта отображающего данные Карты

            cardDisplay.card = currentCard;  // Присвоение данных Картe на столе           

            defaultCards.Add(currentCard); // Добавление Карты в выпавшие                        
        }

        foreach (GameObject card in GameManager.Instance.cardsOnTable) 
            card.GetComponent<CardDisplay>().Refresh(); // Обновление данных Карт

        Combinator.Check(); 
    }

    /// <summary>
    /// ПОЛУЧЕНИЕ рандомной Карты
    /// </summary>
    private void MakeRandomCard()
    {
        currentCard = allCards[UnityEngine.Random.Range(0, 52)]; // Получение рандоомной Карты для стола
        foreach (Card defaultCard in defaultCards) // Проверка не выпадалла ли данная карта ранее
            if (defaultCard.number == currentCard.number
             && defaultCard.suit == currentCard.suit) // Если выпадала, заменить 
                MakeRandomCard(); // Замена
    }
}
