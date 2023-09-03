using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    //-------------------------------------------------------------------NPC시작 세팅
    public RuntimeAnimatorController[] anim; //npc 애니메이터 목록
    //-------------------------------------------------------------------NPC요구사항
    private GameObject ChatBalloon;//말풍선
    public SpriteRenderer StateImage;//말풍선에 들어갈 이미지 스프라이트렌더러
    public Sprite[] stateType;//요구사항 넣어놓은 배열
    public string stateName;//현재 요구사항 이름

    public bool isSickHyae = false;
    public bool isSelectObject = false;
    public GameObject click_obj;

    // Start is called before the first frame update
    void Start()
    {
        click_obj = Resources.Load("Prefabs/NpcSendObject") as GameObject;
        RandomNpcSkin();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelectObject)
        {
            GameObject obj = GameObject.Find(click_obj.name + "(Clone)");
            obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, 0f);

            if(hit.collider != null)
            {
                GameObject clicks = hit.transform.gameObject;
                Debug.Log(clicks.name);
                switch (clicks.name)
                {
                    case "SickHyaeMachine":
                        isSelectObject = true;
                        GameObject Clickclone = Instantiate(click_obj);
                        break;
                    case "ChatImage":
                        if (isSelectObject && isSickHyae)
                        {
                            isSelectObject = false;
                            Destroy(GameObject.Find(click_obj.name + "(Clone)"));
                            isSickHyae = false;
                            StopCoroutine(NpcState());
                        }
                        //Destroy(GameObject.Find(click_obj.name+"(Clone)"));
                        break;
                }

                //click_obj.transform.position = point;
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Dowm");
    }
    void RandomNpcSkin()
    {
        ChatBalloon = gameObject.transform.GetChild(0).gameObject; //자신의 말풍선 가져오기

        Animator animator = gameObject.transform.GetComponent<Animator>();//자신의 애니메이터 갖고 오기
        int Randoms = Random.Range(0, anim.Length); //랜덤 값 받기
        animator.runtimeAnimatorController = anim[Randoms]; //배열에 있는 걸로 변경
        StartCoroutine(NpcState());//NPC 설정 완료 후 요구사항 띄우기
    }

    void RandomState()
    {
        StateImage = ChatBalloon.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        int Randoms = Random.Range(0, stateType.Length);
        StateImage.sprite = stateType[Randoms];
        stateName = stateType[Randoms].name.ToString();
    }

    public void State()
    {

        switch (stateName)
        {        
            case "Clean":
                Debug.Log(stateName);
                break;
        
            case "ScrupTower":
                Debug.Log(stateName);
                break;

            case "SickHyae":
                isSickHyae = true;
                Debug.Log(stateName);
                break;
        }
    }

    IEnumerator NpcState()
    {
        while (true)
        {
            Debug.Log("코루틴");
            yield return new WaitForSeconds(2.0f); //2초 뒤 요구사항 내보내기
            RandomState(); //요구사항 가져오기
            ChatBalloon.SetActive(true); //말풍선 띄우기
            State();//상태에 따라 준비하기
            yield return new WaitForSeconds(5.0f);//5초 뒤 요구사항 마감
            ChatBalloon.SetActive(false);
        }
    }
}
