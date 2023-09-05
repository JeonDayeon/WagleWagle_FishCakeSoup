using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider timerSlider;
    public float gameTime;

    public GameManager game;

    private bool stopTimer;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time;

        if (time <= 0)
        {
            stopTimer = true;
            game.OverOrClear();
        }
        if (stopTimer == false)
        {
            timerSlider.value = time;
        }
    }

    
}
