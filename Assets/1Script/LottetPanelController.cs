using UnityEngine;

public class LottetPanelController : MonoBehaviour
{
    public GameObject[] backgrounds;  // Image 오브젝트들을 순서대로 넣기
    private int currentIndex = 0;
    public GameObject menuPanel; // 메뉴창 오브젝트 Drag&Drop
    public AudioClip buttonSound; // 버튼 사운드 클립 Drag&Drop

    public void ShowNext()
    {
        Debug.Log("현재 인덱스: " + currentIndex);
        if (currentIndex < backgrounds.Length - 1)
        {
            GameManager.instance.PlayButtonSound();
            backgrounds[currentIndex].SetActive(false);
            currentIndex++;
            backgrounds[currentIndex].SetActive(true);
            Debug.Log("다음 이미지: " + backgrounds[currentIndex].name);
        }
    }


    public void ShowPrevious()
    {
        // 메뉴창이 켜져있으면 메뉴창을 먼저 닫기
        if (menuPanel.activeSelf)
        {
            GameManager.instance.PlayButtonSound();
            HideMenu();
            return; // 메뉴창만 닫고, 이미지 전환은 하지 않음
        }

        if (currentIndex > 0)
        {
            GameManager.instance.PlayButtonSound();
            backgrounds[currentIndex].SetActive(false);
            currentIndex--;
            backgrounds[currentIndex].SetActive(true);
        }
    }

    public void ShowMenu()
    {
        GameManager.instance.PlayButtonSound();
        menuPanel.SetActive(true); // 메뉴창 보이기
    }
    public void HideMenu()
    {
        GameManager.instance.PlayButtonSound();
        menuPanel.SetActive(false); // 메뉴창 숨기기
    }
}
