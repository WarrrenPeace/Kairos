using UnityEngine;

public class KillAfterAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AnimationOver()
    {
        Destroy(gameObject);
    }
}
