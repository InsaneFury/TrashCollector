using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonobehaviourSingleton<UIManager>
{
    [Header("HUD Settings")]
    public GameObject inGameHUD;
    public GameObject menuHUD;

    [Header("Score Settings")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;

    [Header("Pause Settings")]
    public GameObject pause;

    [Header("GameOver")]
    public TextMeshProUGUI maxScore;
    public TextMeshProUGUI maxItemsCollected;

    Player player;
    ScoreManager sManager;
    TimerManager tManager;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        inGameHUD.SetActive(false);
        menuHUD.SetActive(true);
        player = Player.Get();
        sManager = ScoreManager.Get();
        tManager = TimerManager.Get();
    }

    private void Update()
    {
        RefreshStats();
    }

    public void ActiveInGameUI()
    {
        inGameHUD.SetActive(true);
        menuHUD.SetActive(false);
    }

    public void InitMenu()
    {
        inGameHUD.SetActive(false);
        menuHUD.SetActive(true);
    }

    public void SetGameOverResults(int s)
    {
        score.text = s.ToString();
    }

    #region Refresh UI

    public void RefreshStats()
    {
        score.text = sManager.score.ToString();
        time.text = tManager.seconds.ToString();
    }

    #endregion
}
