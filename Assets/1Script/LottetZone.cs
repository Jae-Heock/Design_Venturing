using UnityEngine;

public class LottetZone : MonoBehaviour
{
    Player player;
    public GameObject lottetPanel; // 인스펙터에서 할당
    bool isPlayerInZone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        lottetPanel.transform.localScale = Vector3.zero; // 처음엔 안 보이게
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.Space))
        {
            player.isMove = true;
            lottetPanel.transform.localScale = Vector3.one; // (1,1,1)로 변경
        }
        EscPanel();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }

    void EscPanel()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.Escape))
        {
            player.isMove = false;
            lottetPanel.transform.localScale = Vector3.zero;
        }
    }
}
