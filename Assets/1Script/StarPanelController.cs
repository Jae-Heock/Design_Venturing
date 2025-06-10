using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarPanelController : MonoBehaviour
{
    public Sprite[] backgrounds;  // 8개의 스프라이트를 순서대로 넣기
    public Image specialImage; // <- 특정 이미지를 보여줄 UI 오브젝트의 Image 컴포넌트
    private int currentIndex = 0;
    public float fadeSpeed = 1.0f;    // 페이드 효과 속도
    public Image displayImage;        // 이미지를 표시할 UI Image 컴포넌트
    public GameObject menuPanel; // 메뉴 이미지를 보여줄 오브젝트
    public GameObject reservePanel; // 예약 패널
    public Image reserveDisplayImage; // 예약 패널의 이미지를 표시할 Image 컴포넌트
    public Sprite[] reserveImages; // 예약 관련 이미지들
    private int reserveIndex = 0; // 예약 이미지 인덱스

    private void Start()
    {
        // 시작할 때 첫 번째 이미지 표시
        if (backgrounds.Length > 0)
        {
            displayImage.sprite = backgrounds[0];
        }

        reservePanel.SetActive(false);
        menuPanel.SetActive(false);
    }

    public void ShowNext()
    {
        if (currentIndex < backgrounds.Length - 1)
        {
            StartCoroutine(FadeTransition(currentIndex, currentIndex + 1));
            currentIndex++;
        }
    }

    public void ShowPrevious()
    {
        if (currentIndex > 0)
        {
            StartCoroutine(FadeTransition(currentIndex, currentIndex - 1));
            currentIndex--;
        }
    }

    private IEnumerator FadeTransition(int fromIndex, int toIndex)
    {
        float elapsedTime = 0;
        Color startColor = displayImage.color;
        Color endColor = new Color(1, 1, 1, 0);

        // 페이드 아웃
        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeSpeed);
            displayImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // 이미지 변경
        displayImage.sprite = backgrounds[toIndex];

        if (toIndex == 2)
        {
            specialImage.gameObject.SetActive(true);
        }
        else
        {
            specialImage.gameObject.SetActive(false);
        }

        // 페이드 인
        elapsedTime = 0;
        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeSpeed;
            displayImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        displayImage.color = new Color(1, 1, 1, 1);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }

    public void ShowReservePanel()
    {
        reservePanel.SetActive(true);
        reserveIndex = 0;
        if (reserveImages.Length > 0)
        {
            reserveDisplayImage.sprite = reserveImages[0];
        }
    }

    public void HideReservePanel()
    {
        reservePanel.SetActive(false);
    }

    public void ShowNextReserveImage()
    {
        if (reserveIndex < reserveImages.Length - 1)
        {
            reserveIndex++;
            reserveDisplayImage.sprite = reserveImages[reserveIndex];
        }
    }

    public void ShowPreviousReserveImage()
    {
        if (reserveIndex > 0)
        {
            reserveIndex--;
            reserveDisplayImage.sprite = reserveImages[reserveIndex];
        }
    }
}

