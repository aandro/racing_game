using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public static class TriggerHelper
{
    public static bool SatisfiesCondition(Collider c)
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
