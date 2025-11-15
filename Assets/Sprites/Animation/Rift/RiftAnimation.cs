using UnityEngine;

public class RiftAnimation : MonoBehaviour
{
    [SerializeField] Animator AMForMask;
    void OnEnable()
    {
        GameManager.gameHasEndedWON += TriggerWINAnimation;
        GameManager.gameHasEndedLOST += TriggerLOSTAnimation;
    }
    void OnDisable()
    {
        GameManager.gameHasEndedWON -= TriggerWINAnimation;
        GameManager.gameHasEndedLOST -= TriggerLOSTAnimation;
    }
    void TriggerWINAnimation()
    {
        AMForMask.SetTrigger("GAMEWON");
    }
    void TriggerLOSTAnimation()
    {
        AMForMask.SetTrigger("GAMELOST");
    }
}
