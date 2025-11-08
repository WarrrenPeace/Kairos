using System;
using UnityEngine;

public class TimeFairy : MonoBehaviour
{
    [SerializeField] bool isTimeFlowing;
    public static event Action TimeObjectHasChangedState;

    void Start()
    {

    }
    public void ToggleFlowOfTime()
    {
        if (isTimeFlowing)
        {
            StopFlowOfTime();
        }
        else
        {
            StartFlowOfTime();
        }
        TimeObjectHasChangedState?.Invoke();
    }
    void StartFlowOfTime()
    {
        isTimeFlowing = true;
    }
    void StopFlowOfTime()
    {
        isTimeFlowing = false;
    }
    public bool ManagerCheckIfFlowOfTime()
    {
        if (isTimeFlowing) { return true; } else return false;
    }
}
