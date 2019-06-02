using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatistics : MonoBehaviour
{
    private int _checkpointCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoActivateTrigger()
    {
        Debug.Log("DoActivateTrigger" + ++_checkpointCount);
    }


}
