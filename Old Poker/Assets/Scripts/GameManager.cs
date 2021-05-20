using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GameManager")]
    public static GameManager Instance; // Синглтон главного управления

    [Header("Cards")]
    public GameObject[] cardsOnTable; // Карты на столк

    [Header("Cards")]
    [SerializeField]
    private Result result; // Результат раздачи

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Передача результатов в UI
    public void SetResult(string txt) => result.resultString = txt;
}
