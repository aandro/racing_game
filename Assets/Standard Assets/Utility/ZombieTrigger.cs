using UnityEngine;

public class ZombieTrigger : MonoBehaviour
{

    public Animator Animator;

    void OnCollisionEnter(Collision collision)
    {
        if (!TriggerHelper.SatisfiesCondition(collision.collider))
        {
            return;
        }

        Animator.Play("fallingback");
    }
}
