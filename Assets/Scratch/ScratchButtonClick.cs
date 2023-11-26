using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchButtonClick : MonoBehaviour
{
    public GameObject maskPrefab;
    public GameObject goodPrefab;
    public Transform maskspawnPoint;
    public Transform goodspawnPoint;
    public CameraManager cameraT; //카메라 드래그 막기 용

    void Start()
    {
        cameraT = FindObjectOfType<CameraManager>();
        maskPrefab.SetActive(false);
        goodPrefab.SetActive(false);
    }


    public void OnButtonClick()
    {
        cameraT.isNotDrag_Zoom = true;
        GameObject mask = Instantiate(maskPrefab, maskspawnPoint.position, Quaternion.identity);
        mask.SetActive(true);

        GameObject good = Instantiate(goodPrefab, goodspawnPoint.position, Quaternion.identity);
        good.SetActive(true);       
    }
}
