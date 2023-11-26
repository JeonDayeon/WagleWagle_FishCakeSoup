using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector2 clickPoint; // ���� Ŭ���� ����
    float dragSpeed = 20.0f; // ī�޶� �̵� �ӵ�

    Camera MainCamera; //�巡�׿� �� ī�޶� ������Ʈ

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
        //-------------------------------------------------------ī�޶� �巡�� �̵�
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