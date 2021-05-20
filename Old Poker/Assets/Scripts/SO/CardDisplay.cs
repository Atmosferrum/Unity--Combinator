using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ОТОБРАЖЕНИЕ Карты
/// </summary>
public class CardDisplay : MonoBehaviour
{
    public Card card; //Ссылка на Карту
    Image face; //Лицо Карты

    public void Refresh()
    {
        gameObject.name = card.name + " of " + card.suit; //Смена нименования Карт
        face = GetComponent<Image>(); //Получение лица Карты
        face.sprite = card.face; //Замена лица Карты
    }



}
