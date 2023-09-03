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
            Debug.Log("�ڷ�ƾ");
            yield return new WaitForSeconds(2.0f); //2�� �� �䱸���� ��������
            RandomState(); //�䱸���� ��������
            ChatBalloon.SetActive(true); //��ǳ�� ����
            State();//���¿� ���� �غ��ϱ�
            yield return new WaitForSeconds(5.0f);//5�� �� �䱸���� ����
            ChatBalloon.SetActive(false);
        }
    }
}
