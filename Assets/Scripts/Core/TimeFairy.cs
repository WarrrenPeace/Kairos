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
            timeColorMask.SetActive(isTimeFlowing);
        }
    }
    public void ToggleFlowOfTime()
    {
        if (isTimeFlowing)
        {
            StopFlowOfTime();
            Debug.Log(name + " has been toggled to Frozen");
            TimeObjectHasChangedState?.Invoke();
        }
        else
        {
            StartFlowOfTime();
            Debug.Log(name + " has been toggled to Flowing");
            TimeObjectHasChangedState?.Invoke();
        }
    }
    void StopFlowOfTime()
    {
        isTimeFlowing = false;
        GetComponent<Movement>().ToggleFreezeStateToTrue(true);
        timeColorMask.SetActive(isTimeFlowing);
        if(AS)AS.PlayOneShot(pause);
    }
    void StartFlowOfTime()
    {
        isTimeFlowing = true;
        GetComponent<Movement>().ToggleFreezeStateToTrue(false);
        timeColorMask.SetActive(isTimeFlowing);
        if(AS)AS.PlayOneShot(resume);
    }
    
    public bool ManagerCheckIfFlowOfTime()
    {
        return isTimeFlowing;
    }
    //void OnDestroy()
    //{
    //    Debug.Log(name + "destroyed");
    //    TimeObjectHasChangedState?.Invoke();
    //}
}
