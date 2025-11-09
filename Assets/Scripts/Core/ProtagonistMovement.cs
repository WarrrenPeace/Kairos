using UnityEngine;

public class ProtagonistMovement : Movement
{
    public enum State { Running, Falling, Interacting, Frozen }
    [Header("Behavior")]
    public State state;
    Animator AM;
    State stateBeforeFrozen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AM = GetComponent<Animator>();
    }
    public override void ToggleFreezeState(bool isCharacterBeingFrozen) //Called from timefairy
    {
        if (isCharacterBeingFrozen)
        {
            AM.speed = 1;
            ChangeState(State.Frozen);
        }
        else
        {
            AM.speed = 0;
            ChangeState(stateBeforeFrozen);
        }
    }
    void ChangeState(State stateToChangeTo)
    {
        state = stateToChangeTo;

        switch (stateToChangeTo)
        {
            case State.Running:
                stateBeforeFrozen = stateToChangeTo;
                ChangeStateToRunning();
                break;
            case State.Falling:
                stateBeforeFrozen = stateToChangeTo;
                ChangeStateToFalling();
                break;
            case State.Interacting:
                stateBeforeFrozen = stateToChangeTo;
                ChangeStateToInteracting();
                break;
            case State.Frozen:
                ChangeStateToFrozen();
                break;
        }
    }
    void ChangeStateToRunning()
    {
        AM.SetBool("IsRunning", true);
    }
    void ChangeStateToFalling()
    {
        AM.SetBool("IsFalling", true);
    }
    void ChangeStateToInteracting()
    {
        AM.SetTrigger("Interacting");
    }
    void ChangeStateToFrozen()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Running:
                UpdateWhileRunning();
                break;
            case State.Falling:
                UpdateWhileFalling();
                break;
            case State.Interacting:
                UpdateWhileInteracting();
                break;
            case State.Frozen:
                UpdateWhileFrozen();
                break;
        }
    }
    void UpdateWhileRunning()
    {

    }
    void UpdateWhileFalling()
    {
        
    }
    void UpdateWhileInteracting()
    {
        
    }
    void UpdateWhileFrozen()
    {
        
    }
}
