using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaraSpawn : MonoBehaviour
{
    public GameObject Mara;         // Mara ������ 

    GameManager game;

    private BoxCollider2D area;     // BoxCollider2D�� ����� �������� ���� ����
    private List<GameObject> MaraList = new List<GameObject>();    // ������ ���� ������Ʈ ����Ʈ

    public float spawnInterval = 5f;    // ���� ��� �ð�
    public float objectLifetime = 10f;   // ������Ʈ ���� �ð�

    public AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        game = FindObjectOfType<GameManager>();
        // �ֱ������� ������Ʈ�� ��ȯ�ϴ� Coroutine ����
        StartCoroutine(Spawn(4));
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Spawn(float delayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3 spawnPos = GetRandomPosition(); // ���� ��ġ return
            GameObject instance = Instantiate(Mara, spawnPos, Quaternion.identity);
            MaraList.Add(instance); // ������Ʈ ������ ���� ����Ʈ�� �߰�

            // ���� �ð��� ���� �Ŀ� ������Ʈ�� ������� �ϴ� Coroutine ����
            StartCoroutine(DestroyObject(instance, objectLifetime));

            // ���� ���� ���� ���
            
        }
    }

    private IEnumerator DestroyObject(GameObject obj, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        // ������Ʈ�� ����Ʈ���� �����ϰ� �ı�
        if (MaraList.Contains(obj))
        {
            MaraList.Remove(obj);
            Destroy(obj);
            Debug.Log("�ð����� ������Ʈ �ı���");
            game.MinusLife();
        }
    }

    // BoxCollider2D ���� ������ ��ġ�� return
    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;  // ������Ʈ�� ��ġ
        Vector2 size = area.size;                   // BoxCollider2D, ���� ũ�� ����

        // x, y�� ���� ��ǥ ���
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ŭ���� ��ġ���� Ray�� ���� �浹�� ������Ʈ�� ������
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, 0f);

            // �浹�� ������Ʈ�� �ְ� �� ������Ʈ�� �±� '����' ������Ʈ�̸� �ı�
            if (hit.collider != null)
            {
                Debug.Log("Ŭ���� ��ü�� �±�: " + hit.collider.gameObject.tag);
                GameObject clickedObject = hit.collider.gameObject;

                if (MaraList.Contains(clickedObject))
                {
                    audioSource.Play();
                    MaraList.Remove(clickedObject);
                    Destroy(clickedObject);
                    Debug.Log("�÷��̾ ���� ������Ʈ �ı���");
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
