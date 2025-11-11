using System;
using UnityEngine;

public class TimeFairy : MonoBehaviour
{
    [SerializeField] AudioSource AS;
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
            GetComponent<Movement>().ToggleFreezeStateToTrue(false);
            timeColorMask.SetActive(isTimeFlowing);
        }
        else
        {
            isTimeFlowing = false;
            GetComponent<Movement>().ToggleFreezeStateToTrue(true);
            timeColorMask.SetActive(isTimeFlowing);StopFlowOfTime();
        }
    }
    public void ToggleFlowOfTime()
    {
        if (isTimeFlowing)
        {
            StopFlowOfTime();
            Debug.Log(name + "IsNOTFlowing");
            TimeObjectHasChangedState?.Invoke();
        }
        else
        {
            StartFlowOfTime();
            Debug.Log(name + "IsFlowing");
            TimeObjectHasChangedState?.Invoke();
        }
    }
    void StartFlowOfTime()
    {
        isTimeFlowing = true;
        GetComponent<Movement>().ToggleFreezeStateToTrue(false);
        timeColorMask.SetActive(isTimeFlowing);
        if(AS)AS.PlayOneShot(resume);
    }
    void StopFlowOfTime()
    {

        isTimeFlowing = false;
        GetComponent<Movement>().ToggleFreezeStateToTrue(true);
        timeColorMask.SetActive(isTimeFlowing);
        if(AS)AS.PlayOneShot(pause);
    }
    public bool ManagerCheckIfFlowOfTime()
    {
        return isTimeFlowing;
    }
    void OnDestroy()
    {
        Debug.Log(name + "destroyed");
        TimeObjectHasChangedState?.Invoke();
    }
}
