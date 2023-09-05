using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanWater : MonoBehaviour
{
    GameManager game;
    SpriteRenderer DirtyWater;
    GameObject CleanChat;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        DirtyWater = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        CleanChat = gameObject.transform.GetChild(1).gameObject;
        StartCoroutine(Water());
    }

    IEnumerator Water()
    {
        while (DirtyWater.color.a < 1.0f)
        {
            DirtyWater.color = new Color(DirtyWater.color.r, DirtyWater.color.g, DirtyWater.color.b, DirtyWater.color.a + 0.01f);
            yield return new WaitForSeconds(0.1f);
        }

        if (DirtyWater.color.a >= 1.0f)
        {
            CleanChat.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, 0f);

            if (hit.collider != null)
            {
                if (hit.transform.gameObject == CleanChat) //클린하게 만들면
                {
                    DirtyWater.color = new Color(DirtyWater.color.r, DirtyWater.color.g, DirtyWater.color.b, 0);
                    CleanChat.SetActive(false);
                    StartCoroutine(Water());
                    game.PlusScore(2);
                }
            }
        }
    }
}
