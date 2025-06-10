using UnityEngine;

public class StarZone : MonoBehaviour
{
    public StarPanelController starPanelController; // Inspector에서 연결
    private bool isPlayerInside = false;
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.Space))
        {
            player.isMove = true;
            starPanelController.gameObject.SetActive(true); // 패널 열기
        }


        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            player.isMove = false;
            starPanelController.gameObject.SetActive(false); // 패널 닫기
        }
    }




}
