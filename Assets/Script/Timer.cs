using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider timerSlider;
    public float gameTime = 20f;

    public GameManager game;

    private bool stopTimer;

    void Start()
    {
        timerSlider = gameObject.transform.GetComponent<Slider>();
        game = FindObjectOfType<GameManager>();
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = timerSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerSlider.value > 0)
        {
            timerSlider.value -= Time.deltaTime;
        }

        if(timerSlider.value <= 0)
        {
            Debug.Log("³¡³µ´ç");
            game.OverOrClear();
        }
    }

    
}
