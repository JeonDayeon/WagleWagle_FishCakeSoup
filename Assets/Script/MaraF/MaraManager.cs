using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaraManager : MonoBehaviour
{
    public float speed;

    bool isLeft = true; // 왼쪽인지 아닌지 left가 참이면 회전시켜줌

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "endPoint")
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
            Debug.Log("부딪혔어요!");
        }
    }
}
