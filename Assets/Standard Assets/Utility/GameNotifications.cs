using System.Collections;
using UnityEngine;

public class GameNotifications : MonoBehaviour
{
    public LevelController Controller;

    public GameObject LevelEndUI;
    public GameObject VictoryLabel;
    public GameObject FailLabel;
    public GameObject ScoreLabel;

    public GameObject[] Stars;

    public GameObject CheckpointsLabel;

    void Awake()
    {
        Controller.TimeEnded += Controller_TimeEnded;
        Controller.CheckpointErrorOccured += Controller_CheckpointErrorOccured;
        Controller.ScoreUpdated += Controller_ScoreUpdated;
    }

    private void Controller_ScoreUpdated(object sender, int e)
    {
        FailLabel.SetActive(false);
        VictoryLabel.SetActive(true);
        ShowEndLevelScreen(e);
    }

    private void Controller_TimeEnded(object sender, int e)
    {
        VictoryLabel.SetActive(false);
        FailLabel.SetActive(true);
        ShowEndLevelScreen(e);
    }

    private void ShowEndLevelScreen(int score)
    {
        ScoreLabel.SetActive(score == 0);
        for (int i = 0; i < score; i++)
        {
            Stars[i].SetActive(true);
        }

        LevelEndUI.SetActive(true);
    }

    private void Controller_CheckpointErrorOccured(object sender, System.EventArgs e)
    {
        StartCoroutine(CheckpointNotificationRoutine());
    }

    private IEnumerator CheckpointNotificationRoutine()
    {
        CheckpointsLabel.SetActive(true);
        yield return new WaitForSeconds(2);
        CheckpointsLabel.SetActive(false);
    }
}
