using UnityEngine;

/// <summary>
/// Шаблон для Карт 
/// </summary>
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //Имя Карты
    public new string name;
    //Масть Карты
    public string suit;
    //Номер Карты
    public int number;
    //Лицо Карты
    public Sprite face;
}
