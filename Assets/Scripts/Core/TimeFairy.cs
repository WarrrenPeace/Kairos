using System;
using UnityEngine;

public class TimeFairy : MonoBehaviour
{
    AudioSource AS;
    [SerializeField] bool isTimeFlowing;
    public static event Action TimeObjectHasChangedState;
    [SerializeField] GameObject timeColorMask;
    [SerializeField] AudioClip resume;
     [SerializeField] AudioClip pause;
    

    void Start()
    {
        AS = GetComponent<AudioSource>();
        if (isTimeFlowing)
        {
            isTimeFlowing = true;
            GetComponent<Movement>().ToggleFreezeState(true);
            timeColorMask.SetActive(isTimeFlowing);
        }
        else
        {
            isTimeFlowing = false;
            GetComponent<Movement>().ToggleFreezeState(false);
            timeColorMask.SetActive(isTimeFlowing);StopFlowOfTime();
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
        GetComponent<Movement>().ToggleFreezeState(true);
        timeColorMask.SetActive(isTimeFlowing);
        AS.PlayOneShot(resume);
    }
    void StopFlowOfTime()
    {

        isTimeFlowing = false;
        GetComponent<Movement>().ToggleFreezeState(false);
        timeColorMask.SetActive(isTimeFlowing);
        AS.PlayOneShot(pause);
    }
    public bool ManagerCheckIfFlowOfTime()
    {
        if (isTimeFlowing) { return true; } else return false;
    }
}
