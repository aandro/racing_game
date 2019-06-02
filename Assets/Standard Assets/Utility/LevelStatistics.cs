using System.Diagnostics;
using UnityEngine;

public class LevelStatistics : MonoBehaviour
{
    private int _checkpointCount = 0;

    private Stopwatch _timer = new Stopwatch();

    [SerializeField] private double TimeElapsed;
    [SerializeField] private int StarsEarned;

    // Start is called before the first frame update
    void Start()
    {
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapsed = _timer.Elapsed.TotalSeconds; //todo
        StarsEarned = 0; //todo calc

        if (TimeElapsed == 30000) //todo is it reliable?
        {
            _timer.Stop();
        }
    }


    public void StartCountdown()
    {
        _timer = new Stopwatch();
        _timer.Start();
    }

    public void DoActivateTrigger()
    {
        UnityEngine.Debug.Log("DoActivateTrigger" + ++_checkpointCount);
    }


}
