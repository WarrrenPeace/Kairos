using UnityEngine;

public class TimeFairy : MonoBehaviour
{
    [SerializeField] bool isTimeFlowing;

    void Start()
    {

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
