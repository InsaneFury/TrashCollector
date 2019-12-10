using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonobehaviourSingleton<ScoreManager>
{
    public int score = 0;
    public int itemsCollected = 0;

    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        Player.OnCollectAction += AddItemCollected;
        Player.OnCollectAction += AddScore;
    }
    private void OnDisable()
    {
        Player.OnCollectAction -= AddItemCollected;
        Player.OnCollectAction -= AddScore;
    }

    public void AddScore(Player p)
    {
        int randScore = Random.Range(10, 1000);
        score += randScore;
    }

    public void AddItemCollected(Player p)
    {
        itemsCollected++;
    }
}
