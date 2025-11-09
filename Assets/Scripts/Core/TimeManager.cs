using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    [SerializeField] AudioSource ASTicking;
    [SerializeField] AudioSource ASMusic;
    bool isEverythingFrozen;
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
            instance = this;
    }
    void Start()
    {
        allTimeObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TimeTag"));

        TimeFairy.TimeObjectHasChangedState += CheckAllTimeObjects;
        
        CheckAllTimeObjects();
    }
    void CheckAllTimeObjects() //Called via event from any time object
    {
        if(isTimeObjectListDynamic) {allTimeObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TimeTag"));} //Enable if list needs to be updated
        
        for (int i = 0; i < allTimeObjects.Count; i++)
        {
            if (allTimeObjects[i].GetComponent<TimeFairy>().ManagerCheckIfFlowOfTime()) //If ANYTHING returns true, time IS FLOWING
            {
                //Debug.Log(i);
                TimeResumed();
                return;
            }
            TimeFrozen(); //Debug.Log("Time Frozen");
        }
            
    }
    void TimeFrozen()
    {
        isEverythingFrozen = true;
        ASTicking.Pause();
        ASMusic.Pause();
    }
    void TimeResumed()
    {
        isEverythingFrozen = false;
        if (!ASTicking.isPlaying) { ASTicking.Play(); }
        if(!ASMusic.isPlaying) {ASMusic.Play();}
        
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
        Debug.Log("TIME IS UP");
        ASTicking.Pause();
        ASMusic.Pause();
        enabled = false;
    }
}
