using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonobehaviourSingleton<GameManager>
{
    Player player;

    [Header("Player settings")]
    public Transform startPosition;

    [Header("Game Settings")]
    public bool gameStarted;
    public bool pause;

    [Header("GameOver")]
    public GameObject gameOverText;
    public GameObject winText;

    ScoreManager sManager;
    UIManager uiManager;
    TimerManager tManager;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        sManager = ScoreManager.Get();
        uiManager = UIManager.Get();
        tManager = TimerManager.Get();
        player = Player.Get();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            PauseGame();
        }
        if (tManager.timeOut)
        {
            GameOver();
        }
        if (!tManager.timeOut && sManager.itemsCollected == 7)
        {
            Win();
        }
    }

    public void ActiveGame()
    {
        ResetPlayer();
        gameStarted = true;
        uiManager.ActiveInGameUI();
    }

    void GameOver()
    {
        gameStarted = false;
        player.canPlay = false;
        gameOverText.SetActive(true);
        uiManager.SetGameOverResults(sManager.score);
    }

    void Win()
    {
        gameStarted = false;
        player.canPlay = false;
        gameOverText.SetActive(true);
        uiManager.SetGameOverResults(sManager.score);
    }

    void PauseGame()
    {
        pause = !pause;
        uiManager.pause.SetActive(pause);
        Time.timeScale = pause ? 0 : 1;
        player.canPlay = !pause;
    }

    public void RestartGame()
    {
        ResetPlayer();
        ActiveGame();
        gameOverText.SetActive(false);
        sManager.score = 0;
        sManager.itemsCollected = 0;
        tManager.ResetTimer();
        if (pause)
        PauseGame();
    }

    public void Menu()
    {
        if (pause)
            PauseGame();
        uiManager.InitMenu();

        //Sacar y cambiar por volver al menu sin recagar la scene.
        SceneManager.LoadScene("Gameplay");
    }

    void ResetPlayer()
    {
        player.transform.position = startPosition.position;
        player.transform.rotation = startPosition.rotation;
        player.canPlay = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE && !UNITY_EDITOR
        Application.Quit();
#endif
    }
}
