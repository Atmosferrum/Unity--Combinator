using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// СБРОС Карт
/// </summary>
public class CardReset : MonoBehaviour
{
    [SerializeField]
    Sprite cardBack; // Спина Карт
    
    /// <summary>
    /// Возвращение Карт в исходное положение
    /// </summary>
    public void Restart()
    {
        foreach (GameObject card in GameManager.Instance.cardsOnTable)
            card.GetComponent<Image>().sprite = cardBack; // Смена лица Карт
    }
}
