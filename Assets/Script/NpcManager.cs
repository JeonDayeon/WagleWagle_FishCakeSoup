using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    //-------------------------------------------------------------------NPC���� ����
    public RuntimeAnimatorController[] anim; //npc �ִϸ����� ���
    //-------------------------------------------------------------------NPC�䱸����
    private GameObject ChatBalloon;//��ǳ��
    public SpriteRenderer StateImage;//��ǳ���� �� �̹��� ��������Ʈ������
    public Sprite[] stateType;//�䱸���� �־���� �迭
    public string stateName;//���� �䱸���� �̸�

    public bool isSickHyae = false; //���� ����ϴ��� Ȯ��
    public bool isSelectObject = false; //���� ���� ������Ʈ�� ���� �ִ��� Ȯ��
    public GameObject click_obj;//������ ����

    bool isStartState = false; //true�϶� �ڷ�ƾ ����

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
            GameObject obj = GameObject.Find(click_obj.name + "(Clone)"); //������ ã��
            if (obj != null)
            {
                obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z)); //���콺 Ŀ���� ������Ʈ�� ����ٴϰ�
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
                        GameObject objt = GameObject.Find(click_obj.name + "(Clone)"); //�ߺ����� Ȯ�ο�
                        if (objt == null)//�ߺ����� ������
                        {
                            Instantiate(click_obj);
                        }
                        break;

                    case "ChatImage":
                        if(StateImage.gameObject == clicks)
                        {
                            State(); //�� ������Ʈ�� ���缭 ����Ǵ� �Լ�
                        }
                            break;
                }
            }
        }

        if(Input.GetMouseButtonDown(1))//���� ��ư���� �ൿ ���
        {
            SelectObjDestroy();
        }

    }

    void RandomNpcSkin()
    {
        ChatBalloon = gameObject.transform.GetChild(0).gameObject; //�ڽ��� ��ǳ�� ��������

        Animator animator = gameObject.transform.GetComponent<Animator>();//�ڽ��� �ִϸ����� ���� ����
        int Randoms = Random.Range(0, anim.Length); //���� �� �ޱ�
        animator.runtimeAnimatorController = anim[Randoms]; //�迭�� �ִ� �ɷ� ����
        StartCoroutine(NpcState());//NPC ���� �Ϸ� �� �䱸���� ����
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
                if (isSelectObject) //������
                {
                    isSickHyae = false;
                    SelectObjDestroy();
                    StateImage.sprite = Heart;
                    Invoke("StopStateCorutine",1f); //2�ʵ� ��ǳ�� ������
                    game.PlusScore(3);
                }
                break;
        }
    }

    IEnumerator NpcState()
    {
        Debug.Log("�ڷ�ƾ");
        yield return new WaitForSeconds(1.0f);//2�� �� �䱸���� �ݺ��� ����
        isStartState = true;

        while (isStartState)
        {
            yield return new WaitForSeconds(1.0f);//2�� �� �䱸���� �ݺ��� ����
            RandomState(); //�䱸���� ��������

            ChatBalloon.SetActive(true); //��ǳ�� ����

            yield return new WaitForSeconds(5.0f);//5�� �� �䱸���� ����
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
