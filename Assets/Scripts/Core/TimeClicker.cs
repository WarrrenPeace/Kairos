using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeClicker : MonoBehaviour
{
    Vector2 mousePosition;
    GameObject currentTarget;
    //bool canClick = true;
    [SerializeField] GameObject clickEffectPrefab;

    [SerializeField] LayerMask maskForTimeObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        CheckForObjectToClick();
        CheckForClick();
    }
    void CheckForObjectToClick()
    {
        if (Physics2D.OverlapPoint(mousePosition, maskForTimeObjects))
        {
            currentTarget = Physics2D.OverlapPoint(mousePosition, maskForTimeObjects).gameObject;
        }
        else
        {
            currentTarget = null;
        }

    }
    void CheckForClick()
    {
        if (currentTarget)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ToggleTimeForClickedObject();
                Instantiate(clickEffectPrefab, currentTarget.transform.position, quaternion.identity,currentTarget.transform);
            }
        }
    }
    void ToggleTimeForClickedObject()
    {
        currentTarget.GetComponent<TimeFairy>().ToggleFlowOfTime();
    }
}
