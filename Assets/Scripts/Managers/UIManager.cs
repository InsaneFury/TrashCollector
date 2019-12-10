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

    [Header("Pause Settings")]
    public GameObject pause;

    [Header("GameOver")]
    public TextMeshProUGUI maxScore;
    public TextMeshProUGUI maxItemsCollected;

    Player player;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        inGameHUD.SetActive(false);
        menuHUD.SetActive(true);
        player = Player.Get();
    }

    private void Update()
    {

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

    public void SetGameOverResults(int i,int s)
    {
        maxItemsCollected.text = i.ToString();
        score.text = s.ToString();
    }

    #region Refresh UI

    public void RefreshStats()
    {
        score.text = ScoreManager.Get().score.ToString();
    }

    #endregion
}
