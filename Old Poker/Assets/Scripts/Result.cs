using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    Text resultTxt; // Текст UI результата
    string result; // Поле результата

    /// <summary>
    /// ПРИСВОЕНИЕ строки результата с автоматическим выводом
    /// </summary>
    public string resultString 
    {
        private get { return null; }
        set { this.result = value; ShowResult(); } 
    }

    /// <summary>
    /// ВЫВОД результата
    /// </summary>
    private void ShowResult() => resultTxt.text = $"You've got {result}";

    /// <summary>
    /// ОЧИСТКА поля резкльтата
    /// </summary>
    public void ClearResult() => resultTxt.text = ""; 
}
