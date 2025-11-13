using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public static event Action TIMEOVER;
    [SerializeField] AudioSource ASTicking;
    [SerializeField] AudioSource ASMusic;
    [SerializeField] bool isEverythingFrozen;
    public float timeLeftInLevel = 25;
    [SerializeField] bool isTimeObjectListDynamic;
    [SerializeField] List<GameObject> allTimeObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("THERE ARE 2 TIMEMANAGERS?!");
        }
        else
        {
            instance = this;
        }
    }
    void OnEnable()
    {
        TimeFairy.TimeObjectHasChangedState += CheckAllTimeObjects;
    }
    void OnDisable()
    {
        TimeFairy.TimeObjectHasChangedState -= CheckAllTimeObjects;
    }
    void Start()
    {
        instance = this;
        allTimeObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TimeTag"));
        
        CheckAllTimeObjects();

        //TimeFairy.TimeObjectHasChangedState += CheckAllTimeObjects;
    }
    void CheckAllTimeObjects() //Called via event from any time object
    {
        Debug.Log("CheckAllObjects");
        if (isTimeObjectListDynamic) { allTimeObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TimeTag")); } //Enable if list needs to be updated
        
        if(allTimeObjects.Count == 0) { Debug.Log("NoTimeObjects"); TimeFrozen(); return; }
        for (int i = 0; i < allTimeObjects.Count; i++)
        {
            if (allTimeObjects[i].GetComponent<TimeFairy>().ManagerCheckIfFlowOfTime()) //If ANYTHING returns true, time IS FLOWING
            {
                Debug.Log(allTimeObjects[i].name + " IsFlowing");
                TimeResumed();
                return;
            }

        }
        TimeFrozen(); Debug.Log("Time Frozen");
            
    }
    void TimeFrozen()
    {
        Debug.Log("TIME FROZEN");
        isEverythingFrozen = true;
        if (ASTicking != null) { if (ASTicking.isPlaying) ASTicking.Pause(); } //I didnt do this at will. ITS BUGGED WHEN RESTARTED LEVELS IDK WHY
        if (ASMusic != null) { if (ASMusic.isPlaying) ASMusic.Pause(); } //I didnt do this at will. ITS BUGGED WHEN RESTARTED LEVELS IDK WHY
    }
    void TimeResumed()
    {
        Debug.Log("TIME RESUMED");
        isEverythingFrozen = false;
        if (ASTicking != null) { if (!ASTicking.isPlaying) { ASTicking.Play(); } } //I didnt do this at will. ITS BUGGED WHEN RESTARTED LEVELS IDK WHY 
        if (ASMusic != null) { if (!ASMusic.isPlaying) { ASMusic.Play(); } } //I didnt do this at will. ITS BUGGED WHEN RESTARTED LEVELS IDK WHY
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEverythingFrozen)
        {
            TickDownLevelTimer();
        }
    }
    
    void TickDownLevelTimer()
    {
        timeLeftInLevel -= 1 * Time.deltaTime;
        if (timeLeftInLevel <= 0)
        {
            NoTimeLeft();
        }
    }
    void NoTimeLeft() //Talk to game manager to END LEVEL
    {
        //Debug.Log("TIME IS UP");
        if (ASTicking != null) { if (ASTicking.isPlaying) ASTicking.Pause(); } //I didnt do this at will. ITS BUGGED WHEN RESTARTED LEVELS IDK WHY
        if (ASMusic != null) { if (ASMusic.isPlaying) ASMusic.Pause(); } //I didnt do this at will. ITS BUGGED WHEN RESTARTED LEVELS IDK WHY
        enabled = false;
        TIMEOVER?.Invoke();
    }
}
