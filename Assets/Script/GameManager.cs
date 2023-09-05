using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
   
    public TextMeshProUGUI Score;
    public GameObject[] Lifes;

    public GameObject gameOverUI;
    public GameObject gameClearUI;

    public int DestoryLifesNum;

    // Start is called before the first frame update
    void Start()
    {
        Score = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();

        GameObject Life = GameObject.Find("LIFE");
        Lifes = new GameObject[Life.transform.childCount];
        DestoryLifesNum = Life.transform.childCount - 1;
        for (int i = 0; i < Life.transform.childCount; i++)
        {
            Lifes[i] = Life.transform.GetChild(i).gameObject;
        }

        Score.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlusScore(int num)
    {
        Score.text = (int.Parse(Score.text) + num).ToString();
    }

    public void MinusLife()
    {
        if (DestoryLifesNum >= 0)
        {
            Lifes[DestoryLifesNum].SetActive(false);
            DestoryLifesNum -= 1;
        }

        if (DestoryLifesNum <= -1)
        {
            OverOrClear();
        }
    }


    public void OverOrClear()
    {
        if (int.TryParse(Score.text, out int score))
        {
            if (score > 50 && DestoryLifesNum >= 0)
                gameClearUI.SetActive(true);
            else
                gameOverUI.SetActive(true);
        }

        

    }
}
