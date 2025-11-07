using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateClockFromTimeManager : MonoBehaviour
{
    TextMeshProUGUI TEXT;
    float minuites;
    float seconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TEXT = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeManager.instance.timeLeftInLevel > 0)
        DisplayTime(TimeManager.instance.timeLeftInLevel);
    }
    void DisplayTime(float timeRemaning)
    {
        minuites = Mathf.FloorToInt(timeRemaning / 60);
        seconds = Mathf.FloorToInt(timeRemaning % 60);

        TEXT.text = string.Format("{0:00}:{1:00}", minuites, seconds);
    }
}
