using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    AudioSource AS;
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
        AS = GetComponent<AudioSource>();
        allTimeObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TimeTag"));

        TimeFairy.TimeObjectHasChangedState += CheckAllTimeObjects;
        
        CheckAllTimeObjects();
    }
    void CheckAllTimeObjects() //Called via event from any time object
    {
        if(isTimeObjectListDynamic) {allTimeObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("TimeTag"));} //Enable if list needs to be updated
        
        for (int i = 0; i < allTimeObjects.Count; i++)
        {
            if (allTimeObjects[i].GetComponent<TimeFairy>().ManagerCheckIfFlowOfTime())
            {
                Debug.Log(i);
                TimeResumed();
                return;
            }
            TimeFrozen();
        }
            
    }
    void TimeFrozen()
    {
        isEverythingFrozen = true;
        AS.Pause();
    }
    void TimeResumed()
    {
        isEverythingFrozen = false;
        AS.Play();
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
        AS.Pause();
        enabled = false;
    }
}
