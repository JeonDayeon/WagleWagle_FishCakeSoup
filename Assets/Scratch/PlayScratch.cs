using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScratch : MonoBehaviour
{
    Vector2 clickPoint; // 현재 클릭한 벡터
    public GameObject mask;
    public GameObject good;
    bool pressed;

    Bounds totalBounds;
    Bounds maskBounds;

    void Start()
    {
        totalBounds = new Bounds();
        maskBounds = transform.parent.GetChild(2).GetComponent<Collider2D>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        if (pressed == true)
        {
            GameObject ob = Instantiate(mask, pos, Quaternion.identity);
            ob.transform.parent = GameObject.Find("Scratch").transform;

            if (gameObject.transform.childCount != 0)
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    Collider2D collider = gameObject.transform.GetChild(i).GetComponent<Collider2D>();
                    totalBounds.Encapsulate(collider.bounds);
                }
                Debug.Log(totalBounds.size);
                Debug.Log("마크스" + maskBounds.size);
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            pressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
        }
    }
}
