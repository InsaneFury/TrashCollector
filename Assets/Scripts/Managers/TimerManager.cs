using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonobehaviourSingleton<TimerManager>
{

    UIManager uiManager;
    GameManager gManager;

    public float maxTime = 20;
    public float timer = 0;
    [HideInInspector]
    public int seconds = 0;
    public bool timeOut = false;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        uiManager = UIManager.Get();
        gManager = GameManager.Get();
        timer = maxTime;
    }

    void Update()
    {
        if (gManager.gameStarted)
            Timer();
    }

    void Timer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            seconds = (int)(timer % 60);
        }
        else
        {
            timer = 0;
            timeOut = true;
        }
        
    }
    
    public void ResetTimer()
    {
        timer = maxTime;
        seconds = 0;
        timeOut = false;
    }
}
