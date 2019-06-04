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
        if (!TriggerHelper.SatisfiesCondition(other))
        {
            return;
        }

        DoActivateTrigger();
    }
}