using UnityEngine;
using System;
using System.Diagnostics;
using TMPro;

public class LevelController : MonoBehaviour
{
    public CheckpointTrigger[] Checkpoints;

    public double TimeFor3StarsInSeconds;
    public double TimeFor2StarsInSeconds;
    public double TimeFor1StarInSeconds;

    private Stopwatch _timer = new Stopwatch();

    private int _nextCheckpoint = 0;
    private int _currentScore = 0;

    public event EventHandler<int> ScoreUpdated;
    public event EventHandler CheckpointErrorOccured;
    public event EventHandler TimeEnded;

    [SerializeField]
    private TextMeshProUGUI TimerLabel;

    [SerializeField]
    private TextMeshProUGUI StatisticLabel;

    void Start()
    {
        foreach(var checkpoint in Checkpoints)
        {
            checkpoint.CheckpointPassed += Checkpoint_OnCheckpointPassed;
        }

        StartCountdown();
    }

    void Update()
    {
        if (NoTime())
        {
            UpdateScore();
        }

        TimerLabel.text = _timer.Elapsed.ToString("mm\\:ss\\.ff");
        StatisticLabel.text = "Checkpoints passed: " + _nextCheckpoint.ToString();
    }

    private void Checkpoint_OnCheckpointPassed(object sender, Guid e)
    {       
        if (NoTime() || NoCheckpoints())
        {
            return;
        }

        var nextCheckpoint = Checkpoints[_nextCheckpoint];
        if (nextCheckpoint.CheckpointId == e)
        {
            _nextCheckpoint++;
            UpdateScore();
        }
        else
        {
            CheckpointErrorOccured?.Invoke(this, EventArgs.Empty);
        }
    }

    private void StartCountdown()
    {
        _timer = new Stopwatch();
        _timer.Start();
    }

    private bool NoTime()
    {
        var availableTimeElapsed = (_timer.Elapsed.TotalSeconds >= TimeFor1StarInSeconds);
        if (availableTimeElapsed)
        {
            _timer.Stop();
            TimeEnded?.Invoke(this, EventArgs.Empty);
        }

        return availableTimeElapsed;       
    }

    private bool NoCheckpoints()
    {
        return _nextCheckpoint > Checkpoints.Length - 1;
    }

    private void UpdateScore()
    {
        _currentScore = (int)CalculateScore();
        ScoreUpdated?.Invoke(this, _currentScore);
    }

    private Score CalculateScore()
    {
        if (!NoCheckpoints())
        {
            return Score.NoStars;
        }

        if (_timer.Elapsed.TotalSeconds <= TimeFor3StarsInSeconds)
        {
            return Score.ThreeStars;
        }

        if (_timer.Elapsed.TotalSeconds <= TimeFor2StarsInSeconds)
        {
            return Score.TwoStars;
        }

        if (_timer.Elapsed.TotalSeconds == TimeFor1StarInSeconds)
        {
            return Score.OneStar;
        }

        return Score.NoStars;
    }
}

public enum Score: int
{
    NoStars = 0,
    OneStar = 1,
    TwoStars = 2,
    ThreeStars = 3,
}
