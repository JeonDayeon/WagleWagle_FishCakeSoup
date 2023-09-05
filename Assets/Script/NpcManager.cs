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

    public bool isSickHyae = false; //식혜 줘야하는지 확인
    public bool isSelectObject = false; //게임 진행 오브젝트를 갖고 있는지 확인
    public GameObject click_obj;//프리팹 저장

    bool isStartState = false; //true일때 코루틴 진행

    public Sprite Heart;
    public Sprite Angry;

    public int number;
    //-----------------------------------------------------------------Score
    public GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        click_obj = Resources.Load("Prefabs/NpcSendObject") as GameObject;
        game = FindObjectOfType<GameManager>();
        RandomNpcSkin();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelectObject)
        {
            GameObject obj = GameObject.Find(click_obj.name + "(Clone)"); //프리팹 찾기
            if (obj != null)
            {
                obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z)); //마우스 커서에 오브젝트가 따라다니게
            }
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
                        GameObject objt = GameObject.Find(click_obj.name + "(Clone)"); //중복생성 확인용
                        if (objt == null)//중복생성 방지용
                        {
                            Instantiate(click_obj);
                        }
                        break;

                    case "ChatImage":
                        if(StateImage.gameObject == clicks)
                        {
                            State(); //각 스테이트에 맞춰서 진행되는 함수
                        }
                            break;
                }
            }
        }

        if(Input.GetMouseButtonDown(1))//왼쪽 버튼으로 행동 취소
        {
            SelectObjDestroy();
        }

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
        //StateImage.sprite = stateType[Randoms];
        //stateName = stateType[Randoms].name.ToString();
        StateImage.sprite = stateType[0];
        stateName = stateType[0].name.ToString();
    }

    void SelectObjDestroy()
    {
        isSelectObject = false;
        Destroy(GameObject.Find(click_obj.name + "(Clone)"));
    }

    public void State()
    {

        switch (stateName)
        {        
            case "ScrupTower":
                Debug.Log(stateName);
                break;

            case "SickHyae":
                Debug.Log(stateName);
                if (isSelectObject) //식혜용
                {
                    isSickHyae = false;
                    SelectObjDestroy();
                    StateImage.sprite = Heart;
                    Invoke("StopStateCorutine",1f); //2초뒤 말풍선 없어짐
                    game.PlusScore(3);
                }
                break;
        }
    }

    IEnumerator NpcState()
    {
        Debug.Log("코루틴");
        yield return new WaitForSeconds(1.0f);//2초 뒤 요구사항 반복문 시작
        isStartState = true;

        while (isStartState)
        {
            yield return new WaitForSeconds(1.0f);//2초 뒤 요구사항 반복문 시작
            RandomState(); //요구사항 가져오기

            ChatBalloon.SetActive(true); //말풍선 띄우기

            yield return new WaitForSeconds(5.0f);//5초 뒤 요구사항 마감
            if(isStartState)
            {
                StateImage.sprite = Angry;
                game.MinusLife();
                yield return new WaitForSeconds(1f);
                ChatBalloon.SetActive(false);
            }
        }
    }
    void StopStateCorutine()
    {
        isStartState = false;
        ChatBalloon.SetActive(false);
        StopCoroutine(NpcState());
    }
}
