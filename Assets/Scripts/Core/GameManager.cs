using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action gameHasEndedWON;
    public static event Action gameHasEndedLOST;
    [SerializeField] int playersNeededToWin;
    [SerializeField] bool GameOVER = false;
    [SerializeField] GameObject gameWinScreen;
    [SerializeField] GameObject gameOverScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        TimeManager.TIMEOVER += LoseState;
        ProtagonistMovement.hasTouchedGoal += UpdatePlayerWinCount;
    }

    public void UpdatePlayerWinCount()
    {
        playersNeededToWin += 1;
        if (playersNeededToWin >= 2)
        {
            WinState();
        }
    }
    void WinState()
    {
        if (!GameOVER)
        {
            gameHasEndedWON?.Invoke();
            GameOVER = true;
            Debug.Log("You Win");
            gameWinScreen.SetActive(true);
        }
    }
    void LoseState()
    {
        if (!GameOVER)
        {
            gameHasEndedLOST?.Invoke();
            GameOVER = true;
            Debug.Log("You Lost");
            gameOverScreen.SetActive(true);
        }
    }
}
