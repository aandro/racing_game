using System;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CheckpointTrigger : MonoBehaviour
{                   
    public int triggerCount = 1;
    public bool repeatTrigger = false;

    public Guid CheckpointId { get; } = Guid.NewGuid();
    public event EventHandler<Guid> CheckpointPassed;

    private void DoActivateTrigger()
    {
        triggerCount--;

        if (triggerCount == 0 || repeatTrigger)
        {
            CheckpointPassed?.Invoke(this, CheckpointId);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!SatisfiesCondition(other))
        {
            return;
        }

        DoActivateTrigger();
    }

    private bool SatisfiesCondition(Collider c)
    {
        var t = c.transform;
        do
        {
            if (t.GetComponent<CarController>() != null)
            {
                return true;
            }

            t = t.parent;
        }
        while (t != null);

        return false;
    }
}