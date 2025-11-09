using System;
using UnityEngine;

public class TimeFairy : MonoBehaviour
{
    [SerializeField] bool isTimeFlowing;
    public static event Action TimeObjectHasChangedState;
    [SerializeField] GameObject timeColorMask;
    

    void Start()
    {
        if (isTimeFlowing)
        {
            StartFlowOfTime();
        }
        else
        {
            StopFlowOfTime();
        }
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
        timeColorMask.SetActive(isTimeFlowing);
    }
    void StopFlowOfTime()
    {

        isTimeFlowing = false;
        timeColorMask.SetActive(isTimeFlowing);
    }
    public bool ManagerCheckIfFlowOfTime()
    {
        if (isTimeFlowing) { return true; } else return false;
    }
}
