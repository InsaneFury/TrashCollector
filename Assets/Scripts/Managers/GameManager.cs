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

    ScoreManager sManager;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        sManager = ScoreManager.Get();
        player = Player.Get();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            PauseGame();
        }
        if (!player.isAlive)
        {
            GameOver();
        }
    }

    public void ActiveGame()
    {
        gameStarted = true;
        player.canPlay = true;
        UIManager.Get().ActiveInGameUI();
    }

    void GameOver()
    {
        gameStarted = false;
        player.canPlay = false;
        gameOverText.SetActive(true);
        UIManager.Get().SetGameOverResults(sManager.itemsCollected, sManager.score);
    }

    void PauseGame()
    {
        pause = !pause;
        UIManager.Get().pause.SetActive(pause);
        Time.timeScale = pause ? 0 : 1;
        player.canPlay = !pause;
    }

    public void RestartGame()
    {
        ResetPlayer();
        ActiveGame();
        gameOverText.SetActive(false);
        ScoreManager.Get().score = 0;
        ScoreManager.Get().itemsCollected = 0;
        if(pause)
        PauseGame();
    }

    public void Menu()
    {
        if (pause)
            PauseGame();
        UIManager.Get().InitMenu();

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
