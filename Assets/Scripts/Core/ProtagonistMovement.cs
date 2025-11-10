using System;
using UnityEngine;

public class ProtagonistMovement : Movement
{
    public enum State { Running, Falling, Interacting, Frozen }
    [Header("Behavior")]
    public State state;
    public static event Action hasTouchedGoal;
    
    Animator AM;
    State stateBeforeFrozen = State.Running;

    [Header("Physics")] Rigidbody2D RB;
    [SerializeField] Vector3 movementDirection;
    [SerializeField] float movementSpeed = 1;
    public bool FacingRIGHT;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        AM = GetComponent<Animator>();
        ToggleTurn(FacingRIGHT);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("GATE"))
        {
            hasTouchedGoal?.Invoke();
            Destroy(gameObject);
        }
    }
    public override void ToggleFreezeStateToTrue(bool isCharacterBeingFrozen) //Called from timefairy
    {
        if(!AM) {AM = GetComponent<Animator>();}
        if (isCharacterBeingFrozen)
        {
            AM.speed = 0;
            ChangeState(State.Frozen);
        }
        else
        {
            AM.speed = 1;
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
    void ToggleTurn(bool isFacingRight)
    {
        if (isFacingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            movementDirection = transform.right;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            movementDirection = -transform.right;
        }
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
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Running:
                FixedUpdateWhileRunning();
                break;
            case State.Falling:
                break;
            case State.Interacting:
                break;
            case State.Frozen:
                break;
        }
    }
    void UpdateWhileRunning()
    {
        //Debug.Log("Running1");
    }
    void FixedUpdateWhileRunning()
    {
        //Debug.Log("Running2");
        RB.MovePosition(transform.position + movementDirection * movementSpeed * Time.deltaTime);
    }
    void UpdateWhileFalling()
    {
        
    }
    void UpdateWhileInteracting()
    {
        
    }
    void UpdateWhileFrozen()
    {
        //Debug.Log("IS FROZEN");
    }
}
