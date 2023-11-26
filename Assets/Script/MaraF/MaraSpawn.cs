using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaraSpawn : MonoBehaviour
{
    public GameObject Mara;         // Mara 프리팹 

    GameManager game;

    private BoxCollider2D area;     // BoxCollider2D의 사이즈를 가져오기 위한 변수
    private List<GameObject> MaraList = new List<GameObject>();    // 생성한 마라 오브젝트 리스트

    public float spawnInterval = 5f;    // 스폰 대기 시간
    public float objectLifetime = 10f;   // 오브젝트 유지 시간

    public AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        game = FindObjectOfType<GameManager>();
        // 주기적으로 오브젝트를 소환하는 Coroutine 시작
        StartCoroutine(Spawn(4));
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Spawn(float delayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3 spawnPos = GetRandomPosition(); // 랜덤 위치 return
            GameObject instance = Instantiate(Mara, spawnPos, Quaternion.identity);
            MaraList.Add(instance); // 오브젝트 관리를 위해 리스트에 추가

            // 일정 시간이 지난 후에 오브젝트를 사라지게 하는 Coroutine 시작
            StartCoroutine(DestroyObject(instance, objectLifetime));

            // 일정 간격 동안 대기
            
        }
    }

    private IEnumerator DestroyObject(GameObject obj, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        // 오브젝트를 리스트에서 제거하고 파괴
        if (MaraList.Contains(obj))
        {
            MaraList.Remove(obj);
            Destroy(obj);
            Debug.Log("시간오버 오브젝트 파괴됨");
            game.MinusLife();
        }
    }

    // BoxCollider2D 내의 랜덤한 위치를 return
    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;  // 오브젝트의 위치
        Vector2 size = area.size;                   // BoxCollider2D, 맵의 크기 벡터

        // x, y축 랜덤 좌표 얻기
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭한 위치에서 Ray를 쏴서 충돌한 오브젝트를 가져옴
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, 0f);

            // 충돌한 오브젝트가 있고 그 오브젝트가 태그 '마라' 오브젝트이면 파괴
            if (hit.collider != null)
            {
                Debug.Log("클릭한 객체의 태그: " + hit.collider.gameObject.tag);
                GameObject clickedObject = hit.collider.gameObject;

                if (MaraList.Contains(clickedObject))
                {
                    audioSource.Play();
                    MaraList.Remove(clickedObject);
                    Destroy(clickedObject);
                    Debug.Log("플레이어에 의해 오브젝트 파괴됨");
                    game.PlusScore(2);

                    /*
                    GameObject manager = GameObject.Find("ScoreManager");
                    manager.GetComponent<ScoreManager>().IncScore();
                    */
                }
            }
        }
    }
}
