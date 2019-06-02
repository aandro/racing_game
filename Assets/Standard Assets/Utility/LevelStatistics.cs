using System.Diagnostics;
using TMPro;
using UnityEngine;

public class LevelStatistics : MonoBehaviour
{
    private int _checkpointCount = 0;

    private Stopwatch _timer = new Stopwatch();

    [SerializeField]
    private TextMeshProUGUI TimerLabel;

    [SerializeField]
    private TextMeshProUGUI StatisticLabel;

    // Start is called before the first frame update
    void Start()
    {
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        TimerLabel.text = _timer.Elapsed.ToString("mm\\:ss\\.ff");
        StatisticLabel.text = _checkpointCount.ToString(); //todo calc

        if (_timer.Elapsed.TotalMilliseconds >= 30000) //todo is it reliable?
        {
            _timer.Stop(); //run something to show end
        }
    }


    public void StartCountdown()
    {
        _timer = new Stopwatch();
        _timer.Start();
    }

    public void DoActivateTrigger()
    {
        if (_checkpointCount == 6 || _timer.Elapsed.TotalMilliseconds >= 30000)
        {
            return;
        }

        UnityEngine.Debug.Log("DoActivateTrigger" + ++_checkpointCount); //todo run some animation ?
    }


}
