using UnityEngine;
using System;
using System.Diagnostics;
using TMPro;

public class LevelController : MonoBehaviour
{
    private bool _levelIsFinished = false;

    public CheckpointTrigger[] Checkpoints;

    public double TimeFor3StarsInSeconds;
    public double TimeFor2StarsInSeconds;
    public double TimeFor1StarInSeconds;

    private Stopwatch _timer = new Stopwatch();

    private int _nextCheckpoint = 0;

    public event EventHandler<int> ScoreUpdated;
    public event EventHandler CheckpointErrorOccured;
    public event EventHandler<int> TimeEnded;

    [SerializeField]
    private TextMeshProUGUI TimerLabel;

    [SerializeField]
    private TextMeshProUGUI StatisticLabel;

    void Start()
    {
        foreach (var checkpoint in Checkpoints)
        {
            checkpoint.CheckpointPassed += Checkpoint_OnCheckpointPassed;
        }

        StartCountdown();
    }

    void Update()
    {
        NoTime();
        NoCheckpoints();

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
        }
        else
        {
            CheckpointErrorOccured?.Invoke(this, EventArgs.Empty);
        }
    }

    private void StartCountdown()
    {
        _levelIsFinished = false;
        _nextCheckpoint = 0;

        _timer = new Stopwatch();
        _timer.Start();
    }

    private bool NoTime()
    {
        var availableTimeElapsed = (_timer.Elapsed.TotalSeconds >= TimeFor1StarInSeconds) || _levelIsFinished;
        if (availableTimeElapsed)
        {
            _timer.Stop();

            if (!_levelIsFinished)
            {
                TimeEnded?.Invoke(this, (int)CalculateScore());
            }
            _levelIsFinished = true;           
        }

        return availableTimeElapsed;       
    }

    private bool NoCheckpoints()
    {
        var allCheckpointsPassed = _nextCheckpoint > Checkpoints.Length - 1;
        if (allCheckpointsPassed)
        {
            if (!_levelIsFinished)
            {
                ScoreUpdated?.Invoke(this, (int)CalculateScore());
            }
            _levelIsFinished = true;
        }

        return allCheckpointsPassed;
    }

    private Score CalculateScore()
    {
        if (_nextCheckpoint <= Checkpoints.Length - 1)
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

        if (_timer.Elapsed.TotalSeconds <= TimeFor1StarInSeconds)
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
