using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector2 clickPoint; // 현재 클릭한 벡터
    float dragSpeed = 20.0f; // 카메라 이동 속도

    Camera MainCamera; //드래그에 쓸 카메라 컴포넌트

    public bool isNotDrag_Zoom = false;
    private void Start()
    {
        MainCamera = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (!isNotDrag_Zoom)
        {
            Drag();
            Zoom();
        }
    }

    private void Drag()
    {
        //-------------------------------------------------------카메라 드래그 이동
        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            Vector3 position
                    = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);
            Vector3 move = position * (Time.deltaTime * dragSpeed);

            transform.Translate(move);
            transform.transform.position
                = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * dragSpeed;
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && MainCamera.fieldOfView >= 25f)
        {
            MainCamera.fieldOfView += distance;
        }

        else if(Input.GetAxis("Mouse ScrollWheel") < 0 && MainCamera.fieldOfView <= 93f)
        {
            MainCamera.fieldOfView += distance;
        }
    }
}